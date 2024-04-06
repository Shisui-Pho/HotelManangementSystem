using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public class Feature : IFeature 
    {
        private static int count = 1;
        public string FeatureID { get; private set; }
        public string FeatureName { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public Feature(string _fname, string fdesc, decimal price)
        {
            FeatureID = count.ToString();
            FeatureName = _fname;
            Description = fdesc;
            Price = price;
            count++;
        }//Room features

        public string ToCSVFormat()
        {
            return FeatureID + "," + FeatureName + "," + Description + "," + Price.ToString("0.00");
        }//ToCSVFormat

        public int CompareTo(object obj)
        {
            return this.FeatureID.CompareTo(((IFeature)obj).FeatureID);
        }//CompareTo

        public bool Equals(IFeature other)
        {
            return other.FeatureID == this.FeatureID;
        }//Equals
    }//end class
}//
