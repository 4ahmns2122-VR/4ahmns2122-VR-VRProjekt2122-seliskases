using System.Collections.Generic;

namespace UnityEngine.Chess
{
    public static class Piece
    {
        public const int King = 0;
        public const int Pawn = 1;
        public const int Knight = 2;
        public const int Bishop = 3;
        public const int Rook = 4;
        public const int Queen = 5;

        public const int White = 0;
        public const int Black = 6;

        public static int[] GetLegalSquares(int pieceIndex, int startSquare, Square[] currentSquares)
        {
            List<int> output = new List<int>();
            bool isWhitePiece = pieceIndex < Black;
            int pieceType = (isWhitePiece) ? pieceIndex : pieceIndex - 6;

            switch (pieceType)
            {
                case 0: // King
                    output.AddRange(KingMoves(isWhitePiece, startSquare, currentSquares));
                    break;
                case 1: // Pawn
                    output.AddRange(PawnMoves(isWhitePiece, startSquare, currentSquares));
                    break;
                case 2: // Knight
                    output.AddRange(KnightMoves(isWhitePiece, startSquare, currentSquares));
                    break;
                case 3: // Bishop
                    output.AddRange(SlidingMoves(pieceType, isWhitePiece, startSquare, currentSquares));
                    break;
                case 4: // Rook
                    output.AddRange(SlidingMoves(pieceType, isWhitePiece, startSquare, currentSquares));
                    break;
                case 5: // Queen
                    output.AddRange(SlidingMoves(pieceType, isWhitePiece, startSquare, currentSquares));
                    break;
            }

            return output.ToArray();
        }

        #region Private Methods

        // The following algorithms calculate the quare indexes the current piece can move to. The algorithms account for
        // capturing pieces, double pawn-moves, etc.

        // Source (only the SlidingMoves function!): Sebastian Lague - https://www.youtube.com/watch?v=U4ogK0MIzqk&t=323s
        private static List<int> SlidingMoves(int pieceType, bool isWhitePiece, int startSquare, Square[] currentSquares)
        {
            List<int> output = new List<int>();

            int startDirIndex = (pieceType == Bishop) ? 4 : 0;
            int endDirIndex = (pieceType == Rook) ? 4 : 8;

            for (int directionIndex = startDirIndex; directionIndex < endDirIndex; directionIndex++)
            {
                for (int n = 0; n < PrecomputedMoveData.numSquaresToEdge[startSquare][directionIndex]; n++)
                {
                    int targetSquare = startSquare + PrecomputedMoveData.slidingPieceDirectionOffsets[directionIndex] * (n + 1);
                    bool pieceOnTargetSquareExists = currentSquares[targetSquare].pieceRenderer.sprite != null;
                    int pieceIndexOnTargetSquare = currentSquares[targetSquare].pieceIndex;
                    bool whitePieceOnTargetSquare = pieceIndexOnTargetSquare < Black;

                    if (pieceOnTargetSquareExists && whitePieceOnTargetSquare == isWhitePiece)
                    {
                        break;
                    }

                    output.Add(targetSquare);

                    // If we capture an opponent's piece, we can't move any further
                    if (pieceOnTargetSquareExists && whitePieceOnTargetSquare != isWhitePiece)
                    {
                        break;
                    }
                }
            }

            return output;
        }

        private static List<int> KnightMoves(bool isWhitePiece, int startSquare, Square[] currentSquares)
        {
            List<int> output = new List<int>();
            int currentRank = Mathf.CeilToInt((float)(startSquare + 1) / 8);

            for (int rank = currentRank - 2; rank <= currentRank + 2; rank++)
            {
                int offsetIndex = rank - currentRank + 2;

                for (int offset = 0; offset < PrecomputedMoveData.knightDirectionOffsets[offsetIndex].Length; offset++)
                {
                    int targetSquare = startSquare + PrecomputedMoveData.knightDirectionOffsets[offsetIndex][offset];

                    if (targetSquare < 0 || targetSquare > 63)
                    {
                        continue;
                    }

                    bool pieceOnTargetSquareExists = currentSquares[targetSquare].pieceRenderer.sprite != null;
                    int pieceIndexOnTargetSquare = currentSquares[targetSquare].pieceIndex;
                    bool whitePieceOnTargetSquare = pieceIndexOnTargetSquare < Black;

                    if (pieceOnTargetSquareExists && whitePieceOnTargetSquare == isWhitePiece)
                    {
                        continue;
                    }

                    if (Mathf.CeilToInt((float)(targetSquare + 1) / 8) == rank)
                    {
                        output.Add(targetSquare);
                    }
                }
            }

            return output;
        }

        private static List<int> PawnMoves(bool isWhitePiece, int startSquare, Square[] currentSquares)
        {
            List<int> output = new List<int>();
            int currentRank = Mathf.CeilToInt((float)(startSquare + 1) / 8);
            
            if (isWhitePiece)
            {
                if(currentSquares[startSquare + 8].pieceRenderer.sprite != null)
                {
                    output.Add(startSquare + 8);

                    if (currentRank == 2 && currentSquares[startSquare + 16].pieceRenderer.sprite != null)
                    {
                        output.Add(startSquare + 16);
                    }
                }
            }
            else
            {
                if (currentSquares[startSquare - 8].pieceRenderer.sprite != null)
                {
                    output.Add(startSquare - 8);

                    if (currentRank == 7 && currentSquares[startSquare - 16].pieceRenderer.sprite != null)
                    {
                        output.Add(startSquare - 16);
                    }
                }
            }

            int colorIndex = (isWhitePiece) ? 1 : -1;
            foreach (int offset in PrecomputedMoveData.pawnCaptureOffsets)
            {
                int targetSquare = startSquare + (offset * colorIndex);

                if (targetSquare < 0 || targetSquare > 63)
                {
                    continue;
                }

                bool pieceOnTargetSquareExists = currentSquares[targetSquare].pieceRenderer.sprite != null;
                int pieceIndexOnTargetSquare = currentSquares[targetSquare].pieceIndex;
                bool whitePieceOnTargetSquare = pieceIndexOnTargetSquare < Black;

                if (!pieceOnTargetSquareExists || whitePieceOnTargetSquare == isWhitePiece)
                {
                    continue;
                }

                if (Mathf.CeilToInt((float)(targetSquare + 1) / 8) == currentRank + colorIndex)
                {
                    output.Add(targetSquare);
                }
            }

            return output;
        }

        private static List<int> KingMoves(bool isWhitePiece, int startSquare, Square[] currentSquares)
        {
            List<int> output = new List<int>();
            int currentRank = Mathf.CeilToInt((float)(startSquare + 1) / 8);

            for (int rank = currentRank - 1; rank <= currentRank + 1; rank++)
            {
                int offsetIndex = rank - currentRank + 1;

                for (int offset = 0; offset < PrecomputedMoveData.kingDirectionOffsets[offsetIndex].Length; offset++)
                {
                    int targetSquare = startSquare + PrecomputedMoveData.kingDirectionOffsets[offsetIndex][offset];

                    if (targetSquare < 0 || targetSquare > 63)
                    {
                        continue;
                    }

                    bool pieceOnTargetSquareExists = currentSquares[targetSquare].pieceRenderer.sprite != null;
                    int pieceIndexOnTargetSquare = currentSquares[targetSquare].pieceIndex;
                    bool whitePieceOnTargetSquare = pieceIndexOnTargetSquare < Black;

                    if (pieceOnTargetSquareExists && whitePieceOnTargetSquare == isWhitePiece)
                    {
                        continue;
                    }

                    if (Mathf.CeilToInt((float)(targetSquare + 1) / 8) == rank)
                    {
                        output.Add(targetSquare);
                    }
                }
            }

            return output;
        }

        #endregion
    }
}