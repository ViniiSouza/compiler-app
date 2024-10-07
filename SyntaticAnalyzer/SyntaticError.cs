using AnalyzerUtils;

namespace SyntaticAnalyzer
{
    public class SyntaticError : AnalysisError
    {
        public SyntaticError(String msg, int position) : base(msg, position)
        {}

        public SyntaticError(String msg) : base(msg)
        {}
    }
}
