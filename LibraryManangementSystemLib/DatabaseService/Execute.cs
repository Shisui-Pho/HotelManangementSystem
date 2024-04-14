using System;
using System.Data.OleDb;
namespace HotelManangementSystemLibrary
{
    internal class Execute
    {
        public static int Scalar(OleDbConnection con, OleDbCommand cmd)
        {
            try
            {
                int i = (int)cmd.ExecuteScalar(); ;
                return i;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                con.Close();
                throw;
                //return -1;
            }
            finally
            {
                
            }
        }//Scalar
        public static OleDbDataReader GetReader(OleDbConnection con, OleDbCommand cmd)
        {
            try
            {
                return cmd.ExecuteReader();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                con.Close();
                throw;
                //return default;
            }
            finally
            {
                
            }
        }
        public static int NoneQuery(OleDbConnection con, OleDbCommand cmd)
        {
            try
            {
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                con.Close();
                throw;
                //return -1;
            }
            finally
            {
                
            }
        }
    }//class
}//namesapce
