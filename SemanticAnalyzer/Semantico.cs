using AnalyzerUtils;
using System.Collections;

namespace SemanticAnalyzer
{
    public class Semantico : Constants
    {
        private Stack PilhaTipos = new();
        private List<string> Codigo = [];

        public void ExecuteAction(int action, Token token)
        {
            Console.WriteLine("Ação #"+action+", Token: "+token);
            switch (action) 
            {
                // cabeçalho do programa
                case 100:
                    AppendToCode(".assembly extern mscorlib {}",
                        ".assembly _codigo_objeto{}",
                        ".module _codigo_objeto.exe",
                        "",
                        ".class public _UNICA{",
                        ".method static public void _principal(){",
                        ".entrypoint");
                    break;
                // final do programa
                case 101:
                    AppendToCode("ret", "}", "}");
                    break;
                // bool true
                case 118:
                    PilhaTipos.Push("bool");
                    AppendToCode("ldc.i4.1");
                    break;
                // bool false
                case 119:
                    PilhaTipos.Push("bool");
                    AppendToCode("ldc.i4.0");
                    break;
                // constante int
                case 128:
                    PilhaTipos.Push("int64");
                    AppendToCode($"ldc.i8 {token.GetLexeme()}");
                    AppendToCode("conv.r8");
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

    }
}
