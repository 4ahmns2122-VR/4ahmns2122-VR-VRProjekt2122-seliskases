using System.Collections.Generic;

namespace UnityEngine.Chess
{
    public class Board : MonoBehaviour
    {
        #region Fields

        public GameObject tempSquare;
        public float squareOffset;
        public Sprite[] pieces;
        public List<Puzzle> puzzles;

        [Header("Color Theme")]
        public Color lightColor;
        public Color lightHighlightedTargetColor;
        public Color darkColor;
        public Color darkHighlightedTargetColor;
        public Color highlightedStartColor;

        #endregion

        #region Static Variables

        public static Square[] squares = new Square[64];
        public static List<Move> currentMoves;
        public static bool whiteToMove;

        #endregion

        #region Private Variables

        private int currentPuzzleIndex = 0;

        #endregion

        #region Events

        public delegate void OnWrongMove();
        public static OnWrongMove wrongMoveDelegate;

        public delegate void OnPuzzleSolved();
        public static OnPuzzleSolved puzzleSolvedDelegate;

        #endregion

        #region Private Methods

        private void Start()
        {
            CreateGraphicalBoard();
            LoadPuzzle(puzzles[currentPuzzleIndex]);
            currentMoves = new List<Move>(puzzles[0].moves);

            wrongMoveDelegate += WrongMove;
            puzzleSolvedDelegate += PuzzleSolved;
        }

        private void CreateGraphicalBoard()
        {
            for (int file = 0; file < 8; file++)
            {
                for (int rank = 0; rank < 8; rank++)
                {
                    bool isLightSquare = (file + rank) % 2 != 0;

                    Color standardSquareColor = (isLightSquare) ? lightColor : darkColor;
                    Color highlightedTargetSquareColor = (isLightSquare) ? lightHighlightedTargetColor : darkHighlightedTargetColor;
                    Vector2 squarePosition = new Vector2(-squareOffset + file, -squareOffset + rank);

                    DrawSquare(standardSquareColor, highlightedTargetSquareColor, highlightedStartColor, squarePosition, rank * 8 + file);
                }
            }
        }

        private void DrawSquare(Color standardColor, Color highlightedTargetColor, Color highlightedStartColor, Vector2 position, int index)
        {
            GameObject squareObject = Instantiate(tempSquare);
            squareObject.transform.position = position;

            Square squareLogic = squareObject.GetComponent<Square>();
            squareLogic.Initialize(index, standardColor, highlightedTargetColor, highlightedStartColor);
            squares[index] = squareLogic;
        }

        private void LoadPuzzle(Puzzle position)
        {
            whiteToMove = position.playerIsWhite;

            var pieceTypeFromSymbol = new Dictionary<char, int>()
            {
                ['k'] = Piece.King,
                ['p'] = Piece.Pawn,
                ['n'] = Piece.Knight,
                ['b'] = Piece.Bishop,
                ['r'] = Piece.Rook,
                ['q'] = Piece.Queen
            };

            int file = 0;
            int rank = 7;

            foreach (char symbol in position.fen)
            {
                if (symbol == '/')
                {
                    file = 0;
                    rank--;
                }
                else
                {
                    if (char.IsDigit(symbol))
                    {
                        file += (int)char.GetNumericValue(symbol);
                    }
                    else
                    {
                        // Uppercase letters represent white pieces,
                        // lowercase letters represent black pieces
                        int pieceColor = (char.IsUpper(symbol)) ? Piece.White : Piece.Black;
                        int pieceType = pieceTypeFromSymbol[char.ToLower(symbol)];
                        int pieceIndex = pieceColor + pieceType;

                        Sprite piece = pieces[pieceIndex];
                        squares[rank * 8 + file].SetPiece(piece, pieceIndex);

                        file++;
                    }
                }
            }
        }

        private void WrongMove()
        {
            print("Wrong Move");

            foreach(var square in squares)
            {
                square.Reset();
            }

            LoadPuzzle(puzzles[currentPuzzleIndex]);
            currentMoves = new List<Move>(puzzles[currentPuzzleIndex].moves);
        }

        private void PuzzleSolved()
        {
            print("Congratulations");


            foreach(var square in squares)
            {
                square.Reset();
            }

            currentPuzzleIndex++;

            if(currentPuzzleIndex >= puzzles.Count)
            {
                print("Every puzzle solved!");
                return;
            }

            LoadPuzzle(puzzles[currentPuzzleIndex]);
            currentMoves = new List<Move>(puzzles[currentPuzzleIndex].moves);
        }

        #endregion
    }
}
