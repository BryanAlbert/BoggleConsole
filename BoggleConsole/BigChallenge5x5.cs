using System.Collections.Generic;

namespace BoggleConsole
{
	public class BigChallenge5x5 : BoggleBoard
	{
		public BigChallenge5x5()
		{
			GameName = m_gameName;
			EdgeLength = 5;
			MinWordSize = 4;
			Scoring = new List<int> { 0, 0, 0, 1, 2, 3, 5, 11 };
			Cubes = new List<string>
			{
				"AAAFRS", "AAEEEE", "AAFIRS", "ADENNN", "AEEEEM",
				"AEEGMU", "AEGMNN", "AFIRSY", "BJK1XZ", "CCENST",
				"CEIILT", "CEIPST", "DDHNOT", "DHHLOR", "IKLM1U",
				"DHLNOR", "EIIITT", "CEILPT", "EMOTTT", "ENSSSU",
				"FIPRSY", "GORRVW", "IPRRRY", "NOOTUW", "OOOTTU"
			};
		}


		public const string m_gameName = "Big Boggle Challenge";
	}
}
