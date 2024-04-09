using HotelManangementSystemLibrary.Utilities.Extensions;
using System;
using System.Linq;

namespace HotelManangementSystemLibrary
{
    public class Features : GeneralCollection<IFeature>,IFeatures
    {
        private readonly static Features features = new Features();
        private Features() : base()
        {
            LoadFeatures();
        }
        ~Features()
        {
            Dispose();
        }
        //For now i will load the features here
        private void LoadFeatures()
        {
            string[] data =  Service.CheckFilesExistAndLoadTextData("Features.csv");
            if (data.Length <= 0)
            {
                Feature fet = new Feature("Single bed", "Standard : 0.8m x 1.9m", 140m);
                base._collection.Add(fet);
                fet = new Feature("Single bed", "Standard : 0.8m x 2m", 160m);
                base._collection.Add(fet);
                fet = new Feature("Twin Bed", "Two standard sinlge beds: 1.6m each", 280m);
                base._collection.Add(fet);
                fet = new Feature("Double bed", "1 Double bed : 1.2m X 1.9m", 380m);
                base._collection.Add(fet);
                fet = new Feature("King size bed", "1 King Size bed : 2m X 2m", 500m);
                base._collection.Add(fet);

                fet = new Feature("Sofa", "Mod 3 Seater", 200m);
                base._collection.Add(fet);

                fet = new Feature("Sofa", "Mod 2 Seater", 125m);
                base._collection.Add(fet);

                fet = new Feature("Desk", "Standard working desk", 100m);
                base._collection.Add(fet);
                return;
            }
            foreach (var item in data)
            {
                string[] fields = item.Split(',');
                decimal price = Service.GetValueOfMoney(fields[3]);
                Feature fet = new Feature(fields[1], fields[2], price);
                fet.SetID(fields[0]);
                base._collection.Add(fet);
            }//
        }//LoadFeatures
        public static IFeatures GetFeaturesInstance()
            => features;
        public IFeature GetFeature(string id)
            => base._collection.FirstOrDefault(f => f.FeatureID == id);

        public void Dispose()
        {
            ((IGeneralCollection<IFeature>)this).SaveData("Features.csv");
        }//Dispose
    }//interface
}//
