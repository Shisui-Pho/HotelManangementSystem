using System.Collections.Generic;
namespace HotelManangementSystemLibrary.Utilities.Extensions
{
    public static class Extensions
    {
        public static List<T2> ToList<T1,T2>(this SortedList<T1,T2> sortedlist)
        {
            List<T2> lst = new List<T2>();
            foreach (T2 item in sortedlist.Values)
            {
                lst.Add(item);
            }
            return lst;
        }
    }
}
