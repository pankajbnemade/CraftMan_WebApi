using CraftMan_WebApi.DataAccessLayer;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.Data.SqlClient;
using System.Collections;

namespace CraftMan_WebApi.Models
{
    public class MunicipalityMaster
    {

        public int MunicipalityId { get; set; }
        public string MunicipalityName { get; set; }
        public int CountyId { get; set; }

        public static MunicipalityMaster GetMunicipalityMasterDetail(int? MunicipalityId)
        {
            DBAccess db = new DBAccess();
            Response strReturn = new Response();

            string qstr = "  SELECT  MunicipalityId, CountyId, MunicipalityName FROM  dbo.tblMunicipalityMaster where MunicipalityId="
                        + MunicipalityId == null ? "0" : MunicipalityId.ToString()
                        + "  ";

            SqlDataReader reader = db.ReadDB(qstr);

            var pMunicipalityMaster = new MunicipalityMaster();

            while (reader.Read())
            {
                pMunicipalityMaster.MunicipalityId = Convert.ToInt32(reader["MunicipalityId"]);
                pMunicipalityMaster.MunicipalityName = (string)reader["MunicipalityName"];
                pMunicipalityMaster.CountyId = Convert.ToInt32(reader["CountyId"]);
            }

            reader.Close();

            return pMunicipalityMaster;
        }

        public static ArrayList GetMunicipalityList(int CountyId)
        {
            ArrayList MunicipalityList = new ArrayList();

            DBAccess db = new DBAccess();
            Response strReturn = new Response();

            string qstr = "  SELECT  MunicipalityId, CountyId, MunicipalityName FROM  dbo.tblMunicipalityMaster where CountyId=" + CountyId.ToString() + "  ";

            SqlDataReader reader = db.ReadDB(qstr);

            while (reader.Read())
            {
                var pMunicipalityMaster = new MunicipalityMaster();

                pMunicipalityMaster.MunicipalityId = Convert.ToInt32(reader["MunicipalityId"]);
                pMunicipalityMaster.MunicipalityName = (string)reader["MunicipalityName"];
                pMunicipalityMaster.CountyId = Convert.ToInt32(reader["CountyId"]);

                MunicipalityList.Add(pMunicipalityMaster);
            }

            reader.Close();

            return MunicipalityList;
        }

        public Response ValidateCompany(MunicipalityMaster _MunicipalityMaster)
        {
            string qstr = " select MunicipalityName, CountyId from dbo.tblMunicipalityMaster where upper(MunicipalityName) = upper('" + _MunicipalityMaster.MunicipalityName + "')  and CountyId = " + _MunicipalityMaster.CountyId.ToString() + ")";
            DBAccess db = new DBAccess();
            return db.validate(qstr);
        }

        public static int InsertCompany(MunicipalityMaster _MunicipalityMaster)
        {
            string qstr = " INSERT into dbo.tblMunicipalityMaster(MunicipalityName, CountyId)  VALUES('" + _MunicipalityMaster.MunicipalityName + "', '" + _MunicipalityMaster.CountyId + "') ";

            DBAccess db = new DBAccess();
            int i = db.ExecuteNonQuery(qstr);

            return i;
        }

        public static int UpdateCompany(MunicipalityMaster _MunicipalityMaster)
        {
            string qstr = "UPDATE dbo.tblMunicipalityMaster " +
                            " SET  " +
                            "   MunicipalityName = '" + _MunicipalityMaster.MunicipalityName + "'," +
                            "   CountyId = '" + _MunicipalityMaster.CountyId + "'," +
                            "   WHERE " +
                            "   MunicipalityId = " + _MunicipalityMaster.MunicipalityId + "  ";

            DBAccess db = new DBAccess();
            return db.ExecuteNonQuery(qstr);
        }

    }
}