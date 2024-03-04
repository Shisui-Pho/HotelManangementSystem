﻿using System;
using System.Collections.Generic;
using System.Text;

namespace UIServiceLibrary.Evaluations
{
    public static class Extensions
    {
        private static readonly string[] unwantedValues =
            { "PASSWORD", "USERNAME","NAMES","SURNAME","EMAIL","PHONE NUMBER"};
        public static bool IsValidInput(this string input)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
                return false;
            if (input.Length == 1)
                return false;
            if (IsStringUnWanted(input))
                return false;
            return true;
        }//IsValidInput
        private static bool IsStringUnWanted(string s)
        {
            s = s.ToUpper();
            for (int i = 0; i < unwantedValues.Length; i++)
            {
                if (s == unwantedValues[i])
                    return true;
            }
            return false;
        }//CheckForUNWanteChars
        public static bool IsValidPhoneNumber(this string number)
        {
            //Using South African phone numbers
            if (number.Length == 12 && number.StartsWith("+27"))
                return true;
            if (number.Length == 10 && number.StartsWith("0"))
                return true;
            return false;
        }//IsValidPhoneNumber

    }//class
}//namespace
