namespace LexicalAnalyzer
{
    internal class Token
    {
        private int id;
        private string lexeme;
        private int position;

        public Token(int id, string lexeme, int position)
        {
            this.id = id;
            this.lexeme = lexeme;
            this.position = position;
        }

        public int GetId()
        {
            return id;
        }

        public string GetLexeme()
        {
            return lexeme;
        }

        public int GetPosition()
        {
            return position;
        }

        public override string ToString()
        {
            return id + " ( " + lexeme + " ) @ " + position;
        }
    }
}
