using AnalyzerUtils;
using System.Collections;
using System.Collections.Generic;

namespace SemanticAnalyzer
{
    public class Semantico : Constants
    {
        private Stack PilhaTipos = new();
        private Stack PilhaRotulos = new();
        private List<string> ListaIdentificadores = [];
        private List<string> Codigo = [];
        private Dictionary<string, string> TabelaSimbolos = new Dictionary<string, string>();
        private string OperadorRelacional = "";
        private Dictionary<string, string> RelationalOperators = new Dictionary<string, string>();
        private Dictionary<string, string> TypeClasses = new Dictionary<string, string>();
        private int contadorRotulos = 0;


        public Semantico()
        {
            RelationalOperators.Add("==", "cet");
            RelationalOperators.Add("!=", "cet\nnot");
            RelationalOperators.Add("<", "clt");
            RelationalOperators.Add(">", "cgt");
            TypeClasses.Add("int64", "Int64");
            TypeClasses.Add("float64", "Double");
            TypeClasses.Add("bool", "Boolean");
        }

        public void ExecuteAction(int action, Token token)
        {
            Console.WriteLine("Ação #"+action+", Token: "+token);
            switch (action) 
            {
                // cabeçalho do programa
                case 100:
                    AppendToCode(
                        ".assembly extern mscorlib {}",
                        ".assembly _codigo_objeto{}",
                        ".module _codigo_objeto.exe",
                        "",
                        ".class public _UNICA{",
                        ".method static public void _principal(){",
                        ".entrypoint"
                    );
                    break;
                // final do programa
                case 101:
                    AppendToCode("ret", "}", "}");
                    break;
                // constante bool true
                case 118:
                    PilhaTipos.Push("bool");
                    AppendToCode("ldc.i4.1");
                    break;
                // constante bool false
                case 119:
                    PilhaTipos.Push("bool");
                    AppendToCode("ldc.i4.0");
                    break;
                // constante int
                case 128:
                    PilhaTipos.Push("int64");
                    AppendToCode(
                        $"ldc.i8 {token.GetLexeme()}",
                        "conv.r8"
                    );
                    break;
                // constante float
                case 129:
                    PilhaTipos.Push("float64");
                    var valorConvertido = token.GetLexeme().Replace(",", ".");
                    AppendToCode($"ldc.r8 {valorConvertido}");
                    break;
                // constante string
                case 130:
                    PilhaTipos.Push("string");
                    AppendToCode($"ldstr \"{token.GetLexeme()}\"");
                    break;
                // operador aritmético unário "-"
                case 131:
                    AppendToCode(
                        "ldc.i8 -1",
                        "conv.r8",
                        "mul"
                    );
                    break;
                // operador aritmetico binario "+"
                case 123:
                    string tipo1 = (string) PilhaTipos.Pop();
                    string tipo2 = (string) PilhaTipos.Pop();
                    ValidateTypes(tipo1, tipo2);
                    AppendToCode("add");
                    break;
                // operador aritmetico binario "-"
                case 124:
                    tipo1 = (string) PilhaTipos.Pop();
                    tipo2 = (string) PilhaTipos.Pop();
                    ValidateTypes(tipo1, tipo2);
                    AppendToCode("sub");
                    break;
                // operador aritmetico binario "*"
                case 125:
                    tipo1 = (string) PilhaTipos.Pop();
                    tipo2 = (string) PilhaTipos.Pop();
                    ValidateTypes(tipo1, tipo2);
                    AppendToCode("mul");
                    break;
                // operador aritmetico binario "/"
                case 126:
                    tipo1 = (string) PilhaTipos.Pop();
                    tipo2 = (string) PilhaTipos.Pop();

                    if (tipo1 != "int64" || tipo1 != "float64")
                    {
                        throw new SemanticError("a", 1);
                    }

                    if (tipo2 != "int64" || tipo2 != "float64")
                    {
                        throw new SemanticError("a", 1);
                    }

                    PilhaTipos.Push("float64");
                    AppendToCode("div");
                    break;
                // guardar operadores relacionais
                case 121:
                    OperadorRelacional = token.GetLexeme();
                    break;
                // executar operacao relacional
                case 122:
                    tipo1 = (string) PilhaTipos.Pop();
                    tipo2 = (string) PilhaTipos.Pop();
                    if (tipo1 != tipo2) {
                        throw new SemanticError("a", 1);
                    }
                    PilhaTipos.Push("bool");
                    AppendToCode(RelationalOperators[OperadorRelacional]);
                    break;
                // operador logico unario "!"
                case 120:
                    string tipo = (string) PilhaTipos.Pop();
                    if (tipo != "bool")
                    {
                        throw new SemanticError("a", 1);
                    }
        
                    AppendToCode(
                        "ldc.i4.1",
                        "xor"
                    );
                    break;
                // operador logico &&
                case 116:
                    tipo1 = (string) PilhaTipos.Pop();
                    tipo2 = (string) PilhaTipos.Pop();

                    if (tipo1 != "bool")
                    {
                        throw new SemanticError("a", 1);
                    }

                    if (tipo2 != "bool")
                    {
                        throw new SemanticError("a", 1);
                    }

                    PilhaTipos.Push("bool");
                    AppendToCode("and");
                    break;
                // operador logico ||
                case 117:
                    tipo1 = (string) PilhaTipos.Pop();
                    tipo2 = (string) PilhaTipos.Pop();

                    if (tipo1 != "bool")
                    {
                        throw new SemanticError("a", 1);
                    }

                    if (tipo2 != "bool")
                    {
                        throw new SemanticError("a", 1);
                    }

                    PilhaTipos.Push("bool");
                    AppendToCode("or");
                    break;
                // identificadores em expressoes
                case 127:
                    string identificador = token.GetLexeme();
                    if (!TabelaSimbolos.ContainsKey(identificador))
                    {
                        throw new SemanticError("a", 1);
                    }

                    tipo = TabelaSimbolos[identificador];
                    PilhaTipos.Push(tipo);
                    AppendToCode($"ldloc {token.GetLexeme()}");

                    if (tipo == "int64")
                    {
                        AppendToCode("conv.r8");
                    }

                    break;
                // comando de saida (write)
                case 108:
                    tipo = (string) PilhaTipos.Pop();
                    if (tipo == "int64")
                    {
                        AppendToCode("conv.i8");
                    }

                    AppendToCode($"call void [mscorlib]System.Console::Write({tipo})");
                    break;
                // quebra de linha por padrao (writeln)
                case 107:
                    AppendToCode(
                        "ldstr \"\\n\"",
                        "call void [mscorlib]System.Console::Write(string)"
                    );
                    break;
                // inserir identificador na lista de identificadores
                case 104:
                    ListaIdentificadores.Add(token.GetLexeme());
                    break;
                // declara variavel
                case 102:
                    identificador = token.GetLexeme();
                    if (!TabelaSimbolos.ContainsKey(identificador))
                    {
                        throw new SemanticError("a", 1);
                    }

                    tipo = ExtractType(identificador);
                    TabelaSimbolos.Add(identificador, tipo);
                    AppendToCode($".locals ({tipo} {identificador})");
                    ListaIdentificadores.Clear();
                    break;
                // atribuicao de valores
                case 103:
                    tipo = (string) PilhaTipos.Pop();
                    if (tipo == "int64")
                    {
                        AppendToCode("conv.i8");
                    }

                    foreach (string _ in ListaIdentificadores)
                    {
                        AppendToCode("dup");
                    }

                    foreach (string id in ListaIdentificadores)
                    {
                        if (!TabelaSimbolos.ContainsKey(id))
                        {
                            throw new SemanticError("a", 1);
                        }

                        AppendToCode($"stloc {id}");

                    }

                    ListaIdentificadores.Clear();
                    break;
                // escreve constante string do comando de entrada
                case 106:
                    AppendToCode(
                        $"ldstr \"{token.GetLexeme()}\"",
                        "call void [mscorlib]System.Console::Write(string)"
                    );
                    break;
                // comando de entrada (read)
                case 105:
                    identificador = token.GetLexeme();
                    if (!TabelaSimbolos.ContainsKey(identificador))
                    {
                        throw new SemanticError("a", 1);
                    }

                    AppendToCode("call string [mscorlib]System.Console::ReadLine()");
                    tipo = TabelaSimbolos[identificador];
                    if (tipo != "string")
                    {
                        AppendToCode($"call {tipo} [mscorlib]System.{TypeClasses[tipo]}::Parse(string)");
                    }

                    AppendToCode($"stloc {identificador}");
                    break;
                //if caso o resultado da avaliasao for false
                case 109:
                    string novoRotulo1 = GenerateLabel();
                    PilhaRotulos.Push(novoRotulo1);

                    string novoRotulo2 = GenerateLabel();
                    PilhaRotulos.Push(novoRotulo2);

                    AppendToCode($"brfalse {novoRotulo2}");
                    break;

                case 110:
                    string rotulo_Desempilhado1 = (string)PilhaRotulos.Pop();
                    string rotulo_Desempilhado2 = (string)PilhaRotulos.Pop();

                    AppendToCode($"br {rotulo_Desempilhado1}");

                    PilhaRotulos.Push(rotulo_Desempilhado1);

                    AppendToCode($"{rotulo_Desempilhado2}:");
                    break;
                case 111:
                    string rotulo_Desempilhado = (string)PilhaRotulos.Pop();

                    AppendToCode($"{rotulo_Desempilhado}:");
                    break;
                case 112:
                    string novoRotulo = GenerateLabel();

                    AppendToCode($"brfalse {novoRotulo}");

                    PilhaRotulos.Push(novoRotulo);
                    break;
                case 113:
                    novoRotulo = GenerateLabel();
                    AppendToCode($"{novoRotulo}:");

                    PilhaRotulos.Push(novoRotulo);
                    break;
                case 114:
                    rotulo_Desempilhado = (string)PilhaRotulos.Pop();
                    AppendToCode($"brtrue {rotulo_Desempilhado}");

                    break;
                case 115:
                    rotulo_Desempilhado = (string)PilhaRotulos.Pop();
                    AppendToCode($"brfalse {rotulo_Desempilhado}");

                    break;
                default:
                    // em teoria não deveria chegar aqui
                    break;
            }
        }

        //Gera um rótulo novo único
        public string GenerateLabel() 
        {
            return $"Label{contadorRotulos++}";
        }

        public void AppendToCode(params string[] linhas)
        {
            Codigo.AddRange(linhas);
        }

        public string GetCodigo()
        {
            return string.Join("\n", Codigo);
        }

        public void ValidateTypes(string type1, string type2)
        {
            if (type1 == "int64")
            {
                if (type2 == "int64")
                {
                    PilhaTipos.Push("int64");
                } else if (type2 == "float64")
                {
                    PilhaTipos.Push("float64");
                } else
                {
                    throw new SemanticError("a", 1);
                }
            } else if (type1 == "float64")
            {
                if (type2 != "int64" || type2 != "float64")
                {
                    throw new SemanticError("a", 1);
                }

                PilhaTipos.Push("float64");
            } else
            {
                throw new SemanticError("a", 1);
            }
        }

        public string ExtractType(string identifier)
        {
            string prefix = identifier.Split("_")[0];
            switch (prefix)
            {
                case "i":
                    return "int64";
                case "f":
                    return "float64";
                case "s":
                    return "string";
                case "b":
                    return "bool";
                default:
                    throw new SemanticError("a", 1);
            }
        }
    }
}
