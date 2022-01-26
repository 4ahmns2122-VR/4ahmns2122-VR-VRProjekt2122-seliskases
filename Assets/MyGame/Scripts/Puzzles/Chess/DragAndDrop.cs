using System.Collections;

namespace UnityEngine.Chess
{
    public class DragAndDrop : MonoBehaviour
    {
        public SpriteRenderer pieceRenderer;
        public float opponentAnimationSpeed;
        public AnimationCurve opponentAnimationCurve;

        private int currentPieceIndex;
        private int[] currentLegalSquares = new int[0];
        private int currentStartSquare;

        public AudioClip moveSFX;
        public AudioClip errorSFX;
        public AudioClip solvedPuzzleSFX;

        public static DragAndDrop instance;

        private void Start()
        {
            instance = this;
        }

        public IEnumerator DragPiece(Sprite piece, int pieceIndex, int startSquare, int[] legalSquares)
        {
            currentLegalSquares = legalSquares;
            currentPieceIndex = pieceIndex;
            currentStartSquare = startSquare;

            YieldInstruction instruction = new WaitForEndOfFrame();
            pieceRenderer.sprite = piece;

            while (true)
            {
                Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                Vector2 objectPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                transform.position = objectPosition;

                yield return instruction;
            }
        }

        public bool RequestPiecePlacement(Square targetSquare)
        {
            foreach (int legalSquare in currentLegalSquares)
            {
                if (legalSquare == targetSquare.squareIndex)
                {
                    //Check, whether the move has been correct
                    Move correctMove = Board.currentMoves[0];
                    bool correctStartSquare = correctMove.GetStartSquareIndex() == currentStartSquare;
                    bool correctTargetSquare = correctMove.GetTargetSquareIndex() == targetSquare.squareIndex;

                    StopAllCoroutines();
                    targetSquare.SetPiece(pieceRenderer.sprite, currentPieceIndex);
                    GetComponent<AudioSource>().PlayOneShot(moveSFX);
                    currentLegalSquares = new int[0];
                    pieceRenderer.sprite = null;

                    if (!correctStartSquare || !correctTargetSquare)
                    {
                        GetComponent<AudioSource>().PlayOneShot(errorSFX);
                        Board.wrongMoveDelegate();
                        return true;
                    }

                    Board.currentMoves.RemoveAt(0);

                    //Animate next opponent's move. If there is no next move, the player has won
                    if (Board.currentMoves.Count > 0)
                    {
                        Move nextMove = Board.currentMoves[0];
                        SpriteRenderer pieceToMoveRenderer = Board.squares[nextMove.GetStartSquareIndex()].pieceRenderer;
                        Sprite pieceToMove = pieceToMoveRenderer.sprite;
                        pieceToMoveRenderer.sprite = null;
                        int pieceIndex = Board.squares[nextMove.GetStartSquareIndex()].pieceIndex;
                        StartCoroutine(AnimateOpponentsMove(nextMove, pieceToMove, pieceIndex));
                    }
                    else
                    {
                        GetComponent<AudioSource>().PlayOneShot(solvedPuzzleSFX);
                        Board.puzzleSolvedDelegate();
                    }

                    return true;
                }
            }

            return false;
        }

        private IEnumerator AnimateOpponentsMove(Move move, Sprite piece, int pieceIndex)
        {
            pieceRenderer.sprite = piece;
            YieldInstruction instruction = new WaitForEndOfFrame();

            Vector2 tempOrigin = Board.squares[move.GetStartSquareIndex()].transform.position;
            Vector2 origin = tempOrigin + Board.squares[move.GetStartSquareIndex()].GetComponent<BoxCollider2D>().offset;

            Vector2 tempDestination = Board.squares[move.GetTargetSquareIndex()].transform.position;
            Vector2 destination = tempDestination + Board.squares[move.GetTargetSquareIndex()].GetComponent<BoxCollider2D>().offset;

            float duration = Vector2.Distance(origin, destination) / opponentAnimationSpeed;
            Vector2 currentPos;

            float currentLerpTime = 0f;
            float clampLerpTime;

            while (true)
            {
                currentLerpTime += Time.deltaTime;
                if (currentLerpTime > duration)
                {
                    Board.squares[move.GetTargetSquareIndex()].SetPiece(piece, pieceIndex);
                    Board.currentMoves.RemoveAt(0);
                    Board.whiteToMove = !Board.whiteToMove;

                    GetComponent<AudioSource>().PlayOneShot(moveSFX);
                    pieceRenderer.sprite = null;
                    break;
                }

                clampLerpTime = Mathf.Clamp01(currentLerpTime / duration);
                currentPos = Vector2.Lerp(origin, destination, opponentAnimationCurve.Evaluate(clampLerpTime));

                transform.position = currentPos;
                yield return instruction;
            }
        }
    }
}

