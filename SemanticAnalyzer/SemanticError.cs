using AnalyzerUtils;

namespace SemanticAnalyzer
{
    internal class SemanticError : AnalysisError
    {
        public SemanticError(String msg, int position) : base(msg, position)
        {}

        public SemanticError(String msg) : base(msg)
        {}
    }
}
