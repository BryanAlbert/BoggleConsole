using System;
using System.Collections.Generic;
using System.Linq;

namespace BoggleConsole
{
	public class Board4x4
	{
		public int CubeNumber { get; set; }
		public int EdgeLength { get => m_edgeLength; set { m_edgeLength = value; CubeCount = value * value; } }
		public int MinimumWordSize { get; set; }
		public int? Seed { get; set; }
		public bool Print { get; set; }
		public int CubeCount { get; private set; }


		public string GenerateBoard()
		{
			if (EdgeLength != 4)
				throw new NotImplementedException("Only 4x4 Boggle is supported at this time.");

			m_random = Seed.HasValue ? new Random(Seed.Value) : new Random();
			string board = string.Empty;
			List<int> cubes = new List<int>();
			WriteLineIf(Print, "Generating board...");
			for (int index = 0; index < m_cubes.Length; index++)
			{
				int cube = m_random.Next(m_cubeCount);
				while (cubes.Contains(cube % m_cubeCount))
					cube++;

				cube %= m_cubeCount;
				cubes.Add(cube);
				int side = m_random.Next(6);
				board += m_cubes[cube][side];
				if (Print)
				{
					Console.Write($"Cube {PrintNumber(cube + 1)} ({m_cubes[cube]}), letter: {m_cubes[cube][side]}, ");
					if ((index + 1) % EdgeLength == 0)
						Console.WriteLine();
				}
			}

			if (Print)
			{
				Console.Write($"Cube order: ");
				foreach (int cube in cubes.Take(cubes.Count - 1))
					Console.Write($"{cube + 1}, ");

				Console.WriteLine($"{cubes.Last() + 1}");
			}

			return board;
		}

		public void PrintBoard(string board)
		{
			for (int row = 0; row < EdgeLength; row++)
			{
				for (int column = 0; column < EdgeLength; column++)
					Console.Write($"{board[row * EdgeLength + column]} ");

				Console.WriteLine();
			}
		}


		private string PrintNumber(int number)
		{
			return $"{(number > 9 ? $"{number}" : $" {number}")}";
		}

		private void WriteLineIf(bool print, string line)
		{
			if (print)
				Console.WriteLine(line);
		}


		private readonly int m_cubeCount = 16;
		private Random m_random;
		private int m_edgeLength = 4;
		private readonly string[] m_cubes = {
			"AEANEG", "AHSPCO", "ASPFFK", "OBJOAB",
			"IOTMUC", "RYVDEL", "LREIXD", "EIUNES",
			"WNGEEH", "LNHNRZ", "TSTIYD", "OWTOAT",
			"ERTTYL", "TOESSI", "TERWHV", "NUIHMQ"
		};
	}
}

