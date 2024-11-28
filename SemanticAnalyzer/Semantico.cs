using AnalyzerUtils;
using System;
using System.Collections;

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
            RelationalOperators.Add("==", "ceq");
            RelationalOperators.Add("!=", "ceq\nnot");
            RelationalOperators.Add("<", "clt");
            RelationalOperators.Add(">", "cgt");
            TypeClasses.Add("int64", "Int64");
            TypeClasses.Add("float64", "Double");
            TypeClasses.Add("bool", "Boolean");
        }

        public void ExecuteAction(int action, Token token)
        {
            Console.WriteLine("Ação #" + action + ", Token: " + token);
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
                    AppendToCode($"ldstr {token.GetLexeme()}");
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
                    string tipo1 = (string)PilhaTipos.Pop();
                    string tipo2 = (string)PilhaTipos.Pop();
                    ValidateTypes(tipo1, tipo2, token);
                    AppendToCode("add");
                    break;
                // operador aritmetico binario "-"
                case 124:
                    tipo1 = (string)PilhaTipos.Pop();
                    tipo2 = (string)PilhaTipos.Pop();
                    ValidateTypes(tipo1, tipo2, token);
                    AppendToCode("sub");
                    break;
                // operador aritmetico binario "*"
                case 125:
                    tipo1 = (string)PilhaTipos.Pop();
                    tipo2 = (string)PilhaTipos.Pop();
                    ValidateTypes(tipo1, tipo2, token);
                    AppendToCode("mul");
                    break;
                // operador aritmetico binario "/"
                case 126:
                    tipo1 = (string)PilhaTipos.Pop();
                    tipo2 = (string)PilhaTipos.Pop();

                    if (tipo1 != "int64" && tipo1 != "float64")
                    {
                        throw new SemanticError($"Operando mais à direita deve ser numérico", token.GetPosition());
                    }

                    if (tipo2 != "int64" && tipo2 != "float64")
                    {
                        throw new SemanticError($"Operando mais à esquerda deve ser numérico", token.GetPosition());
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
                    tipo1 = (string)PilhaTipos.Pop();
                    tipo2 = (string)PilhaTipos.Pop();
                    if (tipo1 != tipo2)
                    {
                        throw new SemanticError($"Tipo {tipo1} não compatível com {tipo2}", token.GetPosition());
                    }
                    PilhaTipos.Push("bool");
                    AppendToCode(RelationalOperators[OperadorRelacional]);
                    break;
                // operador logico unario "!"
                case 120:
                    string tipo = (string)PilhaTipos.Pop();
                    if (tipo != "bool")
                    {
                        throw new SemanticError("Operando deve ser booleano", token.GetPosition());
                    }

                    AppendToCode(
                        "ldc.i4.1",
                        "xor"
                    );
                    break;
                // operador logico &&
                case 116:
                    tipo1 = (string)PilhaTipos.Pop();
                    tipo2 = (string)PilhaTipos.Pop();

                    if (tipo1 != "bool")
                    {
                        throw new SemanticError("Operando mais à direita deve ser booleano", token.GetPosition());
                    }

                    if (tipo2 != "bool")
                    {
                        throw new SemanticError("Operando mais à esquerda deve ser booleano", token.GetPosition());
                    }

                    PilhaTipos.Push("bool");
                    AppendToCode("and");
                    break;
                // operador logico ||
                case 117:
                    tipo1 = (string)PilhaTipos.Pop();
                    tipo2 = (string)PilhaTipos.Pop();

                    if (tipo1 != "bool")
                    {
                        throw new SemanticError("Operando mais à direita deve ser booleano", token.GetPosition());
                    }

                    if (tipo2 != "bool")
                    {
                        throw new SemanticError("Operando mais à esquerda deve ser booleano", token.GetPosition());
                    }

                    PilhaTipos.Push("bool");
                    AppendToCode("or");
                    break;
                // identificadores em expressoes
                case 127:
                    string identificador = token.GetLexeme();
                    if (!TabelaSimbolos.ContainsKey(identificador))
                    {
                        throw new SemanticError($"{identificador} já declarado", token.GetPosition());
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
                    tipo = (string)PilhaTipos.Pop();
                    if (tipo == "int64")
                    {
                        AppendToCode("conv.i8");
                    }

                    AppendToCode($"call void [mscorlib]System.Console::Write({tipo})");
                    break;
                // quebra de linha por padrao (writeln)
                case 107:
                    AppendToCode(
                        "ldstr \"\"",
                        "call void [mscorlib]System.Console::WriteLine(string)"
                    );
                    break;
                // inserir identificador na lista de identificadores
                case 104:
                    ListaIdentificadores.Add(token.GetLexeme());
                    break;
                // declara variavel
                case 102:
                    foreach (var id in ListaIdentificadores)
                    {
                        if (TabelaSimbolos.ContainsKey(id))
                        {
                            throw new SemanticError($"{id} já declarado", token.GetPosition());
                        }

                        tipo = ExtractType(id, token);
                        TabelaSimbolos.Add(id, tipo);
                        AppendToCode($".locals ({tipo} {id})");
                    }
                    ListaIdentificadores.Clear();
                    break;
                // atribuicao de valores
                case 103:
                    tipo = (string)PilhaTipos.Pop();
                    if (tipo == "int64")
                    {
                        AppendToCode("conv.i8");
                    }

                    for (int i = 0; i < ListaIdentificadores.Count - 1; i++)
                    {
                        AppendToCode("dup");
                    }

                    foreach (string id in ListaIdentificadores)
                    {
                        if (!TabelaSimbolos.ContainsKey(id))
                        {
                            throw new SemanticError($"{id} não declarado", token.GetPosition());
                        }

                        AppendToCode($"stloc {id}");

                    }

                    ListaIdentificadores.Clear();
                    break;
                // escreve constante string do comando de entrada
                case 106:
                    AppendToCode(
                        $"ldstr {token.GetLexeme()}",
                        "call void [mscorlib]System.Console::Write(string)"
                    );
                    break;
                // comando de entrada (read)
                case 105:
                    identificador = token.GetLexeme();
                    if (!TabelaSimbolos.ContainsKey(identificador))
                    {
                        throw new SemanticError($"{identificador} não declarado", token.GetPosition());
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
                    string rotulo_Desempilhado2 = (string)PilhaRotulos.Pop();
                    string rotulo_Desempilhado1 = (string)PilhaRotulos.Pop();

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
                    string novoRotulo5 = GenerateLabel();
                    AppendToCode($"{novoRotulo5}:");

                    PilhaRotulos.Push(novoRotulo5);
                    break;
                case 114:
                    string rotulo_Desempilhado3 = (string)PilhaRotulos.Pop();
                    AppendToCode($"brtrue {rotulo_Desempilhado3}");

                    break;
                case 115:
                    string rotulo_Desempilhado7 = (string)PilhaRotulos.Pop();
                    AppendToCode($"brfalse {rotulo_Desempilhado7}");

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

        public void ValidateTypes(string type1, string type2, Token token)
        {
            if (type1 == "int64")
            {
                if (type2 == "int64")
                {
                    PilhaTipos.Push("int64");
                }
                else if (type2 == "float64")
                {
                    PilhaTipos.Push("float64");
                }
                else
                {
                    throw new SemanticError($"Tipo {type2} não compatível com {type1}", token.GetPosition());
                }
            }
            else if (type1 == "float64")
            {
                if (type2 != "int64" && type2 != "float64")
                {
                    throw new SemanticError($"Tipo {type1} não compatível com {type2}", token.GetPosition());
                }

                PilhaTipos.Push("float64");
            }
            else
            {
                throw new SemanticError($"Tipo {type1} incompatível", 1);
            }
        }

        public string ExtractType(string identifier, Token token)
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
                    throw new SemanticError($"Tipo {prefix} inválido", token.GetPosition());
            }
        }
    }
}
