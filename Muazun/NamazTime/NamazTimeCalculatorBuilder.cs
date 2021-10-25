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
    public class NamazTimeCalculatorBuilder
    {
        private CalculationMethod calculationMethod = CalculationMethod.MWL;
        private JuristicMethod juristicMethod = JuristicMethod.Shafii;
        private TimeFormat timeFormat = TimeFormat.Time24;

        public NamazTimeCalculatorBuilder WithCalculationMethod(CalculationMethod calculationMethod)
        {
            this.calculationMethod = calculationMethod;
            return this;
        }

        public NamazTimeCalculatorBuilder WithAsarMethod(JuristicMethod juristicMethod)
        {
            this.juristicMethod = juristicMethod;
            return this;
        }

        public NamazTimeCalculatorBuilder WithTimeFormat(TimeFormat timeFormat)
        {
            this.timeFormat = timeFormat;
            return this;
        }

        public NamazTimeCalculator Build()
        {
            return new NamazTimeCalculator(
                this.calculationMethod,
                this.juristicMethod,
                this.timeFormat);
        }
    }
}
