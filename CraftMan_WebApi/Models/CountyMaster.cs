using CraftMan_WebApi.DataAccessLayer;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.Data.SqlClient;
using System.Collections;

namespace CraftMan_WebApi.Models
{
    public class CountyMaster
    {
        public int CountyId { get; set; }
        public string CountyName { get; set; }

        public static CountyMaster GetCountyDetail(int? CountyId)
        {
            DBAccess db = new DBAccess();
            Response strReturn = new Response();

            string qstr = " SELECT  CountyId, CountyName FROM  dbo.tblCountyMaster where CountyId = " + CountyId.ToString() + "  ";

            SqlDataReader reader = db.ReadDB(qstr);

            var pCountyMaster = new CountyMaster();

            while (reader.Read())
            {
                pCountyMaster.CountyId = Convert.ToInt32(reader["CountyId"]);
                pCountyMaster.CountyName = reader["CountyName"] == DBNull.Value ? "" : (string)reader["CountyName"];
            }

            reader.Close();
            reader.Dispose();

            return pCountyMaster;
        }


        public static ArrayList GetCountyList()
        {
            ArrayList CountyList = new ArrayList();

            DBAccess db = new DBAccess();
            Response strReturn = new Response();

            string qstr = " SELECT  CountyId, CountyName FROM  dbo.tblCountyMaster";

            SqlDataReader reader = db.ReadDB(qstr);

            while (reader.Read())
            {
                var pCountyMaster = new CountyMaster();

                pCountyMaster.CountyId = Convert.ToInt32(reader["CountyId"]);
                pCountyMaster.CountyName = reader["CountyName"] == DBNull.Value ? "" : (string)reader["CountyName"];

                CountyList.Add(pCountyMaster);
            }

            reader.Close();
            reader.Dispose();

            return CountyList;
        }

        public static ArrayList GetCountyListByCompanyId(int CompanyId)
        {
            ArrayList CountyList = new ArrayList();

            DBAccess db = new DBAccess();
            Response strReturn = new Response();

            string qstr = " SELECT tblCountyMaster.CountyId, tblCountyMaster.CountyName, tblCompanyCountyRel.pCompId " +
                        " FROM   tblCompanyCountyRel  " +
                        " INNER JOIN  tblCountyMaster ON tblCompanyCountyRel.CountyId = tblCountyMaster.CountyId " +
                        " WHERE tblCompanyCountyRel.pCompId = " + CompanyId + " ";

            SqlDataReader reader = db.ReadDB(qstr);

            while (reader.Read())
            {
                var pCountyMaster = new CountyMaster();

                pCountyMaster.CountyId = Convert.ToInt32(reader["CountyId"]);
                pCountyMaster.CountyName = reader["CountyName"] == DBNull.Value ? "" : (string)reader["CountyName"];

                CountyList.Add(pCountyMaster);
            }

            reader.Close();
            reader.Dispose();

            return CountyList;
        }

        public static Boolean ValidateCounty(CountyMaster _County)
        {
            DBAccess db = new DBAccess();

            Response strReturn = new Response();

            string qstr = " select CountyName from dbo.tblCountyMaster where upper(CountyName) = upper('" + _County.CountyName + "')";

            SqlDataReader reader = db.ReadDB(qstr);

            while (reader.Read())
            {
                return true;
            }

            reader.Close();
            reader.Dispose();

            return false;
        }

        public static Boolean ValidateUpdateCounty(CountyMaster _County)
        {
            DBAccess db = new DBAccess();

            Response strReturn = new Response();

            string qstr = " select CountyName from dbo.tblCountyMaster where upper(CountyName) = upper('" + _County.CountyName + "') and CountyId != " + _County.CountyId.ToString() + "";

            SqlDataReader reader = db.ReadDB(qstr);

            while (reader.Read())
            {
                return true;
            }

            reader.Close();
            reader.Dispose();

            return false;
        }

        public static int InsertCounty(CountyMaster _County)
        {
            string qstr = " INSERT into dbo.tblCountyMaster(CountyName)  VALUES('" + _County.CountyName + "') ";

            DBAccess db = new DBAccess();
            int i = db.ExecuteNonQuery(qstr);

            return i;
        }

        public static int UpdateCounty(CountyMaster _County)
        {
            string qstr = " UPDATE dbo.tblCountyMaster " +
                            " SET  " +
                            "   CountyName = '" + _County.CountyName + "'" +
                            "   WHERE " +
                            "   CountyId = " + _County.CountyId + "  ";

            DBAccess db = new DBAccess();
            return db.ExecuteNonQuery(qstr);
        }

    }
}