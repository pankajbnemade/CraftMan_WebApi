using CraftMan_WebApi.DataAccessLayer;
using Microsoft.Data.SqlClient;
using System.Collections;

namespace CraftMan_WebApi.Models
{
    public class CompanyCountyRelation
    {
        public int pCompId { get; set; }
        public int CountyId { get; set; }
        public int? MunicipalityId { get; set; }
        public string? CompanyName { get; set; }
        public string? CountyName { get; set; }
        public string? MunicipalityName { get; set; }

        public static ArrayList GetRelationDetailByCompany(int CompanyId)
        {
            ArrayList CountyRelationList = new ArrayList();

            DBAccess db = new DBAccess();
            Response strReturn = new Response();

            string qstr = " SELECT distinct tblMunicipalityMaster.MunicipalityName, tblCountyMaster.CountyName, tblCompanyMaster.CompanyName, tblCompanyCountyRel.pCompId, tblCompanyCountyRel.MunicipalityId, tblCompanyCountyRel.CountyId " +
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
                pCompanyCountyRelation.MunicipalityId = reader["MunicipalityId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MunicipalityId"]);
                pCompanyCountyRelation.CompanyName = reader["CompanyName"] == DBNull.Value ? "" : (string)reader["CompanyName"];
                pCompanyCountyRelation.CountyName = reader["CountyName"] == DBNull.Value ? "" : (string)reader["CountyName"];
                pCompanyCountyRelation.MunicipalityName = reader["MunicipalityName"] == DBNull.Value ? "" : (string)reader["MunicipalityName"];
                //pCompanyCountyRelation.MunicipalityName = reader["MunicipalityName"] == DBNull.Value ? "All Municipality for " + (reader["CountyName"] == DBNull.Value ? "" : (string)reader["CountyName"]) : (string)reader["MunicipalityName"];

                CountyRelationList.Add(pCompanyCountyRelation);
            }

            reader.Close();
            reader.Dispose();

            return CountyRelationList;
        }

        public Response ValidateInsertRelation(CompanyCountyRelation _CompanyCountyRelation)
        {
            string qstr = " select pCompId from tblCompanyCountyRel " +
                        " where tblCompanyCountyRel.pCompId = " + _CompanyCountyRelation.pCompId +
                        " and tblCompanyCountyRel.CountyId = " + _CompanyCountyRelation.CountyId +
                        " and tblCompanyCountyRel.MunicipalityId = " + _CompanyCountyRelation.MunicipalityId;

            DBAccess db = new DBAccess();

            return db.validate(qstr);
        }

        public static int InsertNewRelation(CompanyCountyRelation _CompanyCountyRelation)
        {
            _CompanyCountyRelation.MunicipalityId = _CompanyCountyRelation.MunicipalityId == 0 ? null : _CompanyCountyRelation.MunicipalityId;

            string municipalityVal = _CompanyCountyRelation.MunicipalityId.HasValue
                        ? _CompanyCountyRelation.MunicipalityId.Value.ToString()
                        : "NULL";

            string qstr = " INSERT into tblCompanyCountyRel(pCompId, CountyId, MunicipalityId)  " +
                            " VALUES(" + _CompanyCountyRelation.pCompId + "," + _CompanyCountyRelation.CountyId + "," + municipalityVal + ") ";

            DBAccess db = new DBAccess();

            int i = db.ExecuteNonQuery(qstr);

            return i;
        }


        public static int InsertNewRelations(List<CompanyCountyRelation> _CompanyCountyRelations)
        {
            try
            {
                if (_CompanyCountyRelations == null || _CompanyCountyRelations.Count == 0)
                    return 0; // No Relations to insert

                string qstr = "INSERT INTO tblCompanyCountyRel (pCompId, CountyId, MunicipalityId) VALUES ";

                List<string> valuesList = new List<string>();

                foreach (var relation in _CompanyCountyRelations)
                {
                    relation.MunicipalityId = relation.MunicipalityId == 0 ? null : relation.MunicipalityId;

                    string municipalityVal = relation.MunicipalityId.HasValue
                        ? relation.MunicipalityId.Value.ToString()
                        : "NULL";

                    string values = $"({relation.pCompId}, {relation.CountyId}, {municipalityVal})";
                    valuesList.Add(values);
                }

                qstr += string.Join(",", valuesList);

                DBAccess db = new DBAccess();
                return db.ExecuteNonQuery(qstr);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
        }

        public static int DeleteRelation(CompanyCountyRelation _CompanyCountyRelation)
        {
            string qstr = " DELETE FROM dbo.tblCompanyCountyRel " +
                        " where tblCompanyCountyRel.pCompId=" + _CompanyCountyRelation.pCompId +
                        " and tblCompanyCountyRel.CountyId = " + _CompanyCountyRelation.CountyId +
                        " and tblCompanyCountyRel.MunicipalityId = " + _CompanyCountyRelation.MunicipalityId;

            DBAccess db = new DBAccess();

            return db.ExecuteNonQuery(qstr);
        }

        public static int DeleteRelations(int CompanyId)
        {
            string qstr = " DELETE FROM dbo.tblCompanyCountyRel " +
                        " where tblCompanyCountyRel.pCompId=" + CompanyId;

            DBAccess db = new DBAccess();

            return db.ExecuteNonQuery(qstr);
        }

    }
}