using System.Collections.Generic;

namespace BoggleConsole
{
	public class Dutch4x4 : BoggleBoard
	{
		public Dutch4x4()
		{
			GameName = m_gameName;
			EdgeLength = 4;
			MinimumWordSize = 3;
			Scoring = new List<int> { 0, 0, 1, 1, 2, 3, 5, 11 };
			Cubes = new List<string>
			{
				"AAEIOW", "AIMORS", "EGNJUY", "ABILTN",
				"ACDEMP", "EGINTV", "GELRUW", "ELPNTU",
				"DENOST", "ACEHRS", "DBJMO1", "EEFHIS",
				"EHINRS", "EKNOTZ", "ADENVZ", "AIFKRX"
			};
		}


		public const string m_gameName = "Boggle Dutch";
	}
}
