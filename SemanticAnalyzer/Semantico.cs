﻿using AnalyzerUtils;
using System.Collections;
using System.Collections.Generic.Dictionary;

Dictionary<string, string> RelationalOperators = new Dictionary<string, string>();
RelationalOperators.Add("==", "cet");
RelationalOperators.Add("!=", "cet\nnot");
RelationalOperators.Add("<", "clt");
RelationalOperators.Add(">", "cgt");

Dictionary<string, string> TypeClasses = new Dictionary<string, string>();
RelationalOperators.Add("int64", "Int64");
RelationalOperators.Add("float64", "Double");
RelationalOperators.Add("bool", "Boolean");

namespace SemanticAnalyzer
{
    public class Semantico : Constants
    {
        private Stack PilhaTipos = new();
        private Stack PilhaRotulos = new();
        private List<string> ListaIdentificadores = [];
        private List<string> Codigo = [];
        private Dictionary<string, string> TabelaSimbolos = new Dictionary<string, string>();
        private OperadorRelacional string;

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
                    string tipo1 = PilhaTipos.Pop();
                    string tipo2 = PilhaTipos.Pop();
                    ValidateTypes(tipo1, tipo2);
                    AppendToCode("add");
                    break;
                // operador aritmetico binario "-"
                case 124:
                    string tipo1 = PilhaTipos.Pop();
                    string tipo2 = PilhaTipos.Pop();
                    ValidateTypes(tipo1, tipo2);
                    AppendToCode("sub");
                    break;
                // operador aritmetico binario "*"
                case 125:
                    string tipo1 = PilhaTipos.Pop();
                    string tipo2 = PilhaTipos.Pop();
                    ValidateTypes(tipo1, tipo2);
                    AppendToCode("mul");
                    break;
                // operador aritmetico binario "/"
                case 126
                    string tipo1 = PilhaTipos.Pop();
                    string tipo2 = PilhaTipos.Pop();

                    if (tipo1 != "int64" || tipo1 != "float64")
                    {
                        throw SemanticError;
                    }

                    if (tipo2 != "int64" || tipo2 != "float64")
                    {
                        throw SemanticError
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
                    string tipo1 = PilhaTipos.Pop();
                    string tipo2 = PilhaTipos.Pop();
                    if (tipo1 != tipo2) {
                        throw SemanticError;
                    }
                    PilhaTipos.Push("bool");
                    AppendToCode(RelationalOperators[OperadorRelacional]);
                    break;
                // operador logico unario "!"
                case 120:
                    string tipo = PilhaTipos.Pop();
                    if (PilhaTipos != "bool")
                    {
                        throw SemanticError;
                    }
        
                    AppendToCode(
                        "ldc.i4.1",
                        "xor"
                    );
                    break;
                // operador logico &&
                case 116:
                    string tipo1 = PilhaTipos.Pop();
                    string tipo2 = PilhaTipos.Pop();

                    if (tipo1 != "bool")
                    {
                        throw SemanticError;
                    }

                    if (tipo2 != "bool")
                    {
                        throw SemanticError;
                    }

                    PilhaTipos.Push("bool");
                    AppendToCode("and");
                    break;
                // operador logico ||
                case 117:
                    string tipo1 = PilhaTipos.Pop();
                    string tipo2 = PilhaTipos.Pop();

                    if (tipo1 != "bool")
                    {
                        throw SemanticError;
                    }

                    if (tipo2 != "bool")
                    {
                        throw SemanticError;
                    }

                    PilhaTipos.Push("bool");
                    AppendToCode("or");
                    break;
                // identificadores em expressoes
                case 127:
                    string identificador = token.GetLexeme();
                    if (!TabelaSimbolos.ContainsKey(identificador))
                    {
                        throw SemanticError;
                    }

                    string tipo = TabelaSimbolos[identificador];
                    PilhaTipos.Push(tipo);
                    AppendToCode($"ldloc {token.GetLexeme()}");

                    if (tipo == "int64")
                    {
                        AppendToCode("conv.r8");
                    }

                    break;
                // comando de saida (write)
                case 108:
                    string tipo = PilhaTipos.Pop();
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
                    string identificador = token.GetLexeme();
                    if (!TabelaSimbolos.ContainsKey(identificador))
                    {
                        throw SemanticError;
                    }

                    string tipo = ExtractType(identificador);
                    TabelaSimbolos.Add(identificador, tipo);
                    AppendToCode($".locals ({tipo} {identificador})")
                    ListaIdentificadores.Clear();
                    break;
                // atribuicao de valores
                case 103:
                    string tipo = PilhaTipos.Pop();
                    if (tipo == "int64")
                    {
                        AppendToCode("conv.i8");
                    }

                    foreach (string _ in ListaIdentificadores)
                    {
                        AppendToCode("dup");
                    }

                    foreach (string identificador in ListaIdentificadores)
                    {
                        if (!TabelaSimbolos.ContainsKey(identificador))
                        {
                            throw SemanticError;
                        }

                        AppendToCode($"stloc {identificador}");

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
                    string identificador = token.GetLexeme();
                    if (!TabelaSimbolos.ContainsKey(identificador))
                    {
                        throw SemanticError;
                    }

                    AppendToCode("call string [mscorlib]System.Console::ReadLine()");
                    string tipo = TabelaSimbolos[identificador];
                    if (tipo != "string")
                    {
                        AppendToCode($"call {tipo} [mscorlib]System.{TypeClasses[tipo]}::Parse(string)");
                    }

                    AppendToCode($"stloc {identificador}");
                    break;
                default:
                    // em teoria não deveria chegar aqui
                    break;
            }
        }

        public void AppendToCode(params string[] linhas)
        {
            Codigo.AddRange(linhas);
        }

        public string GetCodigo()
        {
            return string.Join("\n", Codigo);
        }

        public void ValidateTypes(type1 string, type2 string)
        {
            if (type1 == "int64")
            {
                if (type2 == "int64")
                {
                    PilhaTipos.Push("int64")
                } else if (type2 == "float64")
                {
                    PilhaTipos.Push("float64")
                } else
                {
                    throw SemanticError
                }
            } else if (type1 == "float64")
            {
                if (type2 != "int64" || type2 != "float64")
                {
                    throw SemanticError
                }

                PilhaTipos.Push("float64")
            } else
            {
                throw SemanticError
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
                case default:
                    throw SemanticError;
            }
        }
    }
}
