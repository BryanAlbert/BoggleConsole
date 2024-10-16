﻿using System.Collections.Generic;

namespace BoggleConsole
{
	internal class New4x4 : BoggleBoard
	{
		public New4x4()
		{
			GameName = m_gameName;
			EdgeLength = 4;
			MinWordSize = 3;
			Scoring = new List<int> { 0, 0, 0, 1, 2, 3, 5, 11 };
			Cubes = new List<string>
			{
				"AAEEGN", "ELRTTY", "AOOTTW", "ABBJOO",
				"EHRTVW", "CIMOTU", "DISTTY", "EIOSST",
				"DELRVY", "ACHOPS", "HIMN1U", "EEINSU",
				"EEGHNW", "AFFKPS", "HLNNRZ", "DEILRX"
			};
		}


		public const string m_gameName = "Boggle New";
	}
}
