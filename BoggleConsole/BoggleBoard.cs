using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BoggleConsole
{
	public class BoggleBoard
	{
		public static BoggleBoard LoadBoardFromFile(string boardFileName)
		{
			string letters = string.Empty;
			foreach (string line in File.ReadAllLines(boardFileName).ToList())
				foreach (string letter in line.Split(' '))
					letters += letter;

			double edgeLength = Math.Sqrt(letters.Length);
			if ((int) edgeLength != edgeLength)
			{
				Console.WriteLine($"\nError: non-square number of letters from {boardFileName}, read {letters.Length}");
				throw new NotImplementedException("Only square Boggle boards are supported at this time.");
			}

			BoggleBoard board = new BoggleBoard()
			{
				EdgeLength = (int) edgeLength,
				Letters = letters
			};

			return board;
		}


		public string GameName { get; set; }
		public int EdgeLength { get => m_edgeLength; set { m_edgeLength = value; CubeCount = value * value; } }
		public int MinimumWordSize { get; set; }
		public int? Seed { get; set; }
		public bool Print { get; set; }
		public int CubeCount { get; private set; }
		public List<string> Cubes { get; set; }
		public List<int> Scoring { get; set; } = new List<int>();
		public string Letters { get; private set; }

		public void GenerateBoard()
		{
			m_random = Seed.HasValue ? new Random(Seed.Value) : new Random();
			Letters = string.Empty;
			List<int> cubes = new List<int>();
			WriteLineIf(Print, $"Generating {GameName} board...");
			for (int index = 0; index < Cubes.Count; index++)
			{
				int cube = m_random.Next(CubeCount);
				while (cubes.Contains(cube % CubeCount))
					cube++;

				cube %= CubeCount;
				cubes.Add(cube);
				int side = m_random.Next(6);
				Letters += Cubes[cube][side];
				if (Print)
				{
					Console.Write($"Cube {PrintNumber(cube + 1)} ({Cubes[cube]}), letter: {Cubes[cube][side]}, ");
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
		}

		public void PrintBoard()
		{
			if (!string.IsNullOrEmpty(GameName))
				Console.WriteLine(GameName);

			for (int row = 0; row < EdgeLength; row++)
			{
				for (int column = 0; column < EdgeLength; column++)
					Console.Write($"{Letters[row * EdgeLength + column]} ");

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


		private Random m_random;
		private int m_edgeLength;
	}
}

