
namespace LexicalAnalyzer
{
    public class Lexico : Constants
    {
        private int position;
        private string input;

        public Lexico() : this("")
        {
        }

        public Lexico(string input)
        {
            SetInput(input);
        }

        public void SetInput(string input)
        {
            this.input = input;
            SetPosition(0);
        }

        public void SetPosition(int position)
        {
            this.position = position;
        }

        public Token NextToken()
        {
            if (!HasInput())
                return null;

            int start = position;

            int state = 0;
            int lastState = 0;
            int endState = -1;
            int end = -1;

            while (HasInput())
            {
                lastState = state;
                state = NextState(NextChar(), state);

                if (state < 0)
                    break;

                if (TokenForState(state) >=0)
                {
                    endState = state;
                    end = position;
                }
            }

            if (endState < 0 || (endState != state && TokenForState(lastState) == -2))
                throw new LexicalError(SCANNER_ERROR[lastState], start);

            position = end;

            int token = TokenForState(endState);

            if (token == 0)
                return NextToken();

            string lexeme = input.Substring(start, end);
            token = LookupToken(token, lexeme);

            return new Token(token, lexeme, start);
        }

        private int NextState(char c, int state)
        {
            int start = SCANNER_TABLE_INDEXES[state];
            int end = SCANNER_TABLE_INDEXES[state + 1] - 1;

            while (start <= end)
            {
                int half = (start+end) / 2;

                if (SCANNER_TABLE[half, 0] == c)
                    return SCANNER_TABLE[half, 1];
                else if (SCANNER_TABLE[half, 0] < c)
                    start = half + 1;
                else
                    end = half - 1;
            }

            return -1;
        }

        private int TokenForState(int state)
        {
            if (state < 0 || state >= TOKEN_STATE.Length)
                return -1;

            return TOKEN_STATE[state];
        }

        public int LookupToken(int standard, string key)
        {
            int start = SPECIAL_CASES_INDEXES[standard];
            int end = SPECIAL_CASES_INDEXES[standard + 1] - 1;

            while (start <= end)
            {
                int half = (start + end) / 2;
                int comp = SPECIAL_CASES_KEYS[half].CompareTo(key);

                if (comp == 0)
                    return SPECIAL_CASES_VALUES[half];
                else if (comp < 0)
                    start = half + 1;
                else
                    end = half - 1;
            }

            return standard;
        }

        private bool HasInput()
        {
            return position < input.Length;
        }

        private char NextChar()
        {
            if (HasInput())
                return input[position++];

            // validate this
            return (char)-1.ToString()[0];
        }
    }
}
