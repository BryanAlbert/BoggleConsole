using System;
using System.Collections.Generic;
using System.Linq;

namespace BoggleConsole
{
	internal class Program
	{
		public static int Main(string[] args)
		{
			if (!ProcessCommandLine(args.ToList()))
			{
				Usage();
				return -1;
			}

			string board;
			m_board = new Board4x4
			{
				EdgeLength = EdgeLength,
				MinimumWordSize = MinWordSize,
				Seed = Seed,
				Print = VerboseOutput
			};

			try
			{
				board = m_board.GenerateBoard();
				Console.WriteLine();
				m_board.PrintBoard(board);
				Solver solver = new Solver
				{
					Board = board,
					Step = Step,
					Verbose = VerboseOutput
				};

				solver.Solve();
				Console.WriteLine("Words:");
				int words = 1;
				for (int word = 0; word < solver.WordList.Count; word++)
				{
					if (words++ % 10 == 0 || word == solver.WordList.Count - 1)
						Console.WriteLine($"{solver.WordList[word]}");
					else
						Console.Write($"{solver.WordList[word]}, ");
				}
			}
			catch (NotImplementedException exception)
			{
				Console.WriteLine($"\nNotImplementedException: {exception.Message}\n");
				return -2;
			}

			Console.WriteLine("\nHit a key to exit.");
			Console.ReadKey(intercept: true);
			return 0;
		}

		public static bool VerboseOutput { get; private set; }
		public static bool Step { get; private set; }
		public static int? Seed { get; private set; }
		public static int EdgeLength { get; private set; } = 4;
		public static int MinWordSize { get; private set; } = 3;

		private static void Usage()
		{
			Console.WriteLine("\nUsage: [-?|-help][-Verbose][-Step][-Seed seed][-Big size][-MinWordSize min]");
			Console.WriteLine("-help           display this usage message");
			Console.WriteLine("-Verbose        show output generating board");
			Console.WriteLine("-Step           pause when checking each letter");
			Console.WriteLine("-Seed           int, seed the random number generator with seed");
			Console.WriteLine("-Big            int, set the edge length of the board to size, e.g. 4 (Boggle), 5 (Big Boggle), 6 (Super Big Boggle)");
			Console.WriteLine("-MinWordSize    int, set the minimum word size to min, 3 by default");
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
						case "verbose":
							VerboseOutput = true;
							break;
						case "step":
							Step = true;
							VerboseOutput = true;
							break;
						case "seed":
							if (ParseInt(args, out int seed, "\nError: -Seed switch must be followed by an integer",
								"\nError: -Seed switch must be followed by an integer, could not parse: {0}"))
							{
								Seed = seed;
								break;
							}

							return false;
						case "big":
							if (ParseInt(args, out int edgeData, "\nError: -Big switch must be followed by an integer",
								"\nError: -Big switch must be followed by an integer, could not parse: {0}"))
							{
								EdgeLength = edgeData;
								break;
							}

							return false;
						case "minwordsize":
							if (ParseInt(args, out int minWordSize, "\nError: -MinWordSize switch must be followed by an integer",
								"\nError: -MinWordSize switch must be followed by an integer, could not parse: {0}"))
							{
								MinWordSize = minWordSize;
								break;
							}

							Console.WriteLine("\nNote: MinWordSize is not yet implemented.\n");
							return false;
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


		private static Board4x4 m_board;
	}
}
