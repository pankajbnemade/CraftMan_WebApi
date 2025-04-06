using CraftMan_WebApi.DataAccessLayer;
using CraftMan_WebApi.Helper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CraftMan_WebApi.Models
{
    public class UserMaster
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public int UserId { get; set; } // Primary key
        public int? LocationId { get; set; } // Nullable integer
        public string MobileNumber { get; set; }
        public string? ContactPerson { get; set; }
        public string EmailId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int CountyId { get; set; }
        public int MunicipalityId { get; set; }
        public string? CountyName { get; set; }
        public string? MunicipalityName { get; set; }

        public static UserMaster GetUserDetail(string EmailId)
        {
            DBAccess db = new DBAccess();
            Response strReturn = new Response();
            string qstr = " SELECT  tblUserMaster.*,  " +
                            " tblCountyMaster.CountyName, tblMunicipalityMaster.MunicipalityName " +
                            " FROM  tblUserMaster " +
                            " LEFT OUTER JOIN tblCountyMaster ON tblCountyMaster.CountyId = tblUserMaster.CountyId " +
                            " LEFT OUTER JOIN tblMunicipalityMaster ON tblMunicipalityMaster.MunicipalityId = tblUserMaster.MunicipalityId" +
                            " where upper(EmailId)=upper('" + EmailId.Trim() + "')  ";

            SqlDataReader reader = db.ReadDB(qstr);
            var pUserMaster = new UserMaster();

            while (reader.Read())
            {
                pUserMaster.Username = reader["Username"] == DBNull.Value ? "" : (string)reader["Username"];
                pUserMaster.Password = reader["Password"] == DBNull.Value ? "" : (string)reader["Password"];
                pUserMaster.Active = reader["Active"] == DBNull.Value ? false : Convert.ToBoolean(reader["Active"]);

                pUserMaster.UserId = reader["pkey_UId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["pkey_UId"]);
                pUserMaster.LocationId = reader["LocationId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["LocationId"]);
                pUserMaster.MobileNumber = reader["MobileNumber"] == DBNull.Value ? "" : (string)reader["MobileNumber"];
                pUserMaster.ContactPerson = reader["ContactPerson"] == DBNull.Value ? "" : (string)reader["ContactPerson"];
                pUserMaster.EmailId = reader["EmailId"] == DBNull.Value ? "" : (string)reader["EmailId"];

                pUserMaster.CreatedOn = reader["CreatedOn"] == DBNull.Value ? null : (DateTime)reader["CreatedOn"];
                pUserMaster.UpdatedOn = reader["UpdatedOn"] == DBNull.Value ? null : (DateTime)reader["UpdatedOn"];

                pUserMaster.CountyName = reader["CountyName"] == DBNull.Value ? "" : reader["CountyName"].ToString();
                pUserMaster.MunicipalityName = reader["MunicipalityName"] == DBNull.Value ? "" : reader["MunicipalityName"].ToString();
            }

            reader.Close();

            return pUserMaster;
        }

        public static Response LoginValidateForUser(LoginUser _User)
        {
            Response strReturn = new Response();

            strReturn.StatusMessage = "Invalid User";
            strReturn.StatusCode = 1;

            DBAccess db = new DBAccess();

            string qstr = "select pkey_UId from dbo.tblUserMaster where Password='" + _User.Password + "' and Active='" + _User.Active + "' and   upper(EmailId)=upper('" + _User.EmailId.Trim() + "')  ";

            strReturn.StatusCode = db.ExecuteScalar(qstr);

            if (strReturn.StatusCode > 0)
            {
                strReturn.StatusMessage = "Valid User!";

                var token = CommonFunction.GenerateJwtToken(strReturn.StatusCode, "User");

                strReturn.JWTToken = token;
            }
            else { strReturn.StatusMessage = "Invalid User!"; }

            return strReturn;
        }

        public static Response InsertUser(UserMaster _User)
        {
            Response strReturn = new Response();
            string qstr = "select Password from dbo.tblUserMaster where upper(EmailId)=upper('" + _User.EmailId.Trim() + "') or upper(Username)=upper('" + _User.Username.Trim() + "')";
            DBAccess db = new DBAccess();
            if (db.validate(qstr).StatusCode > 0)
            {
                strReturn.StatusMessage = "User already exists...";
                strReturn.StatusCode = 1;
            }
            else
            {
                qstr = " INSERT into dbo.tblUserMaster(Username,Password,Active,LocationId,MobileNumber,ContactPerson,EmailId,CreatedOn, CountyId, MunicipalityId)     " +
                    " VALUES('" + _User.Username.Trim() + "','" + _User.Password + "','" + _User.Active + "','" + _User.LocationId + "','" + _User.MobileNumber + "','"
                    + _User.ContactPerson + "','" + _User.EmailId.Trim() + "', getdate()"
                    + "," + _User.CountyId + "," + _User.MunicipalityId
                    + ")";

                int i = db.ExecuteScalar(qstr);

                if (i > 0)
                {
                    strReturn.StatusCode = i;
                    strReturn.StatusMessage = "User Registered Successfully";
                }
                else
                { strReturn.StatusMessage = "User not registered"; }
            }

            return strReturn;
        }

    }
}
