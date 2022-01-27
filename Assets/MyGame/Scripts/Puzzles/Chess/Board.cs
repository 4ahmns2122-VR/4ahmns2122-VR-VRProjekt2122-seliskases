using System.Collections.Generic;

namespace UnityEngine.Chess
{
    public class Board : MonoBehaviour
    {
        #region Fields

        public GameObject tempSquare;
        public float squareOffset;
        public Sprite[] pieces;
        public Canvas canvas;
        public List<Puzzle> puzzles;
        public float timeToSolve;

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
        private float currentTime;

        #endregion

        #region Events

        public delegate void OnWrongMove();
        public static OnWrongMove wrongMoveDelegate;

        public delegate void OnPuzzleSolved();
        public static OnPuzzleSolved puzzleSolvedDelegate;

        public delegate void OnPuzzleFinished();
        public static OnPuzzleFinished puzzleFinishedDelegate;

        #endregion

        #region Private Methods

        private void Start()
        {
            UserInterfaceManager.instance.SetToGazeHand();

            UserInterfaceManager.instance.timer.gameObject.SetActive(true);
            currentTime = timeToSolve;

            CreateGraphicalBoard();
            LoadPuzzle(puzzles[currentPuzzleIndex]);
            currentMoves = new List<Move>(puzzles[0].moves);

            wrongMoveDelegate += WrongMove;
            puzzleSolvedDelegate += PuzzleSolved;
        }

        private void Update()
        {
            currentTime -= Time.deltaTime;
            UserInterfaceManager.instance.cheatText.text = currentMoves?[0].startSquare + "-" + currentMoves?[0].targetSquare;

            Color color = Color.white;

            if (currentTime > 40)
            {
                color = Color.green;
            }
            else if (currentTime > 20)
            {
                color = Color.yellow;
            }
            else if (currentTime > 0)
            {
                color = Color.red;
            }
            else
            {
                UserInterfaceManager.instance.DisplayRestartPanel("You lost!");
            }

            UserInterfaceManager.instance.SetTimer(currentTime, color);
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
                    Vector3 squarePosition = new Vector3(gameObject.transform.position.x + file * tempSquare.transform.localScale.x, gameObject.transform.position.y + rank * tempSquare.transform.localScale.y, gameObject.transform.position.z);

                    DrawSquare(standardSquareColor, highlightedTargetSquareColor, highlightedStartColor, squarePosition, rank * 8 + file);
                }
            }
        }

        private void DrawSquare(Color standardColor, Color highlightedTargetColor, Color highlightedStartColor, Vector3 position, int index)
        {
            GameObject squareObject = Instantiate(tempSquare);
            squareObject.transform.parent = canvas.transform;
            squareObject.transform.position = position;

            Square squareLogic = squareObject.GetComponent<Square>();
            squareLogic.Initialize(index, standardColor, highlightedTargetColor, highlightedStartColor);
            squares[index] = squareLogic;
        }

        // Source: Sebastian Lague - https://www.youtube.com/watch?v=U4ogK0MIzqk&t=323s
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

            foreach (var square in squares)
            {
                square.Reset();
            }

            LoadPuzzle(puzzles[currentPuzzleIndex]);
            currentMoves = new List<Move>(puzzles[currentPuzzleIndex].moves);
        }

        private void PuzzleSolved()
        {
            currentTime = timeToSolve;

            foreach (var square in squares)
            {
                square.Reset();
            }

            currentPuzzleIndex++;

            if (currentPuzzleIndex >= puzzles.Count)
            {
                print("Every puzzle solved!");
                puzzleFinishedDelegate?.Invoke();

                return;
            }

            LoadPuzzle(puzzles[currentPuzzleIndex]);
            currentMoves = new List<Move>(puzzles[currentPuzzleIndex].moves);
        }

        #endregion
    }
}
