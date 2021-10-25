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

namespace Muazun.NamazTime
{
    // Time Formats
    public enum TimeFormat
    {
        Time24 = 0,    // 24-hour format
        Time12 = 1,    // 12-hour format
        Time12NS = 2,    // 12-hour format with no suffix
        Floating = 3,    // floating point number
    }
}
