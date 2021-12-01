using static System.Math;

namespace UnityEngine.Chess
{
	public static class PrecomputedMoveData
	{
		public static readonly int[][] numSquaresToEdge;
		public static readonly int[][] knightDirectionOffsets;
		public static readonly int[][] kingDirectionOffsets;
		public static readonly int[] pawnCaptureOffsets = { 7, 9 };
		public static readonly int[] slidingPieceDirectionOffsets = { 8, -8, -1, 1, 7, -7, 9, -9 };

		static PrecomputedMoveData()
		{
			knightDirectionOffsets = new int[5][];
			knightDirectionOffsets[0] = new int[] { -17, -15 };
			knightDirectionOffsets[1] = new int[] { -10, -6 };
			knightDirectionOffsets[2] = new int[] { };
			knightDirectionOffsets[3] = new int[] { 6, 10 };
			knightDirectionOffsets[4] = new int[] { 15, 17 };

			kingDirectionOffsets = new int[3][];
			kingDirectionOffsets[0] = new int[] { -7, -8, -9 };
			kingDirectionOffsets[1] = new int[] { -1, 1 };
			kingDirectionOffsets[2] = new int[] { 7, 8, 9 };

			numSquaresToEdge = new int[64][];

			for (int squareIndex = 0; squareIndex < 64; squareIndex++)
			{
				int y = squareIndex / 8;
				int x = squareIndex - y * 8;

				int north = 7 - y;
				int south = y;
				int west = x;
				int east = 7 - x;

				numSquaresToEdge[squareIndex] = new int[8];
				numSquaresToEdge[squareIndex][0] = north;
				numSquaresToEdge[squareIndex][1] = south;
				numSquaresToEdge[squareIndex][2] = west;
				numSquaresToEdge[squareIndex][3] = east;
				numSquaresToEdge[squareIndex][4] = Min(north, west);
				numSquaresToEdge[squareIndex][5] = Min(south, east);
				numSquaresToEdge[squareIndex][6] = Min(north, east);
				numSquaresToEdge[squareIndex][7] = Min(south, west);
			}
		}
	}
}
