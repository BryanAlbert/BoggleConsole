using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BoggleConsole
{
	public class Solver
	{
		public Solver()
		{
			m_dictionary = File.ReadAllLines(c_dictionaryPath).ToList();
		}


		public List<string> WordList { get; set; } = new List<string>();
		public string Board
		{
			get => m_board;
			set
			{
				m_board = value;
				m_edgeLength = (int) Math.Sqrt(m_board.Length);
				m_boardLength = m_board.Length;
				FilterWordList();
			}
		}

		public int MinWordSize { get; set; }
		public bool Step { get; set; }
		public bool Verbose { get; set; }


		public void Solve()
		{
			if (Step)
				ConsoleWriteLine("\nHit a key after each letter is printed\n");

			Console.WriteLine();
			for (int index = 0; index < m_boardLength; index++)
			{
				string word = $"{Board[index]}";
				int[] path = new int[Board.Length];
				path[index] = 1;
				string render = BoggleBoard.RenderLetter(word[0]);
				if (render == "  ")
					continue;

				ConsoleWriteLine($"Checking from {render} at index {index}:");
				ConsoleWrite(render);
				bool found = FindWordsAt(path, word, index);
				ConsoleEraseLetter(render);
				if (found)
					ConsoleWriteLine();
			}

			ConsoleWriteLine();
			WordList = WordList.OrderBy(x => x.Length).ThenBy(x => x).ToList();
		}


		private void FilterWordList()
		{
			// TODO: make a list of letters in use, remove all words which don't contain these letters
			ConsoleWriteLine("\nNote: FilterWordList not yet implemented.\n");
		}

		private bool FindWordsAt(int[] path, string word, int from)
		{
			bool found = false;
			foreach (Directions direction in Enum.GetValues(typeof(Directions)))
			{
				int index = GetIndexInDirection(path, from, direction);
				if (index == -1)
					continue;

				string letter = BoggleBoard.RenderLetter(m_board[index]);
				if (letter == "  ")
					continue;

				ConsoleWrite(letter);
				string test = word + letter;
				bool? isWord = IsWord(test.Trim());
				if (Step)
					Console.ReadKey(intercept: true);

				if (isWord == false)
				{
					ConsoleEraseLetter(letter);
					continue;
				}

				if (isWord == true && test.Length >= MinWordSize)
				{
					if (!WordList.Contains(test))
						WordList.Add(test);

					ConsoleWrite($" {test}");
					found = true;
				}

				int[] pathCopy = new int[path.Length];
				Array.Copy(path, pathCopy, Board.Length);
				pathCopy[index] = test.Length;
				found = FindWordsAt(pathCopy, test, index) || found;
				ConsoleEraseLetter(letter);
			}

			return found;
		}

		private int GetIndexInDirection(int[] path, int from, Directions direction)
		{
			int index;
			switch (direction)
			{
				case Directions.E:
					index = AtRightEdge(from) ? -1 : from + 1;
					break;
				case Directions.SE:
					index = AtRightEdge(from) || AtBottomEdge(from) ? -1 : from + m_edgeLength + 1;
					break;
				case Directions.S:
					index = AtBottomEdge(from) ? -1 : from + m_edgeLength;
					break;
				case Directions.SW:
					index = AtBottomEdge(from) || AtLeftEdge(from) ? -1 : from + m_edgeLength - 1;
					break;
				case Directions.W:
					index = AtLeftEdge(from) ? -1 : from - 1;
					break;
				case Directions.NW:
					index = AtLeftEdge(from) || AtTopEdge(from) ? -1 : from - m_edgeLength - 1;
					break;
				case Directions.N:
					index = AtTopEdge(from) ? -1 : from - m_edgeLength;
					break;
				case Directions.NE:
					index = AtTopEdge(from) || AtRightEdge(from) ? -1 : from - m_edgeLength + 1;
					break;
				default:
					return -1;
			}

			return index != -1 && path[index] == 0 ? index : -1;
		}

		private bool? IsWord(string word)
		{
			foreach (string test in m_dictionary)
			{
				if (string.Compare(test, word) < 0)
					continue;

				if (test == word)
					return true;

				return (test.Length <= word.Length || test.Substring(0, word.Length) != word) ? false : (bool?) null;
			}

			return false;
		}

		private bool AtRightEdge(int from)
		{
			return (from + 1) % m_edgeLength == 0;
		}

		private bool AtBottomEdge(int from)
		{
			return from > m_boardLength - m_edgeLength - 1;
		}

		private bool AtLeftEdge(int from)
		{
			return (from + m_edgeLength) % m_edgeLength == 0;
		}

		private bool AtTopEdge(int from)
		{
			return from < m_edgeLength;
		}

		private void ConsoleWriteLine(string message = null)
		{
			if (Verbose)
				Console.WriteLine(message ?? string.Empty);
		}

		private void ConsoleWrite(string message)
		{
			if (Verbose)
				Console.Write(message);
		}

		private void ConsoleEraseLetter(string letter)
		{
			if (Verbose)
				ConsoleWrite(letter.Length == 1 ? "\b \b" : "\b\b  \b\b");
		}


		private enum Directions
		{
			E,
			SE,
			S,
			SW,
			W,
			NW,
			N,
			NE
		}


		private const string c_dictionaryPath = "Dictionary.txt";
		private readonly List<string> m_dictionary;
		private string m_board;
		private int m_edgeLength;
		private int m_boardLength;
	}
}
