
namespace UnityEngine.Chess
{
    public class Square : MonoBehaviour
    {
        public int squareIndex { get; private set; }
        public Color standardSquareColor { get; private set; }
        public Color highlightedTargetSquareColor { get; private set; }
        public Color highlightedStartSquareColor { get; set; }
        public int pieceIndex { get; private set; }

        public SpriteRenderer pieceRenderer;
        public SpriteRenderer backgroundRenderer;

        private const float pieceScale = 0.06f;

        public void Initialize(int index, Color standardColor, Color targetSquareColor, Color startSquareColor)
        {
            squareIndex = index;
            standardSquareColor = standardColor;
            highlightedTargetSquareColor = targetSquareColor;
            highlightedStartSquareColor = startSquareColor;
            backgroundRenderer.color = standardColor;
            pieceRenderer.transform.localScale = new Vector3(pieceScale, pieceScale, pieceScale);
        }

        public void Reset()
        {
            pieceRenderer.sprite = null;
            pieceIndex = 0;
        }

        public void SetPiece(Sprite piece, int index)
        {
            pieceRenderer.sprite = piece;
            pieceIndex = index;
        }

        // This function is triggered by the XR Raycast Event which is assigned in the inspector
        public void OnRaycast()
        {
            int[] legalSquares = Piece.GetLegalSquares(pieceIndex, squareIndex, Board.squares);
            if (legalSquares.Length == 0) return;

            if (DragAndDrop.instance.RequestPiecePlacement(this))
            {
                foreach (var square in Board.squares)
                {
                    square.backgroundRenderer.color = square.standardSquareColor;
                }

                return;
            }

            if (pieceRenderer.sprite == null)
                return;

            if (DragAndDrop.instance.pieceRenderer.sprite != null)
                return;

            if (Board.whiteToMove != (pieceIndex < Piece.Black) ? true : false)
                return;

            backgroundRenderer.color = highlightedStartSquareColor;

            foreach (var legalSquareIndex in legalSquares)
            {
                foreach (var square in Board.squares)
                {
                    if (square.squareIndex == legalSquareIndex)
                        square.backgroundRenderer.color = square.highlightedTargetSquareColor;
                }
            }

            StartCoroutine(DragAndDrop.instance.DragPiece(pieceRenderer.sprite, pieceIndex, squareIndex, legalSquares));
            pieceRenderer.sprite = null;
            Board.whiteToMove = !Board.whiteToMove;
        }
    }
}
