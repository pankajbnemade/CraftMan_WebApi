using CraftMan_WebApi.DataAccessLayer;
using Microsoft.Data.SqlClient;
using System.Collections;

namespace CraftMan_WebApi.Models
{
    public class MunicipalityMaster
    {
        public int MunicipalityId { get; set; }
        public string MunicipalityName { get; set; }
        public int CountyId { get; set; }

        public static MunicipalityMaster GetMunicipalityDetail(int? MunicipalityId)
        {
            DBAccess db = new DBAccess();
            Response strReturn = new Response();

            string qstr = "  SELECT  MunicipalityId, CountyId, MunicipalityName FROM  dbo.tblMunicipalityMaster where MunicipalityId="
                        + MunicipalityId.ToString() + "  ";

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

            string qstr = "  SELECT  MunicipalityId, CountyId, MunicipalityName FROM  dbo.tblMunicipalityMaster where CountyId=" + CountyId.ToString() + " or  0 = " + CountyId.ToString();

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

        public static ArrayList GetMunicipalityListByCompanyId(int CountyId, int CompanyId)
        {
            ArrayList MunicipalityList = new ArrayList();

            DBAccess db = new DBAccess();
            Response strReturn = new Response();

            string qstr = "  SELECT  tblMunicipalityMaster.MunicipalityId, tblMunicipalityMaster.MunicipalityName, tblMunicipalityMaster.CountyId, tblCompanyCountyRel.pCompId " +
                        " FROM  tblMunicipalityMaster " +
                        " INNER JOIN tblCompanyCountyRel ON tblCompanyCountyRel.MunicipalityId = tblMunicipalityMaster.MunicipalityId " +
                        " where (tblMunicipalityMaster.CountyId=" + CountyId.ToString() + " or  0 = " + CountyId.ToString() + ") " +
                        " and tblCompanyCountyRel.pCompId = " + CompanyId + " ";

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

        public Response ValidateMunicipality(MunicipalityMaster _MunicipalityMaster)
        {
            string qstr = " select MunicipalityName, CountyId from dbo.tblMunicipalityMaster where upper(MunicipalityName) = upper('" + _MunicipalityMaster.MunicipalityName + "')  and CountyId = " + _MunicipalityMaster.CountyId.ToString() + "";
            DBAccess db = new DBAccess();
            return db.validate(qstr);
        }

        public Response ValidateUpdateMunicipality(MunicipalityMaster _MunicipalityMaster)
        {
            string qstr = " select MunicipalityName, CountyId from dbo.tblMunicipalityMaster where upper(MunicipalityName) = upper('" + _MunicipalityMaster.MunicipalityName + "')  and MunicipalityId != " + _MunicipalityMaster.MunicipalityId.ToString() + " and CountyId = " + _MunicipalityMaster.CountyId.ToString() + "";
            DBAccess db = new DBAccess();
            return db.validate(qstr);
        }


        public static int InsertMunicipality(MunicipalityMaster _MunicipalityMaster)
        {
            string qstr = " INSERT into dbo.tblMunicipalityMaster(MunicipalityName, CountyId)  VALUES('" + _MunicipalityMaster.MunicipalityName + "', '" + _MunicipalityMaster.CountyId + "') ";

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