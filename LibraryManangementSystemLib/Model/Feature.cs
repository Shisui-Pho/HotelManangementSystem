namespace HotelManangementSystemLibrary
{
    public class Feature : IFeature 
    {
        private static int count = 1;

        public event delOnPropertyChanged PropertyChangedEvent;

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
        internal void SetID(string id) => FeatureID = id;
        public string ToCSVFormat()
        {
            return FeatureID + "," + FeatureName + "," + Description + "," + Price.ToString("0.00");
        }//ToCSVFormat

        public int CompareTo(object obj)
        {
            return this.FeatureID.CompareTo(((IFeature)obj).FeatureID);
        }//CompareTo
        public override string ToString()
        {
            return FeatureName;
        }//ToString()

        public void ChangePrice(decimal newprice)
        {
            //Business rule apply
            if (newprice < 0)
                return;
            this.Price = newprice;
            //Invoke the propert changed event
            PropertyChangedEvent?.Invoke(this.FeatureID, "Price", newprice.ToString());
        }//ChangePrice

        public void ChangeDescription(string desc)
        {
            this.Description = desc;
            //Invoke the propert changed event
            PropertyChangedEvent?.Invoke(this.FeatureID, "Desc", desc);
        }//ChangeDescription

        public void ChangeName(string newname)
        {
            this.FeatureName = newname;
            //Invoke the propert changed event
            PropertyChangedEvent?.Invoke(this.FeatureID, "FeatureName", newname);
        }//ChangeName
    }//end class
}//
