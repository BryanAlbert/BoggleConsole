using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BoggleConsole
{
	public class BoggleBoard
	{
		public static BoggleBoard LoadBoardFromFile(string boardFileName)
		{
			Console.WriteLine($"Loading board from {boardFileName}");
			string letters = string.Empty;
			foreach (string line in File.ReadAllLines(boardFileName).ToList())
				foreach (string letter in line.Replace("  ", " ").Split(' '))
					letters += SetComboLetter(letter);

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

		private static string SetComboLetter(string letter)
		{
			if (letter.Length == 0)
				return "0";

			if (letter.ToUpper() == "Q")
				return "1";

			string camelCase = letter.Length == 1 ? letter.ToUpper() :
				letter.Substring(0, 1).ToUpper() + letter.Substring(1, 1).ToLower();

			for (int index = 0; index < m_comboLetters.Length; index++)
				if (m_comboLetters[index] == camelCase)
					return index.ToString();

			return letter;
		}

		public static string RenderWord(string word)
		{
			StringBuilder builder = new StringBuilder();
			foreach (char letter in word)
			{
				if (int.TryParse(letter.ToString(), out int number))
					builder.Append(m_comboLetters[number]);
				else
					builder.Append(letter);
			}

			return builder.ToString();
		}

		public static string RenderLetter(char letter, bool padding = false)
		{
			if (int.TryParse(letter.ToString(), out int number))
				return m_comboLetters[number];
			else
				return $"{letter}{(padding ? " " : "")}";
		}


		public static string[] m_comboLetters = { "  ", "Qu", "In", "Th", "Er", "He", "An" };


		public string GameName { get; set; }
		public int EdgeLength { get => m_edgeLength; set { m_edgeLength = value; CubeCount = value * value; } }
		public int MinWordSize { get; set; }
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
					Console.Write($"Cube {PrintNumber(cube + 1)} ({RenderWord(Cubes[cube])})" +
						$" => {RenderLetter(Cubes[cube][side])}, ");

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
					Console.Write($"{RenderLetter(Letters[row * EdgeLength + column], true)} ");

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

