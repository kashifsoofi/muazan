//--------------------- Copyright Block ----------------------
/*

PrayTime.cs: Prayer Times Calculator (ver 1.2)
Copyright (C) 2007-2010 PrayTimes.org

C# Code By: Jandost Khoso
Original JS Code By: Hamid Zarrabi-Zadeh

License: GNU LGPL v3.0

TERMS OF USE:
	Permission is granted to use this code, with or
	without modification, in any website or application
	provided that credit is given to the original work
	with a link back to PrayTimes.org.

This program is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY.

PLEASE DO NOT REMOVE THIS COPYRIGHT BLOCK.

*/

namespace Muazun
{
	// Calculation Methods
	public enum CalculationMethod
	{
		Jafari = 0,    // Ithna Ashari
		Karachi = 1,    // University of Islamic Sciences, Karachi
		ISNA = 2,    // Islamic Society of North America (ISNA)
		MWL = 3,    // Muslim World League (MWL)
		Makkah = 4,    // Umm al-Qura, Makkah
		Egypt = 5,    // Egyptian General Authority of Survey
		Custom = 6,    // Custom Setting
		Tehran = 7,    // Institute of Geophysics, University of Tehran
	}
}
