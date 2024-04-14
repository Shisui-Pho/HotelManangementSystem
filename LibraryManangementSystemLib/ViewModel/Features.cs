using HotelManangementSystemLibrary.Utilities.Extensions;
using System.Data.OleDb;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManangementSystemLibrary
{
    public class Features : GeneralCollection<IFeature>,IFeatures
    {
        private readonly static Features features = new Features();
        private static string connectionString;
        private Features() : base()
        {
            LoadFeatures();
        }
        ~Features()
        {
            Dispose();
        }
        //For now i will load the features here
        private async void LoadFeatures()
        {
            if(connectionString != "")
            {
                if (features != null)
                    return;
                await Task.Run(() =>
                {
                    using (OleDbConnection con = new OleDbConnection(connectionString))
                    {
                        con.Open();
                        string sql = "SELECT * FROM tbl_Feature";
                        OleDbCommand cmd = new OleDbCommand(sql, con);

                        OleDbDataReader rd = Execute.GetReader(con, cmd);

                        while (rd.Read())
                        {
                            string fname = rd["F_Name"].ToString();
                            string dsc = rd["F_Description"].ToString();
                            decimal price = decimal.Parse(rd["Price"].ToString());
                            Feature fet = new Feature(fname, dsc, price);
                            base.Add(fet);
                        }
                    }//
                });
                return;
            }//
            string[] data =  Service.CheckFilesExistAndLoadTextData("Features.csv");
            if (data.Length <= 0)
                return;
            foreach (var item in data)
            {
                string[] fields = item.Split(',');
                decimal price = Service.GetValueOfMoney(fields[3]);
                Feature fet = new Feature(fields[1], fields[2], price);
                fet.SetID(fields[0]);
                base._collection.Add(fet);
            }//
        }//LoadFeatures
        public static IFeatures GetFeaturesInstance(string _connection = "")
        {
            connectionString = _connection;
            return features;
        }
        public IFeature GetFeature(string id)
            => base._collection.FirstOrDefault(f => f.FeatureID == id);

        public void Dispose()
        {
            ((IGeneralCollection<IFeature>)this).SaveData("Features.csv");
        }//Dispose
    }//interface
}//
