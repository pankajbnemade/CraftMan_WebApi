using CraftMan_WebApi.DataAccessLayer;
using Microsoft.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace CraftMan_WebApi.Models
{
    public class MunicipalityMaster
    {
        public int MunicipalityId { get; set; }
        public string MunicipalityName { get; set; }
        public int CountyId { get; set; }
        public string? CountyName { get; set; }
        public static MunicipalityMaster GetMunicipalityDetail(int? MunicipalityId)
        {
            DBAccess db = new DBAccess();
            Response strReturn = new Response();

            string qstr = "  SELECT  tblMunicipalityMaster.MunicipalityId, tblMunicipalityMaster.CountyId, tblMunicipalityMaster.MunicipalityName, tblCountyMaster.CountyName " +
                " FROM  dbo.tblMunicipalityMaster " +
                " LEFT OUTER JOIN  dbo.tblCountyMaster ON dbo.tblMunicipalityMaster.CountyId = dbo.tblCountyMaster.CountyId" +
                " where MunicipalityId = "
                        + MunicipalityId.ToString() + "  ";

            SqlDataReader reader = db.ReadDB(qstr);

            var pMunicipalityMaster = new MunicipalityMaster();

            while (reader.Read())
            {
                pMunicipalityMaster.MunicipalityId = Convert.ToInt32(reader["MunicipalityId"]);
                pMunicipalityMaster.MunicipalityName = reader["MunicipalityName"] == DBNull.Value ? "" : (string)reader["MunicipalityName"];
                pMunicipalityMaster.CountyId = Convert.ToInt32(reader["CountyId"]);
                pMunicipalityMaster.CountyName = reader["CountyName"] == DBNull.Value ? "" : (string)reader["CountyName"];
            }

            reader.Close();
            reader.Dispose();

            return pMunicipalityMaster;
        }

        public static ArrayList GetMunicipalityList(int CountyId)
        {
            ArrayList MunicipalityList = new ArrayList();

            DBAccess db = new DBAccess();
            Response strReturn = new Response();

            string qstr = "  SELECT  MunicipalityId, CountyId, MunicipalityName FROM  dbo.tblMunicipalityMaster where CountyId=" + CountyId.ToString() + " or  0 = " + CountyId.ToString();

            SqlDataReader reader = db.ReadDB(qstr);

            while (reader.Read())
            {
                var pMunicipalityMaster = new MunicipalityMaster();

                pMunicipalityMaster.MunicipalityId = Convert.ToInt32(reader["MunicipalityId"]);
                pMunicipalityMaster.MunicipalityName = reader["MunicipalityName"] == DBNull.Value ? "" : (string)reader["MunicipalityName"];
                pMunicipalityMaster.CountyId = Convert.ToInt32(reader["CountyId"]);

                MunicipalityList.Add(pMunicipalityMaster);
            }

            reader.Close();
            reader.Dispose();

            return MunicipalityList;
        }

        public static List<MunicipalityMaster> GetMunicipalityList(string[]? MunicipalityIdList)
        {
            List<MunicipalityMaster> MunicipalityList = new List<MunicipalityMaster>();

            DBAccess db = new DBAccess();
            Response strReturn = new Response();

            if (MunicipalityIdList != null && MunicipalityIdList.Any())
            {
                List<string> MunicipalityIdListTrim = new List<string>();

                foreach (string Id in MunicipalityIdList)
                {
                    MunicipalityIdListTrim.Add(Id.Trim().Trim('"'));
                }

                string MunicipalityIdStr = MunicipalityIdListTrim != null ? string.Join(",", MunicipalityIdListTrim) : string.Empty;

                string qstr = "  SELECT  tblMunicipalityMaster.MunicipalityId, tblMunicipalityMaster.CountyId, tblMunicipalityMaster.MunicipalityName, tblCountyMaster.CountyName " +
                    " FROM  dbo.tblMunicipalityMaster " +
                    " LEFT OUTER JOIN  dbo.tblCountyMaster ON dbo.tblMunicipalityMaster.CountyId = dbo.tblCountyMaster.CountyId" +
                    " where MunicipalityId in (" + MunicipalityIdStr + ")  ";

                SqlDataReader reader = db.ReadDB(qstr);


                while (reader.Read())
                {
                    var pMunicipalityMaster = new MunicipalityMaster();

                    pMunicipalityMaster.MunicipalityId = Convert.ToInt32(reader["MunicipalityId"]);
                    pMunicipalityMaster.MunicipalityName = reader["MunicipalityName"] == DBNull.Value ? "" : (string)reader["MunicipalityName"];
                    pMunicipalityMaster.CountyId = Convert.ToInt32(reader["CountyId"]);
                    pMunicipalityMaster.CountyName = reader["CountyName"] == DBNull.Value ? "" : (string)reader["CountyName"];

                    MunicipalityList.Add(pMunicipalityMaster);
                }

                reader.Close();
                reader.Dispose();
            }
            return MunicipalityList;
        }


        public static ArrayList GetMunicipalityListByCompanyId(int CountyId, int CompanyId)
        {
            ArrayList MunicipalityList = new ArrayList();

            DBAccess db = new DBAccess();
            Response strReturn = new Response();

            string qstr = @"  SELECT  tblMunicipalityMaster.MunicipalityId, tblMunicipalityMaster.MunicipalityName,  
                            tblMunicipalityMaster.CountyId, tblCompanyCountyRel.pCompId, tblCountyMaster.CountyName
                            FROM  tblCompanyCountyRel
                            INNER JOIN tblCountyMaster ON tblCountyMaster.CountyId = tblCompanyCountyRel.CountyId
                            INNER JOIN tblMunicipalityMaster 
                                ON (
									tblCountyMaster.CountyId = tblMunicipalityMaster.CountyId 
									AND  (tblCompanyCountyRel.MunicipalityId = tblMunicipalityMaster.MunicipalityId OR tblCompanyCountyRel.MunicipalityId IS NULL)
								)" +
                        " WHERE (tblCompanyCountyRel.CountyId=" + CountyId.ToString() + " OR  0 = " + CountyId.ToString() + ") " +
                        " AND tblCompanyCountyRel.pCompId = " + CompanyId + " ";

            SqlDataReader reader = db.ReadDB(qstr);

            while (reader.Read())
            {
                var pMunicipalityMaster = new MunicipalityMaster();

                pMunicipalityMaster.MunicipalityId = Convert.ToInt32(reader["MunicipalityId"]);
                pMunicipalityMaster.MunicipalityName = reader["MunicipalityName"] == DBNull.Value ? "" : (string)reader["MunicipalityName"];
                pMunicipalityMaster.CountyId = Convert.ToInt32(reader["CountyId"]);
                pMunicipalityMaster.CountyName = reader["CountyName"] == DBNull.Value ? "" : (string)reader["CountyName"];

                MunicipalityList.Add(pMunicipalityMaster);
            }

            reader.Close();
            reader.Dispose();

            return MunicipalityList;
        }

        public static Boolean ValidateMunicipality(MunicipalityMaster _MunicipalityMaster)
        {
            DBAccess db = new DBAccess();

            Response strReturn = new Response();

            string qstr = " select MunicipalityName, CountyId from dbo.tblMunicipalityMaster where upper(MunicipalityName) = upper('" + _MunicipalityMaster.MunicipalityName + "')  and CountyId = " + _MunicipalityMaster.CountyId.ToString() + "";

            SqlDataReader reader = db.ReadDB(qstr);

            while (reader.Read())
            {
                return true;
            }

            reader.Close();
            reader.Dispose();

            return false;
        }

        public static Boolean ValidateUpdateMunicipality(MunicipalityMaster _MunicipalityMaster)
        {
            DBAccess db = new DBAccess();

            Response strReturn = new Response();

            string qstr = " select MunicipalityName, CountyId from dbo.tblMunicipalityMaster where upper(MunicipalityName) = upper('" + _MunicipalityMaster.MunicipalityName + "')  and MunicipalityId != " + _MunicipalityMaster.MunicipalityId.ToString() + " and CountyId = " + _MunicipalityMaster.CountyId.ToString() + "";

            SqlDataReader reader = db.ReadDB(qstr);

            while (reader.Read())
            {
                return true;
            }

            reader.Close();
            reader.Dispose();

            return false;
        }

        public static int InsertMunicipality(MunicipalityMaster _MunicipalityMaster)
        {
            string qstr = " INSERT into dbo.tblMunicipalityMaster(MunicipalityName, CountyId)  " +
                " VALUES('" + _MunicipalityMaster.MunicipalityName + "', '" + _MunicipalityMaster.CountyId + "') ";

            DBAccess db = new DBAccess();
            int i = db.ExecuteNonQuery(qstr);

            return i;
        }

        public static int UpdateMunicipality(MunicipalityMaster _MunicipalityMaster)
        {
            string qstr = "UPDATE dbo.tblMunicipalityMaster " +
                            " SET  " +
                            "   MunicipalityName = '" + _MunicipalityMaster.MunicipalityName + "'," +
                            "   CountyId = '" + _MunicipalityMaster.CountyId + "'" +
                            "   WHERE " +
                            "   MunicipalityId = " + _MunicipalityMaster.MunicipalityId + "  ";

            DBAccess db = new DBAccess();
            return db.ExecuteNonQuery(qstr);
        }

    }
}