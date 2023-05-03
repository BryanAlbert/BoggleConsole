using System.Collections.Generic;

namespace BoggleConsole
{
	public class BigDeluxe5x5 : BoggleBoard
	{
		public BigDeluxe5x5()
		{
			GameName = m_gameName;
			EdgeLength = 5;
			MinimumWordSize = 4;
			Scoring = new List<int> { 0, 0, 0, 1, 2, 3, 5, 11 };
			Cubes = new List<string>
			{
				"AAAFRS", "AAEEEE", "AAFIRS", "ADENNN", "AEEEEM",
				"AEEGMU", "AEGMNN", "AFIRSY", "BJK1XZ", "CCNSTW",
				"CEIILT", "CEIPST", "DDLNOR", "DHHLOR", "DHHNOT",
				"DHLNOR", "EIIITT", "CEILPT", "EMOTTT", "ENSSSU",
				"FIPRSY", "GORRVW", "HIPRRY", "NOOTUW", "OOOTTU"
			};
		}


		public const string m_gameName = "Big Boggle Deluxe";
	}
}
