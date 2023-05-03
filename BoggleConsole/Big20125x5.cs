using System.Collections.Generic;

namespace BoggleConsole
{
	public class Big20125x5 : BoggleBoard
	{
		public Big20125x5()
		{
			GameName = m_gameName;
			EdgeLength = 5;
			MinimumWordSize = 4;
			Scoring = new List<int> { 0, 0, 0, 1, 2, 3, 5, 11 };
			Cubes = new List<string>
			{
				"AAAFRS", "AAEEEE", "AAFIRS", "ADENNN", "AEEEEM",
				"AEEGMU", "AEGMNN", "AFIRSY", "BBJKXZ", "CCENST",
				"EIILST", "CEIPST", "DDHNOT", "DHHLOR", "DHHNOW",
				"DHLNOR", "EIIITT", "EILPST", "EMOTTT", "ENSSSU",
				"123456", "GORRVW", "IPRSYY", "NOOTUW", "OOOTTU"
			};
		}


		public const string m_gameName = "Big Boggle 2012";
	}
}
