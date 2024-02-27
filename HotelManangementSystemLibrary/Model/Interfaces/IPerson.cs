using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public interface IPerson
    {
        string Name { get;}
        string Surname { get;}
        DateTime DOB { get;}
        int Age { get; }
    }//IPerson
}//namespace
