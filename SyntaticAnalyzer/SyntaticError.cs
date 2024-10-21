using AnalyzerUtils;

namespace SyntaticAnalyzer
{
    public class SyntaticError : AnalysisError
    {
        private string found;
        public SyntaticError(String msg, int position) : base(msg, position)
        { }

        public SyntaticError(String msg) : base(msg)
        { }
        public SyntaticError(String msg, int position, string found) : base(msg, position)
        {
            if (found.Equals("$"))
            {
                this.found = "EOF";
            }
            else if (found.StartsWith("\"") && found.EndsWith("\""))
            {
                this.found = "constante string";
            }
            else
            {
                this.found = found;
            }
        }

        public string GetFound()
        {
            return this.found;
        }
    }
}
