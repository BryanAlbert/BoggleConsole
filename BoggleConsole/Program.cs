using System;

namespace BoggleConsole
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			string board = m_board.GenerateBoard(print: true);
			m_board.PrintBoard(board);

			Console.WriteLine("\nHit a key to exit.");
			Console.ReadKey(intercept: true);
		}


		private static readonly Board4x4 m_board = new Board4x4();
	}
}
