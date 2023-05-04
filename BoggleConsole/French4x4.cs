using System.Collections.Generic;

namespace BoggleConsole
{
	internal class French4x4 : BoggleBoard
	{
		public French4x4()
		{
			GameName = m_gameName;
			EdgeLength = 4;
			MinimumWordSize = 3;
			Scoring = new List<int> { 0, 0, 0, 1, 2, 3, 5, 11 };
			Cubes = new List<string>
			{
				"AAEIOT", "AIMORS", "EGNLUY", "ABILTR",
				"ACDEMP", "EGINTV", "EILRUW", "ELPSTU",
				"DENOST", "ACELRS", "ABJMO1", "EEFHIS",
				"EHINRS", "EKNOTU", "ADENVZ", "AIFORX"
			};
		}


		public const string m_gameName = "Boggle French";
	}
}
