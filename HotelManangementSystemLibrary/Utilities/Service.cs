using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;

namespace HotelManangementSystemLibrary
{
    public static class Service
    {
        private readonly static char[] _chars = { ' ', '_' ,'-'};
        public static string GetInitials(string names, string surname)
        {
            string[] _names = names.Split(_chars, StringSplitOptions.RemoveEmptyEntries);
            string initials = string.Empty;
            foreach (string _name in _names)
            {
                initials += _name[0].ToString().ToUpper();
            }
            return initials + ", " + surname.ToUpper();
        }//GetInitials
        public static bool IsEmailCorrect(string _email)
        {
            if (_email.Length <= 2)
                return false;

            int i = _email.IndexOf('@');
            if (i <= 0 || i == (_email.Length - 1))//Email starts with an @ or the '@' is nowhere in the string
            {                               // Or if the '@' is at the end of the email string
                return false;
            }
            return true;
        }//IsEmailCorrect
        public static bool IsCellphoneNumberCorrect(string _number)
        {
            if (_number.Length < 10)
                return false;
            if (_number.StartsWith("+27") && _number.Length < 12)
                return false;
            return true;
        }//IsCellphoneNumberCorrect
        private readonly static string[] _deviders = { "\r\n" };
        public static string[] CheckFilesExistAndLoadTextData(string filename)
        {
            if (!File.Exists(filename))
                return new string[0];

            string _text = File.ReadAllText(filename);
            return _text.Split(_deviders, StringSplitOptions.RemoveEmptyEntries);
        }//CheckFilesExistAndLoadTextData
        public static decimal GetValueOfMoney(string _amount)
        {
            char dec = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
            if (dec == ',' && _amount.IndexOf('.') >= 0)
                _amount = _amount.Replace('.', dec);
            else if (dec == '.' && _amount.IndexOf(',') >= 0)
                _amount = _amount.Replace(',', dec);
            return decimal.Parse(_amount);
        }//GetValueOfMoney
        public static string ToStringMoney(decimal amount)
        {
            string sAmount = amount.ToString("0.00");
            if (sAmount.IndexOf(',') >= 0)
                sAmount = sAmount.Replace(',', '.');
            return sAmount;
        }//ToStringMoney
    }//class
}//namespace
