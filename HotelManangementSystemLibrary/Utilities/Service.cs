using System;
using System.Collections.Generic;
using System.Text;

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
    }//class
}//namespace
