﻿namespace AnalyzerUtils
{
    public class AnalysisError : Exception
    {
        private int position;
        public AnalysisError(string msg, int position) : base(msg)
        {
            this.position = position;
        }

        public AnalysisError(string msg) : base(msg)
        {
            this.position = -1;
        }

        public int GetPosition()
        {
            return position;
        }

        public override string ToString()
        {
            return base.ToString() + ", @ "+ position;
        }
    }
}
