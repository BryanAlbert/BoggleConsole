using System.Collections.Generic;

namespace BoggleConsole
{
	public class SuperBig6x6 : BoggleBoard
	{
		public SuperBig6x6()
		{
			GameName = m_gameName;
			EdgeLength = 6;
			MinimumWordSize = 4;
			Scoring = new List<int> { 0, 0, 0, 1, 2, 3, 5, 11, -2 };
			Cubes = new List<string>
			{
				"AAAFRS", "AAEEEE", "AAEEOO", "AAFIRS", "ABDEIO", "ADENNN",
				"AEEEEM", "AEEGMU", "AEGMNN", "AEILMN", "AEINOU", "AFIRSY",
				"123456", "BBJKXZ", "CCENST", "CDDLNN", "CEIITT", "CEIPST",
				"CFGNUY", "DDHNOT", "DHHLOR", "DHHNOW", "DHLNOR", "EHILRS",
				"EIILST", "EILPST", "EIO000", "EMTTTO", "ENSSSU", "GORRVW",
				"HIRSTV", "HOPRST", "IPRSYY", "JK1WXZ", "NOOTUW", "OOOTTU"
			};
		}


		public const string m_gameName = "Super Big Boggle";
	}
}
