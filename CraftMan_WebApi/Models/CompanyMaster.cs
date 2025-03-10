using CraftMan_WebApi.DataAccessLayer;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.Data.SqlClient;
using System.Collections;
using System.IO;
using CraftMan_WebApi.Helper;

namespace CraftMan_WebApi.Models
{
    public class CompanyMaster
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public int pCompId { get; set; }
        public int LocationId { get; set; }
        public string MobileNumber { get; set; }
        public string ContactPerson { get; set; }
        public string EmailId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string CompanyName { get; set; }
        public string CompanyRegistrationNumber { get; set; }
        public string CompanyPresentation { get; set; }
        public string CompetenceDescription { get; set; }
        public string CompanyReferences { get; set; }

        public IFormFile? LogoImage { get; set; }
        public string? LogoImageName { get; set; }
        public string? LogoImagePath { get; set; }

        public string[]? JobList { get; set; }
        //public string[]? CompanyEmplist { get; set; }
        public int[]? CountyList { get; set; }
        public int[]? MunicipalityList { get; set; }
        public int[]? ServiceList { get; set; }
        public string? LogoImageContentType { get; set; }
        public byte[]? LogoImageFileBytes { get; set; }
        public string? LogoImageBase64String { get; set; }

        public static CompanyMaster GetCompanyDetail(string user)
        {

            DBAccess db = new DBAccess();
            Response strReturn = new Response();
            string qstr = " SELECT  tblCompanyMaster.*  " +
                            " FROM  tblCompanyMaster where Username='" + user + "'  ";

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

                pCompanyMaster.CreatedOn = reader["CreatedOn"] == DBNull.Value ? null : (DateTime)reader["CreatedOn"];
                pCompanyMaster.UpdatedOn = reader["UpdatedOn"] == DBNull.Value ? null : (DateTime)reader["UpdatedOn"];

                pCompanyMaster.CompanyName = reader["CompanyName"] == DBNull.Value ? "" : (string)reader["CompanyName"];
                pCompanyMaster.CompanyRegistrationNumber = reader["CompanyRegistrationNumber"] == DBNull.Value ? "" : (string)reader["CompanyRegistrationNumber"];
                pCompanyMaster.CompanyPresentation = reader["CompanyPresentation"] == DBNull.Value ? "" : (string)reader["CompanyPresentation"];
                pCompanyMaster.CompetenceDescription = reader["CompetenceDescription"] == DBNull.Value ? "" : (string)reader["CompetenceDescription"];
                pCompanyMaster.CompanyReferences = reader["CompanyReferences"] == DBNull.Value ? "" : (string)reader["CompanyReferences"];
                pCompanyMaster.LogoImageName = reader["LogoImageName"] == DBNull.Value ? "" : (string)reader["LogoImageName"];
                pCompanyMaster.LogoImagePath = reader["LogoImagePath"] == DBNull.Value ? "" : (string)reader["LogoImagePath"];

                if (pCompanyMaster.LogoImagePath != "")
                {
                    pCompanyMaster.LogoImageContentType = CommonFunction.GetContentType(pCompanyMaster.LogoImagePath);
                    pCompanyMaster.LogoImageFileBytes = System.IO.File.ReadAllBytes(pCompanyMaster.LogoImagePath);
                    pCompanyMaster.LogoImageBase64String = Convert.ToBase64String(pCompanyMaster.LogoImageFileBytes);
                }
            }

            reader.Close();

            return pCompanyMaster;

        }

        public static int GetTotalcnt(string user)
        {
            int cnt = 0;
            DBAccess db = new DBAccess();
            Response strReturn = new Response();
            string qstr = " select count(*) totaljobrequest from   [dbo].[tblIssueTicketMaster] a inner join [dbo].[tblCompanyMaster] b  on b.LocationId=a.Pincode and b.Username= '" + user + "'   ";
            SqlDataReader reader = db.ReadDB(qstr);

            while (reader.Read())
            {
                cnt = (int)reader["totaljobrequest"];
            }

            reader.Close();

            return cnt;
        }

        public Response ValidateCompany(CompanyMaster _Company)
        {
            string qstr = " select pCompId from tblCompanyMaster where upper(EmailId) = upper('" + _Company.EmailId + "') and upper(Password)= upper('" + _Company.Password + "')";

            DBAccess db = new DBAccess();
            return db.validate(qstr);
        }

        public static Response LoginValidateForCompanyUser(LoginComp _User)
        {
            Response strReturn = new Response();

            strReturn.StatusMessage = "Invalid User";
            strReturn.StatusCode = 1;

            DBAccess db = new DBAccess();

            string qstr = " select pCompId from tblCompanyMaster where upper(EmailId) = upper('" + _User.EmailId + "') and upper(Password)= upper('" + _User.Password + "')";

            strReturn.StatusCode = db.ExecuteScalar(qstr);

            if (strReturn.StatusCode > 0)
            {
                strReturn.StatusMessage = "Valid User!";
            }
            else { strReturn.StatusMessage = "Invalid User!"; }

            return strReturn;
        }

        public static int InsertCompany(CompanyMaster _Company)
        {
            string qstr = " INSERT into tblCompanyMaster" +
                " (" +
                "Username, Password, Active, LocationId, MobileNumber, ContactPerson, " +
                "EmailId, CreatedOn,  CompanyName, CompanyRegistrationNumber, CompanyPresentation, " +
                "CompetenceDescription, CompanyReferences,  LogoImageName, LogoImagePath " +
                ")  " +
                "   VALUES('" + _Company.Username + "', '" + _Company.Password + "', '" + _Company.Active + "', '" + _Company.LocationId + "', '" + _Company.MobileNumber + "', '" + _Company.ContactPerson +
                "', '" + _Company.EmailId + "'," + " getdate(), " + "'" + _Company.CompanyName + "', '" + _Company.CompanyRegistrationNumber + "', '" + _Company.CompanyPresentation +
                "', '" + _Company.CompetenceDescription + "', '" + _Company.CompanyReferences + "', '" + _Company.LogoImageName + "', '" + _Company.LogoImagePath +
                "')";

            int h = 0;

            DBAccess db = new DBAccess();
            int i = db.ExecuteScalar(qstr);

            return i;
        }

        public static int UpdateCompany(CompanyMaster _Company)
        {
            string qstr = " UPDATE tblCompanyMaster " +
                            " SET  " +
                            "   Password = '" + _Company.Password + "'," +
                            "   Active = '" + _Company.Active + "'," +
                            "   LocationId = '" + _Company.LocationId + "'," +
                            "   MobileNumber = '" + _Company.MobileNumber + "'," +
                            "   ContactPerson = '" + _Company.ContactPerson + "'," +
                            "   WHERE " +
                            "   EmailId ='" + _Company.EmailId + "'  ";

            DBAccess db = new DBAccess();

            return db.ExecuteNonQuery(qstr);
        }

    }
}