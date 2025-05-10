using CraftMan_WebApi.DataAccessLayer;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json.Linq;
using System.ComponentModel.Design;

namespace CraftMan_WebApi.Models
{
    public class DeviceToken
    {
        public static Boolean ValidateToken(DeviceTokenModel _DeviceTokenModel)
        {
            DBAccess db = new DBAccess();

            SqlDataReader reader;

            Response strReturn = new Response();

            string qstr;

            qstr = " select Id from tblCompanyUserDevices where IsValid = 1 AND Token = '" + _DeviceTokenModel.Token + "' and pCompId = " + _DeviceTokenModel.pCompId.ToString();
            //string qstr = "select Id from tblCompanyUserDevices where Token = '" + _DeviceTokenModel.Token + "'";

            reader = db.ReadDB(qstr);

            while (reader.Read())
            {
                return true;
            }

            reader.Close();
            reader.Dispose();


            /// check if for other company exists the token and update to valid = false


            qstr = " select Id from tblCompanyUserDevices where IsValid = 1 AND Token = '" + _DeviceTokenModel.Token + "' and pCompId != " + _DeviceTokenModel.pCompId.ToString();

            reader = db.ReadDB(qstr);

            bool flag = false;

            while (reader.Read())
            {
                flag = true;
            }

            reader.Close();
            reader.Dispose();

            if (flag)
            {
                qstr = " UPDATE tblCompanyUserDevices " +
                        " SET  " +
                        " IsValid = 0"  +
                        " WHERE IsValid = 1 AND Token = '" + _DeviceTokenModel.Token + "'";

                db.ExecuteNonQuery(qstr);
            }

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
