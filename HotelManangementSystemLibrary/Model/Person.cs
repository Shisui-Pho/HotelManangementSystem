using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public abstract class Person : IPerson
    {
        //data members
        private string _name;
        private string _surname;
        public string Name
        {
            get { return _name; }
            private set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Please enter a valid name!!");
                _name = value;
            }//end setter
        }//Name

        public string Surname
        {
            get { return _surname; }
            private set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Please enter a valid name!!");
                _surname = value;
            }//end setter
        }//Surname

        public DateTime DOB { get; private set; }

        public int Age { get; private set; }

        public Person(string _name, string _surname, DateTime _dob)
        {
            Name = _name;
            Surname = _surname;
            DOB = _dob;
            CalculateAge();
        }//ctor
        private void CalculateAge()
        {
            DateTime today = DateTime.UtcNow;
            int iAge = today.Year - DOB.Year;
            if (DOB.Month > today.Month)
                iAge--;
            else if (DOB.Day > today.Day && DOB.Month == today.Month)
                iAge--;
            Age = iAge;
        }//CalculateAge
        public override string ToString()
        {
            return Service.GetInitials(Name,Surname);
        }//ToString
    }//class
}//namespace
