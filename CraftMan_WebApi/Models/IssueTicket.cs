using CraftMan_WebApi.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace CraftMan_WebApi.Models
{
    public class IssueTicket
    {
        public int TicketId { get; set; }
        public string ReportingPerson { get; set; }
        public string ReportingDescription { get; set; }
        public int OperationId { get; set; }
        public string Status { get; set; }
        public string ToCraftmanType { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
        public int CountyId { get; set; }
        public int MunicipalityId { get; set; }
        public string? CountyName { get; set; }
        public string? MunicipalityName { get; set; }
        public List<IFormFile>? Images { get; set; }
        public List<IssueTicketImage>? IssueTicketImages { get; set; }

        public static Boolean validateticket(IssueTicket _IssueTicket)
        {
            DBAccess db = new DBAccess();

            Response strReturn = new Response();

            string qstr = "select TicketId,ReportingPerson,Address,City, ReportingDescription,Status,ToCraftmanType,Pincode, CountyId, MunicipalityId from dbo.tblIssueTicketMaster where   ToCraftmanType ='" + _IssueTicket.ToCraftmanType + "' and   ReportingPerson='" + _IssueTicket.ReportingPerson + "'   ";
            SqlDataReader reader = db.ReadDB(qstr);

            while (reader.Read())
            {
                var pIssueTicket = new IssueTicket();
                pIssueTicket.TicketId = Convert.ToInt32(reader["TicketId"]);
                if (pIssueTicket.TicketId > 0)
                {
                    return true;
                }
            }

            reader.Close();

            return false;
        }

        public static int InsertTicket(IssueTicket _IssueTicket)
        {
            if (!validateticket(_IssueTicket))
            {
                string qstr = " INSERT INTO tblIssueTicketMaster   (  ReportingPerson, ReportingDescription, OperationId, Status, ToCraftmanType, Address, City, Pincode, CountyId, MunicipalityId) " +
                    " VALUES ( '" + _IssueTicket.ReportingPerson + "', '" + _IssueTicket.ReportingDescription + "', '" + _IssueTicket.OperationId + "','" + _IssueTicket.Status + "','" + _IssueTicket.ToCraftmanType + "','" + _IssueTicket.Address + "','" + _IssueTicket.City + "','" + _IssueTicket.Pincode + "'," + _IssueTicket.CountyId + "," + _IssueTicket.MunicipalityId + ")";

                DBAccess db = new DBAccess();

                return db.ExecuteScalar(qstr);
            }
            return 0;
        }

        public static int InsertTicketImage(IssueTicketImage _IssueTicketImage)
        {
            if (_IssueTicketImage != null)
            {
                string qstr = " INSERT INTO tblIssueTicketImages (TicketId, ImageName, ImagePath) VALUES    " +
                    " ( '" + _IssueTicketImage.TicketId + "', '" + _IssueTicketImage.ImageName + "', '" + _IssueTicketImage.ImagePath + "')";

                DBAccess db = new DBAccess();

                return db.ExecuteNonQuery(qstr);
            }
            return 0;
        }

        public static IssueTicket GetTicketByTicketId(int TicketId)
        {
            DBAccess db = new DBAccess();
            Response strReturn = new Response();

            string qstr = "select TicketId, ReportingPerson, Address, City, ReportingDescription,Status,ToCraftmanType,Pincode, dbo.tblIssueTicketMaster.CountyId, dbo.tblIssueTicketMaster.MunicipalityId, dbo.tblCountyMaster.CountyName, dbo.tblMunicipalityMaster.MunicipalityName " +
                            " FROM dbo.tblIssueTicketMaster " +
                            " LEFT OUTER JOIN DBO.tblCountyMaster ON DBO.tblCountyMaster.CountyId = dbo.tblIssueTicketMaster.CountyId " +
                            " LEFT OUTER JOIN DBO.tblMunicipalityMaster ON DBO.tblMunicipalityMaster.MunicipalityId = dbo.tblIssueTicketMaster.MunicipalityId" +
                        " where  TicketId=" + TicketId + "   ";

            SqlDataReader reader = db.ReadDB(qstr);
            var pIssueTicket = new IssueTicket();

            while (reader.Read())
            {
                pIssueTicket.TicketId = Convert.ToInt32(reader["TicketId"]);
                pIssueTicket.ReportingPerson = (string)reader["ReportingPerson"];// Convert.ToInt32(reader["TicketId"]);
                pIssueTicket.ReportingDescription = (string)reader["ReportingDescription"];
                pIssueTicket.Status = (string)reader["Status"];
                pIssueTicket.ToCraftmanType = (string)reader["ToCraftmanType"];
                pIssueTicket.Address = (string)reader["Address"];
                pIssueTicket.City = (string)reader["City"];
                pIssueTicket.Pincode = reader["Pincode"].ToString();
                pIssueTicket.CountyId = Convert.ToInt32(reader["CountyId"]);
                pIssueTicket.MunicipalityId = Convert.ToInt32(reader["MunicipalityId"]);
                pIssueTicket.CountyName = reader["CountyName"].ToString();
                pIssueTicket.MunicipalityName = reader["MunicipalityName"].ToString();
            }

            reader.Close();


            qstr = "select ImageId, TicketId, ImageName, ImagePath " +
                    " FROM tblIssueTicketImages " +
                    " where  TicketId=" + TicketId + "   ";

            reader = db.ReadDB(qstr);


            pIssueTicket.IssueTicketImages = new List<IssueTicketImage>();

            while (reader.Read())
            {
                var pIssueTicketImage = new IssueTicketImage();

                pIssueTicketImage.TicketId = pIssueTicket.TicketId;
                pIssueTicketImage.ImageId = Convert.ToInt32(reader["ImageId"]);
                pIssueTicketImage.ImageName = (string)reader["ImageName"];
                pIssueTicketImage.ImagePath = (string)reader["ImagePath"];

                pIssueTicket.IssueTicketImages.Add(pIssueTicketImage);
            }

            reader.Close();


            return pIssueTicket;

        }

        public static ArrayList GetTicketsByUser(string _User)
        {
            ArrayList IssueTicketList = new ArrayList();

            DBAccess db = new DBAccess();
            Response strReturn = new Response();

            string qstr = "select TicketId,ReportingPerson,Address,City, ReportingDescription,Status,ToCraftmanType,Pincode, dbo.tblIssueTicketMaster.CountyId, dbo.tblIssueTicketMaster.MunicipalityId, dbo.tblCountyMaster.CountyName, dbo.tblMunicipalityMaster.MunicipalityName " +
                            " FROM dbo.tblIssueTicketMaster " +
                            " LEFT OUTER JOIN DBO.tblCountyMaster ON DBO.tblCountyMaster.CountyId = dbo.tblIssueTicketMaster.CountyId " +
                            " LEFT OUTER JOIN DBO.tblMunicipalityMaster ON DBO.tblMunicipalityMaster.MunicipalityId = dbo.tblIssueTicketMaster.MunicipalityId" +
                            " where  ReportingPerson='" + _User + "'   ";
            SqlDataReader reader = db.ReadDB(qstr);

            while (reader.Read())
            {
                var pIssueTicket = new IssueTicket();
                pIssueTicket.TicketId = Convert.ToInt32(reader["TicketId"]);
                pIssueTicket.ReportingPerson = (string)reader["ReportingPerson"];
                pIssueTicket.ReportingDescription = (string)reader["ReportingDescription"];
                pIssueTicket.Status = (string)reader["Status"];
                pIssueTicket.Address = (string)reader["Address"];
                pIssueTicket.City = (string)reader["City"];
                pIssueTicket.ToCraftmanType = (string)reader["ToCraftmanType"];
                pIssueTicket.Pincode = reader["Pincode"].ToString();
                pIssueTicket.CountyId = Convert.ToInt32(reader["CountyId"]);
                pIssueTicket.MunicipalityId = Convert.ToInt32(reader["MunicipalityId"]);
                pIssueTicket.CountyName = reader["CountyName"].ToString();
                pIssueTicket.MunicipalityName = reader["MunicipalityName"].ToString();

                IssueTicketList.Add(pIssueTicket);
            }

            reader.Close();

            return IssueTicketList;
        }

        public static ArrayList GetTicketsByCompany(int CompanyId, int CountyId, int MunicipalityId)
        {
            ArrayList IssueTicketList = new ArrayList();

            DBAccess db = new DBAccess();
            Response strReturn = new Response();

            string qstr = " SELECT DISTINCT TicketId, ReportingPerson, Address, City, ReportingDescription, Status,ToCraftmanType,Pincode, " +
                " tblIssueTicketMaster.CountyId, tblIssueTicketMaster.MunicipalityId, tblCountyMaster.CountyName,  " +
                " tblMunicipalityMaster.MunicipalityName " +
                " FROM tblIssueTicketMaster " +
                " INNER JOIN( " +
                " SELECT CountyId, MunicipalityId " +
                " FROM tblCompanyCountyRel  WHERE tblCompanyCountyRel.pCompId = " + CompanyId + " ) AS tRel " +
                " ON tRel.CountyId = tblIssueTicketMaster.CountyId AND tRel.MunicipalityId = tblIssueTicketMaster.MunicipalityId " +
                " LEFT OUTER JOIN tblCountyMaster ON tblIssueTicketMaster.CountyId = tblCountyMaster.CountyId " +
                " LEFT OUTER JOIN tblMunicipalityMaster ON tblIssueTicketMaster.MunicipalityId = tblMunicipalityMaster.MunicipalityId  " +
                " WHERE (" + CountyId + " == 0 OR tblIssueTicketMaster.CountyId = " + CountyId + " )" +
                    " AND (" + MunicipalityId + " == 0 OR tblIssueTicketMaster.MunicipalityId = " + MunicipalityId + " )";


            SqlDataReader reader = db.ReadDB(qstr);

            while (reader.Read())
            {
                var pIssueTicket = new IssueTicket();
                pIssueTicket.TicketId = Convert.ToInt32(reader["TicketId"]);
                pIssueTicket.ReportingPerson = (string)reader["ReportingPerson"];
                pIssueTicket.ReportingDescription = (string)reader["ReportingDescription"];
                pIssueTicket.Status = (string)reader["Status"];
                pIssueTicket.Address = (string)reader["Address"];
                pIssueTicket.City = (string)reader["City"];
                pIssueTicket.ToCraftmanType = (string)reader["ToCraftmanType"];
                pIssueTicket.Pincode = reader["Pincode"].ToString();
                pIssueTicket.CountyId = Convert.ToInt32(reader["CountyId"]);
                pIssueTicket.MunicipalityId = Convert.ToInt32(reader["MunicipalityId"]);
                pIssueTicket.CountyName = reader["CountyName"].ToString();
                pIssueTicket.MunicipalityName = reader["MunicipalityName"].ToString();

                IssueTicketList.Add(pIssueTicket);
            }

            reader.Close();

            return IssueTicketList;
        }


    }
}
