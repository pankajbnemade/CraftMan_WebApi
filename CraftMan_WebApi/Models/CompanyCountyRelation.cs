using CraftMan_WebApi.DataAccessLayer;
using Microsoft.Data.SqlClient;
using System.Collections;

namespace CraftMan_WebApi.Models
{
    public class CompanyCountyRelation
    {
        public int pCompId { get; set; }
        public int CountyId { get; set; }
        public int MunicipalityId { get; set; }
        public string? CompanyName { get; set; }
        public string? CountyName { get; set; }
        public string? MunicipalityName { get; set; }

        public static ArrayList GetRelationDetailByCompany(int CompanyId)
        {
            ArrayList CountyRelationList = new ArrayList();

            DBAccess db = new DBAccess();
            Response strReturn = new Response();

            string qstr = " SELECT tblMunicipalityMaster.MunicipalityName, tblCountyMaster.CountyName, tblCompanyMaster.CompanyName, tblCompanyCountyRel.pCompId, tblCompanyCountyRel.MunicipalityId, tblCompanyCountyRel.CountyId " +
                " FROM   tblCompanyCountyRel " +
                " INNER JOIN  tblCompanyMaster ON tblCompanyCountyRel.pCompId = tblCompanyMaster.pCompId " +
                " LEFT OUTER JOIN  tblCountyMaster ON tblCompanyCountyRel.CountyId = tblCountyMaster.CountyId " +
                " LEFT OUTER JOIN  tblMunicipalityMaster ON tblCompanyCountyRel.MunicipalityId = tblMunicipalityMaster.MunicipalityId " +
                " WHERE tblCompanyCountyRel.pCompId = " + CompanyId + "  ";

            SqlDataReader reader = db.ReadDB(qstr);

            while (reader.Read())
            {
                var pCompanyCountyRelation = new CompanyCountyRelation();

                pCompanyCountyRelation.pCompId = Convert.ToInt32(reader["pCompId"]);
                pCompanyCountyRelation.CountyId = Convert.ToInt32(reader["CountyId"]);
                pCompanyCountyRelation.MunicipalityId = Convert.ToInt32(reader["MunicipalityId"]);
                pCompanyCountyRelation.CompanyName = reader["CompanyName"] == DBNull.Value ? "" : (string)reader["CompanyName"];
                pCompanyCountyRelation.CountyName = reader["CountyName"] == DBNull.Value ? "" : (string)reader["CountyName"];
                pCompanyCountyRelation.MunicipalityName = reader["MunicipalityName"] == DBNull.Value ? "" : (string)reader["MunicipalityName"];

                CountyRelationList.Add(pCompanyCountyRelation);
            }

            reader.Close();

            return CountyRelationList;
        }

        public Response ValidateInsertRecord(CompanyCountyRelation _CompanyCountyRelation)
        {
            string qstr = " select pCompId from tblCompanyCountyRel " +
                        " where tblCompanyCountyRel.pCompId=" + _CompanyCountyRelation.pCompId +
                        " and tblCompanyCountyRel.CountyId = " + _CompanyCountyRelation.CountyId +
                        " and tblCompanyCountyRel.MunicipalityId = " + _CompanyCountyRelation.MunicipalityId;

            DBAccess db = new DBAccess();

            return db.validate(qstr);
        }


        public static int InsertNewRecord(CompanyCountyRelation _CompanyCountyRelation)
        {
            string qstr = " INSERT into tblCompanyCountyRel(pCompId, CountyId, MunicipalityId)  " +
                            " VALUES(" + _CompanyCountyRelation.pCompId + "," + _CompanyCountyRelation.MunicipalityId + "," + _CompanyCountyRelation.CountyId + ") ";

            DBAccess db = new DBAccess();

            int i = db.ExecuteNonQuery(qstr);

            return i;
        }

        public static int DeleteRecord(CompanyCountyRelation _CompanyCountyRelation)
        {
            string qstr = " DELETE FROM dbo.tblCompanyCountyRel " +
                        " where tblCompanyCountyRel.pCompId=" + _CompanyCountyRelation.pCompId +
                        " and tblCompanyCountyRel.CountyId = " + _CompanyCountyRelation.CountyId +
                        " and tblCompanyCountyRel.MunicipalityId = " + _CompanyCountyRelation.MunicipalityId;

            DBAccess db = new DBAccess();

            return db.ExecuteNonQuery(qstr);
        }

    }
}