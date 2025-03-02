using CraftMan_WebApi.DataAccessLayer;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.Data.SqlClient;
using System.Collections;

namespace CraftMan_WebApi.Models
{
    public class CompanyMaster
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public int Pkey_UId { get; set; }
        public int LocationId { get; set; }
        public string MobileNumber { get; set; }
        public string ContactPerson { get; set; }
        public string EmailId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string CompanyName { get; set; }
        public string CompanyRegistrationNumber { get; set; }
        public string CompanyPresentation { get; set; }
        public string CompetenceDescription { get; set; }
        public string CompanyReferences { get; set; }

        public string[] JobList { get; set; }
        public string[] CompanyEmplist { get; set; }
        public string[] CountyList { get; set; }
        public string[] MunicipalityList { get; set; }

        public static CompanyMaster GetCompanyDetail(string user)
        {

            DBAccess db = new DBAccess();
            Response strReturn = new Response();
            string qstr = "  SELECT  Username      ,Password      ,Active      ,UserType      ,pCompId      ,LocationId      ,MobileNumber      ,ContactPerson      ,EmailId      ,CreatedOn      ,UpdatedOn      ,CompanyName      ,CompanyRegistrationNumber      ,CompanyPresentation      ,Logotype      ,CompetenceDescription      ,CompanyReferences      ,JobList  FROM  dbo.tblCompanyMaster where Username='" + user + "'  ";
            SqlDataReader reader = db.ReadDB(qstr);
            var pCompanyMaster = new CompanyMaster();
            while (reader.Read())
            {
                pCompanyMaster.Username = (string)reader["Username"];
                pCompanyMaster.Password = (string)reader["Password"];
                pCompanyMaster.Active = Convert.ToBoolean(reader["Active"]); //(Boolean)reader["Active"];
                pCompanyMaster.MobileNumber = (string)reader["MobileNumber"];

                pCompanyMaster.LocationId = Convert.ToInt32(reader["LocationId"]);
                pCompanyMaster.ContactPerson = (string)reader["ContactPerson"];// Convert.ToInt32(reader["TicketId"]);
                pCompanyMaster.EmailId = (string)reader["EmailId"];
                if (reader["CompanyName"].ToString() != "")
                { pCompanyMaster.CompanyName = (string)reader["CompanyName"]; }

                pCompanyMaster.JobList = reader["JobList"].ToString().Split(",");
                if (reader["CompanyRegistrationNumber"].ToString() != "") { pCompanyMaster.CompanyRegistrationNumber = (string)reader["CompanyRegistrationNumber"]; }


            }

            reader.Close();

            return pCompanyMaster;

        }


        //public static int GetActivecountnoofcraftsman(string user) { int cnt = 0;

        //    DBAccess db = new DBAccess();
        //    Response strReturn = new Response();
        //    string qstr = " select count(*)Activecount from   [dbo].[tblIssueTicketMaster] a inner join [dbo].[tblCompanyMaster] b  on b.LocationId=a.Pincode and b.Username= '" + user + "'   ";
        //    SqlDataReader reader = db.ReadDB(qstr);

        //    while (reader.Read())
        //    {
        //        cnt = (int)reader["Activecount"];

        //    }

        //    reader.Close();
        //    return cnt;
        //}
        public static int GetTotalcnt(string user)
        {


            int cnt = 0;
            DBAccess db = new DBAccess();
            Response strReturn = new Response();
            string qstr = " select count(*)totaljobrequest from   [dbo].[tblIssueTicketMaster] a inner join [dbo].[tblCompanyMaster] b  on b.LocationId=a.Pincode and b.Username= '" + user + "'   ";
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
            string qstr = " select Username from dbo.tblCompanyMaster where upper(EmailId) = upper('" + _Company.EmailId + "') and upper(Password)= upper('" + _Company.Password + "')";
            DBAccess db = new DBAccess();
            return db.validate(qstr);
        }
        public static int InsertCompany(CompanyMaster _Company)
        {
            string qstr = " INSERT into dbo.tblCompanyMaster(Username, Password, Active, LocationId, MobileNumber, ContactPerson, EmailId, CreatedOn,joblist)     VALUES('" + _Company.Username + "', '" + _Company.Password + "', '" + _Company.Active + "', '" + _Company.LocationId + "', '" + _Company.MobileNumber + "', '" + _Company.ContactPerson + "', '" + _Company.EmailId + "', getdate(),'" + string.Join(",", _Company.JobList) + "')";

            int h = 0;
            DBAccess db = new DBAccess();
            int i = db.ExecuteNonQuery(qstr);

            return i;
        }
        public static int UpdateCompany(CompanyMaster _Company)
        {
            string qstr = "UPDATE dbo.tblCompanyMaster " +
                             "SET  " +
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