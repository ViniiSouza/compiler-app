using LexicalAnalyzer;
using SemanticAnalyzer;
using AnalyzerUtils;
using System.Collections;

namespace SyntaticAnalyzer
{
    public class Sintatico : Constants
    {
        private Stack stack = new Stack();
        private ParserConstants parserConstants = new ParserConstants();
        private Semantico semanticAnalyser = new Semantico();
        private Lexico scanner = new Lexico();
        private Token? currentToken;
        private Token? previousToken;

        private static bool IsTerminal(int x)
        {
            return x < ParserConstants.FIRST_NON_TERMINAL;
        }
        private static bool IsNonTerminal(int x)
        {
            return x >= ParserConstants.FIRST_NON_TERMINAL && x < ParserConstants.FIRST_SEMANTIC_ACTION;
        }

        private static bool IsSemanticAction(int x)
        {
            return x >= ParserConstants.FIRST_SEMANTIC_ACTION;
        }

        private bool Step()
        {
            if (currentToken == null)
            {
                int pos = 0;
                if (previousToken != null)
                    pos = previousToken.GetPosition()+previousToken.GetLexeme().Length;

                currentToken = new Token(DOLLAR, "$", pos);
            }

            int x = (int) stack.Pop();  
            int a = currentToken.GetId();

            if (x == EPSILON)
            {
                return false;
            }
            else if (IsTerminal(x))
            {
                if (x == a)
                {
                    if (stack.Count == 0)
                        return true;
                    else
                    {
                        previousToken = currentToken;
                        currentToken = scanner.NextToken();
                        return false;
                    }
                }
                else
                {
                    throw new SyntaticError(parserConstants.PARSER_ERROR[x], currentToken.GetPosition(), currentToken.GetLexeme());
                }
            }
            else if (IsNonTerminal(x))
            {
                if (PushProduction(x, a))
                    return false;
                else
                    throw new SyntaticError(parserConstants.PARSER_ERROR[x], currentToken.GetPosition(), currentToken.GetLexeme());
            }
            else if (IsSemanticAction(x))
            {
                semanticAnalyser.ExecuteAction(x - ParserConstants.FIRST_SEMANTIC_ACTION, previousToken);
                return false;
            }
            return false;
        }

        private bool PushProduction(int topStack, int tokenInput)
        {
            int p = parserConstants.PARSER_TABLE[topStack - ParserConstants.FIRST_NON_TERMINAL, tokenInput - 1];
            if (p >= 0)
            {
                int[] production = parserConstants.PRODUCTIONS[p];
                //empilha a produção em ordem reversa
                for (int i = production.Length - 1; i >= 0; i--)
                {
                    stack.Push(production[i]);
                }
                return true;
            }
            else
                return false;
        }

        public void Parse(Lexico scanner, Semantico semanticAnalyser)
        {
            this.scanner = scanner;
            this.semanticAnalyser = semanticAnalyser;

            stack.Clear();
            stack.Push(DOLLAR);
            stack.Push(ParserConstants.START_SYMBOL);

            currentToken = scanner.NextToken();

            while ( ! Step() )
                ;
        }
    }
}