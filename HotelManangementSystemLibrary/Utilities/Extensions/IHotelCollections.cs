using System.Collections.Generic;
using System.Text;
using System.IO;
namespace HotelManangementSystemLibrary.Utilities.Extensions
{
    public static class IHotelCollections
    {
        public static void SaveData<T>(this ICollectionHotel<T> data,string file) 
            where T : IHotelModel
        {
            StringBuilder bl = new StringBuilder();
            foreach (T item in data)
                bl.AppendLine(item.ToCSVFormat());

            File.WriteAllText(file, bl.ToString());
        }//SaveData
    }//class
}//namespace
