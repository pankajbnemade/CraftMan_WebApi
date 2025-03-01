using CraftMan_WebApi.DataAccessLayer;

namespace CraftMan_WebApi.Models
{
    public class UserMaster
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Active { get; set; }
        public int Pkey_UId { get; set; } // Primary key
        public int? LocationId { get; set; } // Nullable integer
        public string MobileNumber { get; set; }
        public string ContactPerson { get; set; }
        public string EmailId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int CountyId { get; set; }
        public int MunicipalityId { get; set; }

        public static Response LoginValidateForUser(LoginUser _User)
        {
            try
            {
                Response strReturn = new Response();
                strReturn.StatusMessage = "Invalid User";
                strReturn.StatusCode = 1;
                DBAccess db = new DBAccess();
                string qstr = "select Password from dbo.tblUserMaster where Password='" + _User.Password + "' and Active='" + _User.Active + "' and   upper(EmailId)=upper('" + _User.EmailId + "')  ";
                strReturn = db.validate(qstr);
                if (strReturn.StatusCode > 0)
                {
                    strReturn.StatusMessage = "Valid User!";
                }
                else { strReturn.StatusMessage = "Invalid User!"; }
                return strReturn;
            }
            catch (Exception ex) { throw; }
        }

        public static Response insertUser(UserMaster _User)
        {

            Response strReturn = new Response();
            string qstr = "select Password from dbo.tblUserMaster where upper(EmailId)=upper('" + _User.EmailId + "') or upper(Username)=upper('" + _User.Username + "')";
            DBAccess db = new DBAccess();
            if (db.validate(qstr).StatusCode > 0)
            {
                strReturn.StatusMessage = "User already exists...";
                strReturn.StatusCode = 1;
            }
            else
            {
                qstr = " INSERT into dbo.tblUserMaster(Username,Password,Active,LocationId,MobileNumber,ContactPerson,EmailId,CreatedOn, CountyId, MunicipalityId)     " +
                    " VALUES('" + _User.Username + "','" + _User.Password + "','" + _User.Active + "','" + _User.LocationId + "','" + _User.MobileNumber + "','"
                    + _User.ContactPerson + "','" + _User.EmailId + "',getdate()"
                    + "," + _User.CountyId + "," + _User.MunicipalityId 
                    + ")";

                if (db.ExecuteNonQuery(qstr) > 0)
                {
                    strReturn.StatusCode = 1;
                    strReturn.StatusMessage = "User Registered Successfully";
                }
                else
                { strReturn.StatusMessage = "User not registered"; }
            }

            return strReturn;
        }


    }
}
