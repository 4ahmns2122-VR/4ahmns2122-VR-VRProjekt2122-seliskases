using System;
using System.Text.RegularExpressions;

namespace UnityEngine.Chess
{
    [Serializable]
    public class Move
    {
        public string startSquare;
        public string targetSquare;

        public int GetStartSquareIndex()
        {
            return StandardNotationToSquareIndex(startSquare);
        }

        public int GetTargetSquareIndex()
        {
            return StandardNotationToSquareIndex(targetSquare);
        }

        // Converts the standard Notation to its respective square index (e.g. d1 => 3)
        private int StandardNotationToSquareIndex(string standardNotation)
        {
            string regexExpression = "^[a-h][1-8]$";
            Regex regex = new Regex(regexExpression);

            if (!regex.IsMatch(standardNotation)) return - 1;

            char[] standardNotationArr = standardNotation.ToCharArray();

            // Convert to ASCII
            int file = (int)standardNotationArr[0] - 96;

            int rank = (int)char.GetNumericValue(standardNotationArr[1]);
            return ((rank - 1) * 8 + (file - 1));
        }
    }

}