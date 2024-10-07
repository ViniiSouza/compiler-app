using AnalyzerUtils;

namespace SemanticAnalyzer
{
    public class Semantico : Constants
    {
        public void ExecuteAction(int action, Token token)
        {
            Console.WriteLine("Ação #"+action+", Token: "+token);
        }
    }
}
