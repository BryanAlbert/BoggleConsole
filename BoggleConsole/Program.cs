using System;
using System.Collections.Generic;
using System.Linq;

namespace BoggleConsole
{
	public class Program
	{
		public static int Main(string[] args)
		{
			LoadGameNames();
			if (!ProcessCommandLine(args.ToList()))
			{
				Usage();
				return -1;
			}

			try
			{
				if (BoardFileName == null)
				{
					if (!LoadBoard())
					{
						Usage();
						return -2;
					}

					Board.GenerateBoard();
				}
				else
				{
					Board = BoggleBoard.LoadBoardFromFile(BoardFileName);
					Board.Scoring = FindScoring();
					Board.MinWordSize = Board.Scoring.Select((x, i) => new { x, i }).First(y => y.x > 0).i + 1;
					Board.Print = VerboseOutput;
				}

				Console.WriteLine();
				Board.PrintBoard();
				Solver solver = new Solver
				{
					Board = Board.Letters,
					MinWordSize = MinWordSize ?? Board.MinWordSize,
					Step = Step,
					Verbose = VerboseOutput
				};

				if (Pause)
				{
					Console.Write("\nHit a key to see the solution...");
					Console.ReadKey(intercept: true);
				}

				DateTime start = DateTime.Now;
				solver.Solve();
				Console.WriteLine($"Time to solve {(int) (DateTime.Now - start).TotalMilliseconds}ms.\nWords (points):");
				int words = 1;
				int totalScore = 0;
				for (int word = 0; word < solver.WordList.Count; word++)
				{
					int wordLength = solver.WordList[word].Length;
					int score = wordLength >= Board.Scoring.Count ? Board.Scoring.Last() : Board.Scoring[wordLength - 1];
					if (score < 0)
						score *= -wordLength;

					totalScore += score;
					if (words++ % 10 == 0 || word == solver.WordList.Count - 1)
						Console.WriteLine($"{solver.WordList[word]} ({score})");
					else
						Console.Write($"{solver.WordList[word]} ({score}), ");
				}

				Console.WriteLine($"Score: {totalScore}");
			}
			catch (NotImplementedException exception)
			{
				Console.WriteLine($"\nNotImplementedException: {exception.Message}\n");
				return -3;
			}
			catch (Exception exception)
			{
				Console.WriteLine($"\nException: {exception.Message}\n");
				return -4;
			}

#if false
			Console.WriteLine("\nHit a key to exit.");
			Console.ReadKey(intercept: true);
#endif
			return 0;
		}


		public static int? GameNameIndex { get; private set; }
		public static int? MinWordSize { get; private set; }
		public static string BoardFileName { get; private set; }
		public static BoggleBoard Board { get; private set; }
		public static bool Pause { get; private set; }
		public static bool VerboseOutput { get; private set; }
		public static bool Step { get; private set; }
		public static int? Seed { get; private set; }

		private static void LoadGameNames()
		{
			m_gameNames.Add(Classic4x4.m_gameName);
			m_gameNames.Add(New4x4.m_gameName);
			m_gameNames.Add(German4x4.m_gameName);
			m_gameNames.Add(French4x4.m_gameName);
			m_gameNames.Add(Dutch4x4.m_gameName);
			m_gameNames.Add(BigClassic5x5.m_gameName);
			m_gameNames.Add(BigChallenge5x5.m_gameName);
			m_gameNames.Add(BigDeluxe5x5.m_gameName);
			m_gameNames.Add(Big20125x5.m_gameName);
			m_gameNames.Add(SuperBig6x6.m_gameName);
		}

		private static bool LoadBoard()
		{
			switch (GameNameIndex)
			{
				case null:
				case 0:
					Board = new Classic4x4();
					break;
				case 1:
					Board = new New4x4();
					break;
				case 2:
					Board = new German4x4();
					break;
				case 3:
					Board = new French4x4();
					break;
				case 4:
					Board = new Dutch4x4();
					break;
				case 5:
					Board = new BigClassic5x5();
					break;
				case 6:
					Board = new BigChallenge5x5();
					break;
				case 7:
					Board = new BigDeluxe5x5();
					break;
				case 8:
					Board = new Big20125x5();
					break;
				case 9:
					Board = new SuperBig6x6();
					break;
				default:
					Console.WriteLine($"Error: {GameNameIndex} is not a valid game number.");
					return false;
			}

			if (MinWordSize.HasValue)
				Board.MinWordSize = MinWordSize.Value;
			
			if (Seed.HasValue)
				Board.Seed = Seed;

			Board.Print = VerboseOutput;
			return true;
		}

		private static List<int> FindScoring()
		{
			switch(Board.EdgeLength)
			{
				case 4:
					return new List<int> { 0, 0, 1, 1, 2, 3, 5, 11 };
				case 5:
					return new List<int> { 0, 0, 0, 1, 2, 3, 5, 11 };
				default:
					return new List<int> { 0, 0, 0, 1, 2, 3, 5, 11, -2 };
			}
		}


		private static void Usage()
		{
			Console.WriteLine("\nUsage: [-?|-help][-Seed seed][-Game numbe][-File filename][-WordSize min][-Pause][-Verbose][-Step]");
			Console.WriteLine("-help           display this usage message");
			Console.WriteLine("-Seed           int, seed the random number generator with seed");
			Console.WriteLine("-Game           the number of the game to play, see below");
			Console.WriteLine("-File           load the board from the file specified by filename");
			Console.WriteLine("-WordSize       int, set the minimum word size to min");
			Console.WriteLine("-Pause          pause before showing the solution");
			Console.WriteLine("-Verbose        show output for generating and solving the game");
			Console.WriteLine("-Step           pause when checking each letter");
			Console.WriteLine("\nBoggle game names:");
			for (int index = 0; index < m_gameNames.Count; index++)
				Console.WriteLine($"{index + 1}: {m_gameNames[index]}");

			Console.WriteLine($"\nIf Game and File aren't specified, {m_gameNames[0]} is played.");
			Console.WriteLine("The board file format uses spaces to deliniate letters, multiple lines are okay, Q represents Qu");
			Console.Write("and these numbers represent letter combinations: 0 = Blank, ");
			int number = 1;
			for (; number < BoggleBoard.m_comboLetters.Length - 1; number++)
				Console.Write($"{number}={BoggleBoard.m_comboLetters[number]}, ");

			Console.WriteLine($"{number}={BoggleBoard.m_comboLetters.Last()}");
		}

		private static bool ProcessCommandLine(List<string> args)
		{
			while (args.Count > 0)
			{
				if (args[0][0] ==  '-')
				{
					switch (args[0].Substring(1).ToLower())
					{
						case "?":
						case "help":
							return false;
						case "seed":
							if (ParseInt(args, out int seed, "\nError: -Seed switch must be followed by an integer",
								"\nError: -Seed switch must be followed by an integer, could not parse: {0}"))
							{
								Seed = seed;
								break;
							}

							return false;
						case "game":
							if (ParseInt(args, out int game, "\nError: -Game switch must be followed by an integer",
								"\nError: -Game switch must be followed by an integer, could not parse: {0}"))
							{
								GameNameIndex = game - 1;
								break;
							}

							return false;
						case "file":
							args.RemoveAt(0);
							if (args.Count == 0)
							{
								Console.WriteLine("\nError: the -Board switch must be followed by a filename");
								return false;
							}

							BoardFileName = args[0];
							break;
						case "wordsize":
							if (ParseInt(args, out int minWordSize, "\nError: -MinWordSize switch must be followed by an integer",
								"\nError: -MinWordSize switch must be followed by an integer, could not parse: {0}"))
							{
								MinWordSize = minWordSize;
								Console.WriteLine("\nNote: MinWordSize is not yet implemented.\n");
								break;
							}

							return false;
						case "pause":
							Pause = true;
							break;
						case "verbose":
							VerboseOutput = true;
							break;
						case "step":
							Step = true;
							VerboseOutput = true;
							break;
						default:
							Console.WriteLine($"\nError: unrecognized command line switch: {args[0]}");
							return false;
					}

					args.RemoveAt(0);
				}
			}

			return true;
		}

		private static bool ParseInt(List<string> args, out int data, string missingMessage, string parseFailMessage)
		{
			args.RemoveAt(0);
			if (args.Count == 0 || !int.TryParse(args[0], out data))
			{
				if (args.Count == 0)
					Console.WriteLine(missingMessage);
				else
					Console.WriteLine(string.Format(parseFailMessage, args[0]));

				data = 0;
				return false;
			}

			return true;
		}

		private static readonly List<string> m_gameNames = new List<string>();
	}
}
