﻿using AnalyzerUtils;

namespace LexicalAnalyzer
{
    public class LexicalError : AnalysisError
    {
        private string lexeme { get; set; }
        public LexicalError(string msg, int position) : base(msg, position)
        {
            
        }

        public LexicalError(string msg) : base(msg)
        {
            
        }

        public LexicalError(string msg, int position, string lexeme) : base(msg, position)
        {
            if (msg == "constante_string inválida")
            {
                this.lexeme = "";
                return;
            }
            this.lexeme = " " + lexeme;
        }

        public string GetLexeme()
        {
            return lexeme;
        }
    }
}
