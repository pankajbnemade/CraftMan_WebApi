using CraftMan_WebApi.DataAccessLayer;
using Microsoft.Data.SqlClient;

namespace CraftMan_WebApi.Models
{
    public class DeviceToken
    {
        public static Boolean ValidateToken(DeviceTokenModel _DeviceTokenModel)
        {
            DBAccess db = new DBAccess();

            Response strReturn = new Response();

            string qstr = " select Id from tblCompanyUserDevices where Token = '" + _DeviceTokenModel.Token + "' and pCompId = " + _DeviceTokenModel.pCompId.ToString() ;

            SqlDataReader reader = db.ReadDB(qstr);

            while (reader.Read())
            {
                return true;
            }

            reader.Close();
            reader.Dispose();

            return false;
        }


        public static int SaveNewDeviceToken(DeviceTokenModel _DeviceTokenModel)
        {
            try
            {
                string qstr = " INSERT into tblCompanyUserDevices(pCompId, Token, Platform, RegisteredOn)  " +
                    " VALUES(" + _DeviceTokenModel.pCompId + ",'" + _DeviceTokenModel.Token + "','" + _DeviceTokenModel.Platform + "', getdate()) ";

                DBAccess db = new DBAccess();
                int i = db.ExecuteNonQuery(qstr);

                return i;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
        }
    }
}
