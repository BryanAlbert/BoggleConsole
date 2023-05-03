using System;
using System.Collections.Generic;

namespace BoggleConsole
{
	public class Classic4x4 : BoggleBoard
	{
		public Classic4x4()
		{
			GameName = m_gameName;
			EdgeLength = 4;
			MinimumWordSize = 3;
			Scoring = new List<int> { 0, 0, 1, 1, 2, 3, 5, 11 };
			Cubes = new List<string>
			{
				"AEANEG", "AHSPCO", "ASPFFK", "OBJOAB",
				"IOTMUC", "RYVDEL", "LREIXD", "EIUNES",
				"WNGEEH", "LNHNRZ", "TSTIYD", "OWTOAT",
				"ERTTYL", "TOESSI", "TERWHV", "NUIHMQ"
			};
		}


		public const string m_gameName = "Boggle Classic";
	}
}
