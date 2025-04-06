using CraftMan_WebApi.DataAccessLayer;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.Data.SqlClient;
using System.Collections;

namespace CraftMan_WebApi.Models
{
    public class CompanyEmp
    {
        public string Fname { get; set; }
        public string Lname { get; set; }
        public bool Active { get; set; }
        public int Pkey_CId { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string CompanyName { get; set; }

        public static ArrayList GetCompEmplist(string _Company)
        {
            ArrayList  CompEmplist = new ArrayList();
            DBAccess db = new DBAccess();
            Response strReturn = new Response();
            string qstr = "select Fname from dbo.tblCompanyEmployees where companyname= '" + _Company + "'   ";
            SqlDataReader reader = db.ReadDB(qstr);

            while (reader.Read())
            {
                var pCompanyEmp = new CompanyEmp();
                pCompanyEmp.Fname = (string)reader["Fname"]; 
                CompEmplist.Add(pCompanyEmp);
            }

            reader.Close();

            return CompEmplist;
        }


    }
}