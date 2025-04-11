using CraftMan_WebApi.DataAccessLayer;
using CraftMan_WebApi.Helper;
using Microsoft.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;

namespace CraftMan_WebApi.Models
{
    public class CompanyMaster
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public int? pCompId { get; set; }
        public int? LocationId { get; set; }
        public string MobileNumber { get; set; }
        public string? ContactPerson { get; set; }
        public string EmailId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Is24X7 { get; set; }
        public string CompanyName { get; set; }
        public string? CompanyRegistrationNumber { get; set; }
        public string? CompanyPresentation { get; set; }
        public string? CompetenceDescription { get; set; }
        public string? CompanyReferences { get; set; }
        public string[]? JobList { get; set; }
        public string[]? CountyIdList { get; set; }
        public string[]? MunicipalityIdList { get; set; }
        public string[]? ServiceIdList { get; set; }
        public IFormFile? LogoImage { get; set; }
        public string? LogoImageName { get; set; }
        public string? LogoImagePath { get; set; }
        public string? LogoImageContentType { get; set; }
        public List<CountyMaster>? CountyList { get; set; }
        public List<MunicipalityMaster>? MunicipalityList { get; set; }
        public List<CompanyCountyRelation>? CountyRelationList { get; set; }
        public List<CompanyServices>? ServiceList { get; set; }

        public static CompanyMaster GetCompanyDetail(string EmailId)
        {

            DBAccess db = new DBAccess();
            Response strReturn = new Response();
            string qstr = " SELECT  tblCompanyMaster.*  " +
                            " FROM  tblCompanyMaster where upper(EmailId) = upper('" + EmailId + "')  ";

            SqlDataReader reader = db.ReadDB(qstr);
            var pCompanyMaster = new CompanyMaster();

            while (reader.Read())
            {
                pCompanyMaster.Username = reader["Username"] == DBNull.Value ? "" : (string)reader["Username"];
                pCompanyMaster.Password = reader["Password"] == DBNull.Value ? "" : (string)reader["Password"];
                pCompanyMaster.Active = reader["Active"] == DBNull.Value ? false : Convert.ToBoolean(reader["Active"]);

                pCompanyMaster.pCompId = reader["pCompId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["pCompId"]);
                pCompanyMaster.LocationId = reader["LocationId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["LocationId"]);
                pCompanyMaster.MobileNumber = reader["MobileNumber"] == DBNull.Value ? "" : (string)reader["MobileNumber"];
                pCompanyMaster.ContactPerson = reader["ContactPerson"] == DBNull.Value ? "" : (string)reader["ContactPerson"];
                pCompanyMaster.EmailId = reader["EmailId"] == DBNull.Value ? "" : (string)reader["EmailId"];
                pCompanyMaster.Is24X7 = reader["Is24X7"] == DBNull.Value ? true : Convert.ToBoolean(reader["Is24X7"]);

                pCompanyMaster.CreatedOn = reader["CreatedOn"] == DBNull.Value ? null : (DateTime)reader["CreatedOn"];
                pCompanyMaster.UpdatedOn = reader["UpdatedOn"] == DBNull.Value ? null : (DateTime)reader["UpdatedOn"];

                pCompanyMaster.CompanyName = reader["CompanyName"] == DBNull.Value ? "" : (string)reader["CompanyName"];
                pCompanyMaster.CompanyRegistrationNumber = reader["CompanyRegistrationNumber"] == DBNull.Value ? "" : (string)reader["CompanyRegistrationNumber"];
                pCompanyMaster.CompanyPresentation = reader["CompanyPresentation"] == DBNull.Value ? "" : (string)reader["CompanyPresentation"];
                pCompanyMaster.CompetenceDescription = reader["CompetenceDescription"] == DBNull.Value ? "" : (string)reader["CompetenceDescription"];
                pCompanyMaster.CompanyReferences = reader["CompanyReferences"] == DBNull.Value ? "" : (string)reader["CompanyReferences"];
                pCompanyMaster.LogoImageName = reader["LogoImageName"] == DBNull.Value ? "" : (string)reader["LogoImageName"];
                pCompanyMaster.LogoImagePath = reader["LogoImagePath"] == DBNull.Value ? "" : (string)reader["LogoImagePath"];


                ImageSettings pImageSettings = new ImageSettings();

                if (pCompanyMaster.LogoImagePath != "")
                {
                    pCompanyMaster.LogoImagePath = pCompanyMaster.LogoImagePath.Replace("\\", "/");

                    if (System.IO.File.Exists(pCompanyMaster.LogoImagePath))
                    {
                        pCompanyMaster.LogoImageContentType = CommonFunction.GetContentType(pCompanyMaster.LogoImagePath);

                        pCompanyMaster.LogoImagePath = pCompanyMaster.LogoImagePath.Replace(pImageSettings.StoragePath, pImageSettings.BaseUrl);
                    }
                    else
                    {
                        pCompanyMaster.LogoImageContentType = CommonFunction.GetContentType(pImageSettings.DefaultImageUrl);
                        pCompanyMaster.LogoImagePath = pImageSettings.DefaultImageUrl;
                    }
                }
            }

            reader.Close();
            reader.Dispose();

            return pCompanyMaster;

        }

        public static ArrayList GetCompanyList(int? countyId, int? municipalityId, int? serviceId)
        {
            DBAccess db = new DBAccess();
            Response strReturn = new Response();

            string qstr = " SELECT	DISTINCT tblCompanyMaster.Username, tblCompanyMaster.Password, tblCompanyMaster.Active, tblCompanyMaster.UserType, " +
                            " tblCompanyMaster.pCompId, tblCompanyMaster.LocationId, tblCompanyMaster.MobileNumber, " +
                            " tblCompanyMaster.ContactPerson, tblCompanyMaster.EmailId, tblCompanyMaster.CreatedOn, tblCompanyMaster.UpdatedOn," +
                            " tblCompanyMaster.CompanyName, tblCompanyMaster.CompanyRegistrationNumber, tblCompanyMaster.CompanyPresentation, " +
                            " tblCompanyMaster.Logotype, tblCompanyMaster.CompetenceDescription, tblCompanyMaster.CompanyReferences, " +
                            " tblCompanyMaster.JobList, tblCompanyMaster.LogoImageName, tblCompanyMaster.LogoImagePath, tblCompanyMaster.PasswordResetToken, " +
                            " tblCompanyMaster.ResetTokenExpiry, tblCompanyMaster.Is24X7" +
                            //" , tblCompanyCountyRel.CountyId, tblCompanyCountyRel.MunicipalityId,  tblCompanyServices.ServiceId" +
                            " FROM   tblCompanyMaster" +
                            " LEFT OUTER JOIN tblCompanyCountyRel ON tblCompanyCountyRel.pCompId = tblCompanyMaster.pCompId" +
                            " LEFT OUTER JOIN tblCompanyServices ON tblCompanyServices.pCompId = tblCompanyMaster.pCompId " +
                            " WHERE tblCompanyMaster.pCompId != 0";

            if (countyId != null && countyId != 0)
            {
                qstr = qstr + " AND tblCompanyCountyRel.CountyId = " + countyId;
            }

            if (municipalityId != null && municipalityId != 0)
            {
                qstr = qstr + " AND tblCompanyCountyRel.MunicipalityId = " + municipalityId;
            }

            if (serviceId != null && serviceId != 0)
            {
                qstr = qstr + " AND tblCompanyServices.ServiceId = " + serviceId;
            }

            SqlDataReader reader = db.ReadDB(qstr);

            ArrayList CompanyMasterList = new ArrayList();

            while (reader.Read())
            {
                CompanyMaster pCompanyMaster = new CompanyMaster();

                pCompanyMaster.Username = reader["Username"] == DBNull.Value ? "" : (string)reader["Username"];
                pCompanyMaster.Password = reader["Password"] == DBNull.Value ? "" : (string)reader["Password"];
                pCompanyMaster.Active = reader["Active"] == DBNull.Value ? false : Convert.ToBoolean(reader["Active"]);

                pCompanyMaster.pCompId = reader["pCompId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["pCompId"]);
                pCompanyMaster.LocationId = reader["LocationId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["LocationId"]);
                pCompanyMaster.MobileNumber = reader["MobileNumber"] == DBNull.Value ? "" : (string)reader["MobileNumber"];
                pCompanyMaster.ContactPerson = reader["ContactPerson"] == DBNull.Value ? "" : (string)reader["ContactPerson"];
                pCompanyMaster.EmailId = reader["EmailId"] == DBNull.Value ? "" : (string)reader["EmailId"];
                pCompanyMaster.Is24X7 = reader["Is24X7"] == DBNull.Value ? true : Convert.ToBoolean(reader["Is24X7"]);

                pCompanyMaster.CreatedOn = reader["CreatedOn"] == DBNull.Value ? null : (DateTime)reader["CreatedOn"];
                pCompanyMaster.UpdatedOn = reader["UpdatedOn"] == DBNull.Value ? null : (DateTime)reader["UpdatedOn"];

                pCompanyMaster.CompanyName = reader["CompanyName"] == DBNull.Value ? "" : (string)reader["CompanyName"];
                pCompanyMaster.CompanyRegistrationNumber = reader["CompanyRegistrationNumber"] == DBNull.Value ? "" : (string)reader["CompanyRegistrationNumber"];
                pCompanyMaster.CompanyPresentation = reader["CompanyPresentation"] == DBNull.Value ? "" : (string)reader["CompanyPresentation"];
                pCompanyMaster.CompetenceDescription = reader["CompetenceDescription"] == DBNull.Value ? "" : (string)reader["CompetenceDescription"];
                pCompanyMaster.CompanyReferences = reader["CompanyReferences"] == DBNull.Value ? "" : (string)reader["CompanyReferences"];
                pCompanyMaster.LogoImageName = reader["LogoImageName"] == DBNull.Value ? "" : (string)reader["LogoImageName"];
                pCompanyMaster.LogoImagePath = reader["LogoImagePath"] == DBNull.Value ? "" : (string)reader["LogoImagePath"];


                ImageSettings pImageSettings = new ImageSettings();

                if (pCompanyMaster.LogoImagePath != "")
                {
                    pCompanyMaster.LogoImagePath = pCompanyMaster.LogoImagePath.Replace("\\", "/");

                    if (System.IO.File.Exists(pCompanyMaster.LogoImagePath))
                    {
                        pCompanyMaster.LogoImageContentType = CommonFunction.GetContentType(pCompanyMaster.LogoImagePath);

                        pCompanyMaster.LogoImagePath = pCompanyMaster.LogoImagePath.Replace(pImageSettings.StoragePath, pImageSettings.BaseUrl);
                    }
                    else
                    {
                        pCompanyMaster.LogoImageContentType = CommonFunction.GetContentType(pImageSettings.DefaultImageUrl);
                        pCompanyMaster.LogoImagePath = pImageSettings.DefaultImageUrl;
                    }
                }

                CompanyMasterList.Add(pCompanyMaster);
            }

            reader.Close();
            reader.Dispose();

            return CompanyMasterList;

        }

        public static int GetTotalcnt(string user)
        {
            int cnt = 0;
            DBAccess db = new DBAccess();
            Response strReturn = new Response();
            string qstr = " select count(*) totaljobrequest from   [dbo].[tblIssueTicketMaster] a inner join [dbo].[tblCompanyMaster] b  on b.LocationId=a.Pincode and b.Username= '" + user.Trim() + "'   ";
            SqlDataReader reader = db.ReadDB(qstr);

            while (reader.Read())
            {
                cnt = (int)reader["totaljobrequest"];
            }

            reader.Close();
            reader.Dispose();

            return cnt;
        }

        public Response ValidateCompany(CompanyMaster _Company)
        {
            string qstr = " select pCompId from tblCompanyMaster where upper(EmailId) = upper('" + _Company.EmailId.Trim() + "') or upper(Username)= upper('" + _Company.Username + "')";

            DBAccess db = new DBAccess();
            return db.validate(qstr);
        }

        public static Response LoginValidateForCompanyUser(LoginComp _User)
        {
            Response strReturn = new Response();

            strReturn.StatusMessage = "Invalid User";
            strReturn.StatusCode = 1;

            DBAccess db = new DBAccess();

            string qstr = " select pCompId from tblCompanyMaster where upper(EmailId) = upper('" + _User.EmailId.Trim() + "') and upper(Password)= upper('" + _User.Password + "')";

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

        public static int InsertCompany(CompanyMaster _Company)
        {
            int is24X7 = _Company.Is24X7 == null ? 0 : (_Company.Is24X7 == true ? 1 : 0);

            string qstr = " INSERT into tblCompanyMaster" +
                " (" +
                    "Username, Password, Active, LocationId, MobileNumber, ContactPerson, " +
                    "EmailId, CreatedOn,  CompanyName, CompanyRegistrationNumber, CompanyPresentation, " +
                    "CompetenceDescription, CompanyReferences,  LogoImageName, LogoImagePath, Is24X7 " +
                ")  " +
                "   VALUES('" + _Company.Username.Trim() + "', '" + _Company.Password + "', '" + _Company.Active + "', '" + _Company.LocationId + "', '" + _Company.MobileNumber + "', '" + _Company.ContactPerson +
                        "', '" + _Company.EmailId.Trim() + "'," + " getdate(), " + "'" + _Company.CompanyName.Trim() + "', '" + _Company.CompanyRegistrationNumber + "', '" + _Company.CompanyPresentation +
                        "', '" + _Company.CompetenceDescription + "', '" + _Company.CompanyReferences + "', '" + _Company.LogoImageName + "', '" + _Company.LogoImagePath + "', "
                        //+ "1" //is24X7
                        + is24X7
                        +
                ")";

            Console.WriteLine(qstr);

            DBAccess db = new DBAccess();

            int i = db.ExecuteScalar(qstr);

            return i;
        }

        public static int UpdateCompany(CompanyMaster _Company)
        {
            string qstr = " UPDATE tblCompanyMaster " +
                            " SET  " +
                            "   LocationId = " + _Company.LocationId + "," +
                            "   MobileNumber = '" + _Company.MobileNumber + "'," +
                            "   ContactPerson = '" + _Company.ContactPerson + "'," +
                            "   CompanyName = '" + _Company.CompanyName + "'," +
                            "   CompanyRegistrationNumber = '" + _Company.CompanyRegistrationNumber + "'," +
                            "   CompanyPresentation = '" + _Company.CompanyPresentation + "'," +
                            "   CompetenceDescription = '" + _Company.CompetenceDescription + "'," +
                            "   CompanyReferences = '" + _Company.CompanyReferences + "'," + "   LogoImageName = " + (_Company.LogoImageName == "" ? "LogoImageName," : "'" + _Company.LogoImageName + "', ") +
                            "   LogoImagePath = " + (_Company.LogoImagePath == "" ? "LogoImagePath" : "'" + _Company.LogoImagePath + "'") +
                            "   WHERE " +
                            "   UPPER(EmailId) = UPPER('" + _Company.EmailId.Trim() + "') " +
                            "   AND  pCompId =" + _Company.pCompId;

            DBAccess db = new DBAccess();

            return db.ExecuteNonQuery(qstr);
        }

        public static int UpdateActive(int companyId, string active)
        {
            string qstr = " UPDATE tblCompanyMaster " +
                            " SET  " +
                            " Active = " + active +
                            " WHERE  pCompId = " + companyId;

            DBAccess db = new DBAccess();

            return db.ExecuteNonQuery(qstr);
        }

        public static int UpdateCompanyIs24X7(int companyId, bool is24X7)
        {
            string qstr = " UPDATE tblCompanyMaster " +
                            " SET  " +
                            " Is24X7 = " + (is24X7 == true ? 1 : 0) +
                            " WHERE  pCompId = " + companyId;

            DBAccess db = new DBAccess();

            return db.ExecuteNonQuery(qstr);
        }

        public static ArrayList GetCompany24X7ForUser(Int32 userId)
        {
            DBAccess db = new DBAccess();
            Response strReturn = new Response();

            string qstr = " SELECT DISTINCT	tblCompanyMaster.Username, tblCompanyMaster.Password, tblCompanyMaster.Active, tblCompanyMaster.UserType, " +
                        " tblCompanyMaster.pCompId, tblCompanyMaster.LocationId, tblCompanyMaster.MobileNumber, tblCompanyMaster.ContactPerson,  " +
                        " tblCompanyMaster.EmailId, tblCompanyMaster.CreatedOn, tblCompanyMaster.UpdatedOn, tblCompanyMaster.CompanyName,  " +
                        " tblCompanyMaster.CompanyRegistrationNumber, tblCompanyMaster.CompanyPresentation, tblCompanyMaster.Logotype,  " +
                        " tblCompanyMaster.CompetenceDescription, tblCompanyMaster.CompanyReferences, tblCompanyMaster.JobList,  " +
                        " tblCompanyMaster.LogoImageName, tblCompanyMaster.LogoImagePath, tblCompanyMaster.PasswordResetToken, " +
                        " tblCompanyMaster.ResetTokenExpiry, tblCompanyMaster.Is24X7, tblCompanyCountyRel.CountyId, tblCompanyCountyRel.MunicipalityId " +
                        " FROM   tblCompanyMaster " +
                        " INNER JOIN tblCompanyCountyRel ON tblCompanyMaster.pCompId = tblCompanyCountyRel.pCompId " +
                        " INNER JOIN tblUserMaster ON tblUserMaster.CountyId = tblCompanyCountyRel.CountyId " +
                        " AND(tblUserMaster.MunicipalityId = tblCompanyCountyRel.MunicipalityId OR tblCompanyCountyRel.MunicipalityId = 0) " +
                        " WHERE tblUserMaster.pkey_UId = " + userId.ToString() +
                        " AND ISNULL(tblCompanyMaster.Is24X7, 1) = 1";

            SqlDataReader reader = db.ReadDB(qstr);

            ArrayList CompanyMasterList = new ArrayList();

            while (reader.Read())
            {
                CompanyMaster pCompanyMaster = new CompanyMaster();

                pCompanyMaster.Username = reader["Username"] == DBNull.Value ? "" : (string)reader["Username"];
                pCompanyMaster.Password = reader["Password"] == DBNull.Value ? "" : (string)reader["Password"];
                pCompanyMaster.Active = reader["Active"] == DBNull.Value ? false : Convert.ToBoolean(reader["Active"]);
                pCompanyMaster.pCompId = reader["pCompId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["pCompId"]);
                pCompanyMaster.LocationId = reader["LocationId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["LocationId"]);
                pCompanyMaster.MobileNumber = reader["MobileNumber"] == DBNull.Value ? "" : (string)reader["MobileNumber"];
                pCompanyMaster.ContactPerson = reader["ContactPerson"] == DBNull.Value ? "" : (string)reader["ContactPerson"];
                pCompanyMaster.EmailId = reader["EmailId"] == DBNull.Value ? "" : (string)reader["EmailId"];
                pCompanyMaster.Is24X7 = reader["Is24X7"] == DBNull.Value ? true : Convert.ToBoolean(reader["Is24X7"]);
                pCompanyMaster.CreatedOn = reader["CreatedOn"] == DBNull.Value ? null : (DateTime)reader["CreatedOn"];
                pCompanyMaster.UpdatedOn = reader["UpdatedOn"] == DBNull.Value ? null : (DateTime)reader["UpdatedOn"];
                pCompanyMaster.CompanyName = reader["CompanyName"] == DBNull.Value ? "" : (string)reader["CompanyName"];
                pCompanyMaster.CompanyRegistrationNumber = reader["CompanyRegistrationNumber"] == DBNull.Value ? "" : (string)reader["CompanyRegistrationNumber"];
                pCompanyMaster.CompanyPresentation = reader["CompanyPresentation"] == DBNull.Value ? "" : (string)reader["CompanyPresentation"];
                pCompanyMaster.CompetenceDescription = reader["CompetenceDescription"] == DBNull.Value ? "" : (string)reader["CompetenceDescription"];
                pCompanyMaster.CompanyReferences = reader["CompanyReferences"] == DBNull.Value ? "" : (string)reader["CompanyReferences"];
                pCompanyMaster.LogoImageName = reader["LogoImageName"] == DBNull.Value ? "" : (string)reader["LogoImageName"];
                pCompanyMaster.LogoImagePath = reader["LogoImagePath"] == DBNull.Value ? "" : (string)reader["LogoImagePath"];

                ImageSettings pImageSettings = new ImageSettings();

                if (pCompanyMaster.LogoImagePath != "")
                {
                    pCompanyMaster.LogoImagePath = pCompanyMaster.LogoImagePath.Replace("\\", "/");

                    if (System.IO.File.Exists(pCompanyMaster.LogoImagePath))
                    {
                        pCompanyMaster.LogoImageContentType = CommonFunction.GetContentType(pCompanyMaster.LogoImagePath);

                        pCompanyMaster.LogoImagePath = pCompanyMaster.LogoImagePath.Replace(pImageSettings.StoragePath, pImageSettings.BaseUrl);
                    }
                    else
                    {
                        pCompanyMaster.LogoImageContentType = CommonFunction.GetContentType(pImageSettings.DefaultImageUrl);
                        pCompanyMaster.LogoImagePath = pImageSettings.DefaultImageUrl;
                    }
                }

                CompanyMasterList.Add(pCompanyMaster);
            }

            reader.Close();
            reader.Dispose();

            return CompanyMasterList;
        }


    }
}