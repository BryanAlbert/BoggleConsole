using System.Collections.Generic;

namespace BoggleConsole
{
	public class German4x4 : BoggleBoard
	{
		public German4x4()
		{
			GameName = m_gameName;
			EdgeLength = 4;
			MinimumWordSize = 3;
			Scoring = new List<int> { 0, 0, 0, 1, 2, 3, 5, 11 };
			Cubes = new List<string>
			{
				"PTESUL", "ENTVIG", "PEDCAM", "RESCAL",
				"VANZED", "RILWEU", "FEESIH", "TONKEU",
				"RESNIH", "TAAEIO", "ENTSOD", "BO1JAM",
				"ROSMAI", "YUNGLE", "FOXRAI", "BARTIL"
			};
		}


		public const string m_gameName = "Boggle German";
	}
}
