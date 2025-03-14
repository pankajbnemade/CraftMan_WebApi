using CraftMan_WebApi.DataAccessLayer;
using CraftMan_WebApi.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Design;
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
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? CountyName { get; set; }
        public string? MunicipalityName { get; set; }
        public int? ReviewStarRating { get; set; }
        public string? ReviewComment { get; set; }
        public string? CompanyComment { get; set; }
        public int? ClosingOTP { get; set; }
        public List<IFormFile>? Images { get; set; }
        public List<IssueTicketImage>? TicketImages { get; set; }
        public List<IssueTicketImage>? TicketWorkImages { get; set; }

        public static Boolean validateticket(IssueTicket _IssueTicket)
        {
            DBAccess db = new DBAccess();

            Response strReturn = new Response();

            string qstr = "select TicketId,ReportingPerson,Address,City, ReportingDescription,Status,ToCraftmanType,Pincode, CountyId, MunicipalityId from tblIssueTicketMaster where   ToCraftmanType ='" + _IssueTicket.ToCraftmanType + "' and   ReportingPerson='" + _IssueTicket.ReportingPerson + "'   ";

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
            try
            {
                if (!validateticket(_IssueTicket))
                {
                    string qstr = " INSERT INTO tblIssueTicketMaster   (  ReportingPerson, ReportingDescription, OperationId, Status, ToCraftmanType, Address, City, Pincode, CountyId, MunicipalityId) " +
                        " VALUES ( '" + _IssueTicket.ReportingPerson.Trim() + "', '" + _IssueTicket.ReportingDescription + "', '" + _IssueTicket.OperationId + "','" + _IssueTicket.Status + "','" + _IssueTicket.ToCraftmanType + "','" + _IssueTicket.Address + "','" + _IssueTicket.City + "','" + _IssueTicket.Pincode + "'," + _IssueTicket.CountyId + "," + _IssueTicket.MunicipalityId + ")";

                    DBAccess db = new DBAccess();

                    return db.ExecuteScalar(qstr);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
            return 0;
        }

        public static int InsertTicketImage(IssueTicketImage _IssueTicketImage)
        {
            try
            {
                if (_IssueTicketImage != null)
                {
                    string qstr = " INSERT INTO tblIssueTicketImages (TicketId, ImageName, ImagePath) VALUES    " +
                        " ( '" + _IssueTicketImage.TicketId + "', '" + _IssueTicketImage.ImageName + "', '" + _IssueTicketImage.ImagePath + "')";

                    DBAccess db = new DBAccess();

                    return db.ExecuteNonQuery(qstr);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
            return 0;
        }

        public static int InsertTicketWorkImage(IssueTicketImage _IssueTicketImage)
        {
            try
            {
                if (_IssueTicketImage != null)
                {
                    string qstr = " INSERT INTO tblIssueTicketWorkImages (TicketId, ImageName, ImagePath) VALUES    " +
                        " ( '" + _IssueTicketImage.TicketId + "', '" + _IssueTicketImage.ImageName + "', '" + _IssueTicketImage.ImagePath + "')";

                    DBAccess db = new DBAccess();

                    return db.ExecuteNonQuery(qstr);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }

            return 0;
        }

        public static int UpdateTicketReview(IssueTicketReview _IssueTicketReview)
        {

            string qstr = " UPDATE tblIssueTicketMaster " +
                            " SET  " +
                            "   ReviewStarRating = " + _IssueTicketReview.ReviewStarRating + ", " +
                            "   ReviewComment = '" + _IssueTicketReview.ReviewComment + "'" +
                            "   WHERE " +
                            "   TicketId = " + _IssueTicketReview.TicketId + "  ";

            DBAccess db = new DBAccess();

            return db.ExecuteNonQuery(qstr);

            return 0;
        }

        public static int UpdateCompanyComment(IssueTicketCompanyComment _IssueTicketCompanyComment)
        {

            Random random = new Random();

            int closingOTP = random.Next(1000, 10000);

            string qstr = " UPDATE tblIssueTicketMaster " +
                            " SET  " +
                            "   CompanyComment = '" + _IssueTicketCompanyComment.CompanyComment + "', " +
                            "   ClosingOTP = " + closingOTP +
                            "   WHERE " +
                            "   TicketId = " + _IssueTicketCompanyComment.TicketId + "  ";

            DBAccess db = new DBAccess();

            return db.ExecuteNonQuery(qstr);

            return 0;
        }

        public static int UpdateTicketStatus(IssueTicketUpdateStatus _IssueTicketUpdateStatus)
        {
            string qstr = " UPDATE tblIssueTicketMaster " +
                            " SET  " +
                            "   Status = '" + _IssueTicketUpdateStatus.Status + "'" +
                            "   WHERE " +
                            "   TicketId = " + _IssueTicketUpdateStatus.TicketId + "  ";

            DBAccess db = new DBAccess();

            return db.ExecuteNonQuery(qstr);

            return 0;
        }

        public static Boolean ValidateClosingOTP(IssueTicketUpdateStatus _IssueTicketUpdateStatus)
        {
            DBAccess db = new DBAccess();

            Response strReturn = new Response();

            string qstr = " select ISNULL(ClosingOTP, 0) AS ClosingOTP from tblIssueTicketMaster where  TicketId=" + _IssueTicketUpdateStatus.TicketId;

            SqlDataReader reader = db.ReadDB(qstr);

            while (reader.Read())
            {
                if (_IssueTicketUpdateStatus.ClosingOTP == Convert.ToInt32(reader["ClosingOTP"]) && Convert.ToInt32(reader["ClosingOTP"]) != 0)
                {
                    return true;
                }
            }

            reader.Close();

            return false;
        }

        public static IssueTicket GetTicketByTicketId(int TicketId)
        {
            try
            {
                DBAccess db = new DBAccess();
                Response strReturn = new Response();

                string qstr = "select TicketId, ReportingPerson, Address, City, ReportingDescription,Status,ToCraftmanType,Pincode, " +
                                " tblIssueTicketMaster.CountyId, tblIssueTicketMaster.MunicipalityId, tblIssueTicketMaster.CreatedOn, tblIssueTicketMaster.UpdatedOn, " +
                                " tblIssueTicketMaster.ReviewComment, tblIssueTicketMaster.ReviewStarRating, tblIssueTicketMaster.CompanyComment, ClosingOTP, " +
                                " tblCountyMaster.CountyName, tblMunicipalityMaster.MunicipalityName " +
                                " FROM tblIssueTicketMaster " +
                                " LEFT OUTER JOIN tblCountyMaster ON tblCountyMaster.CountyId = tblIssueTicketMaster.CountyId " +
                                " LEFT OUTER JOIN tblMunicipalityMaster ON tblMunicipalityMaster.MunicipalityId = tblIssueTicketMaster.MunicipalityId" +
                            " where  TicketId = " + TicketId + "   ";

                SqlDataReader reader = db.ReadDB(qstr);

                var pIssueTicket = new IssueTicket();

                while (reader.Read())
                {
                    pIssueTicket.TicketId = Convert.ToInt32(reader["TicketId"]);
                    pIssueTicket.ReportingPerson = reader["ReportingPerson"] == DBNull.Value ? "" : (string)reader["ReportingPerson"];
                    pIssueTicket.ReportingDescription = reader["ReportingDescription"] == DBNull.Value ? "" : (string)reader["ReportingDescription"];
                    pIssueTicket.Status = reader["Status"] == DBNull.Value ? "" : (string)reader["Status"];
                    pIssueTicket.Address = reader["Address"] == DBNull.Value ? "" : (string)reader["Address"];
                    pIssueTicket.City = reader["City"] == DBNull.Value ? "" : (string)reader["City"];
                    pIssueTicket.ToCraftmanType = reader["ToCraftmanType"] == DBNull.Value ? "" : (string)reader["ToCraftmanType"];
                    pIssueTicket.Pincode = reader["Pincode"] == DBNull.Value ? "" : reader["Pincode"].ToString();
                    pIssueTicket.CountyId = Convert.ToInt32(reader["CountyId"]);
                    pIssueTicket.MunicipalityId = Convert.ToInt32(reader["MunicipalityId"]);
                    pIssueTicket.CountyName = reader["CountyName"] == DBNull.Value ? "" : reader["CountyName"].ToString();
                    pIssueTicket.MunicipalityName = reader["MunicipalityName"] == DBNull.Value ? "" : reader["MunicipalityName"].ToString();
                    pIssueTicket.ReviewStarRating = reader["ReviewStarRating"] == DBNull.Value ? null : Convert.ToInt32(reader["ReviewStarRating"]);
                    pIssueTicket.ReviewComment = reader["ReviewComment"] == DBNull.Value ? "" : reader["ReviewComment"].ToString();
                    pIssueTicket.CompanyComment = reader["CompanyComment"] == DBNull.Value ? "" : reader["CompanyComment"].ToString();
                    pIssueTicket.ClosingOTP = reader["ClosingOTP"] == DBNull.Value ? null : Convert.ToInt32(reader["ClosingOTP"]);

                    pIssueTicket.CreatedOn = reader["CreatedOn"] == DBNull.Value ? null : (DateTime)reader["CreatedOn"];
                    pIssueTicket.UpdatedOn = reader["UpdatedOn"] == DBNull.Value ? null : (DateTime)reader["UpdatedOn"];
                }

                reader.Close();


                qstr = "select ImageId, TicketId, ImageName, ImagePath " +
                        " FROM tblIssueTicketImages " +
                        " where  TicketId=" + TicketId + "   ";

                reader = db.ReadDB(qstr);


                pIssueTicket.TicketImages = new List<IssueTicketImage>();

                while (reader.Read())
                {
                    var pIssueTicketImage = new IssueTicketImage();

                    pIssueTicketImage.TicketId = pIssueTicket.TicketId;
                    pIssueTicketImage.ImageId = Convert.ToInt32(reader["ImageId"]);
                    pIssueTicketImage.ImageName = reader["ImageName"] == DBNull.Value ? "" : (string)reader["ImageName"];
                    pIssueTicketImage.ImagePath = reader["ImagePath"] == DBNull.Value ? "" : (string)reader["ImagePath"];

                    if (pIssueTicketImage.ImagePath != "")
                    {
                        if (System.IO.File.Exists(pIssueTicketImage.ImagePath))
                        {
                            pIssueTicketImage.ImageContentType = CommonFunction.GetContentType(pIssueTicketImage.ImagePath);
                            //pServiceMaster.ImageFileBytes = System.IO.File.ReadAllBytes(pServiceMaster.ImagePath);
                            pIssueTicketImage.ImageBase64String = Convert.ToBase64String(System.IO.File.ReadAllBytes(pIssueTicketImage.ImagePath));
                        }
                    }


                    pIssueTicket.TicketImages.Add(pIssueTicketImage);
                }

                reader.Close();

                qstr = "select ImageId, TicketId, ImageName, ImagePath " +
                        " FROM tblIssueTicketWorkImages " +
                        " where  TicketId=" + TicketId + "   ";

                reader = db.ReadDB(qstr);


                pIssueTicket.TicketWorkImages = new List<IssueTicketImage>();

                while (reader.Read())
                {
                    var pIssueTicketImage = new IssueTicketImage();

                    pIssueTicketImage.TicketId = pIssueTicket.TicketId;
                    pIssueTicketImage.ImageId = Convert.ToInt32(reader["ImageId"]);
                    pIssueTicketImage.ImageName = reader["ImageName"] == DBNull.Value ? "" : (string)reader["ImageName"];
                    pIssueTicketImage.ImagePath = reader["ImagePath"] == DBNull.Value ? "" : (string)reader["ImagePath"];

                    if (pIssueTicketImage.ImagePath != "")
                    {
                        if (System.IO.File.Exists(pIssueTicketImage.ImagePath))
                        {
                            pIssueTicketImage.ImageContentType = CommonFunction.GetContentType(pIssueTicketImage.ImagePath);
                            //pServiceMaster.ImageFileBytes = System.IO.File.ReadAllBytes(pServiceMaster.ImagePath);
                            pIssueTicketImage.ImageBase64String = Convert.ToBase64String(System.IO.File.ReadAllBytes(pIssueTicketImage.ImagePath));
                        }
                    }

                    pIssueTicket.TicketWorkImages.Add(pIssueTicketImage);
                }

                reader.Close();

                return pIssueTicket;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }

        }

        public static ArrayList GetTicketsByUser(string _User)
        {
            ArrayList IssueTicketList = new ArrayList();

            DBAccess db = new DBAccess();
            Response strReturn = new Response();

            string qstr = "select TicketId,ReportingPerson,Address,City, ReportingDescription,Status,ToCraftmanType,Pincode, " +
                            " tblIssueTicketMaster.CountyId, tblIssueTicketMaster.MunicipalityId,  tblIssueTicketMaster.CreatedOn, tblIssueTicketMaster.UpdatedOn, " +
                            " tblIssueTicketMaster.ReviewComment, tblIssueTicketMaster.ReviewStarRating, tblIssueTicketMaster.CompanyComment, ClosingOTP, " +
                            " tblCountyMaster.CountyName, tblMunicipalityMaster.MunicipalityName " +
                            " FROM tblIssueTicketMaster " +
                            " LEFT OUTER JOIN tblCountyMaster ON tblCountyMaster.CountyId = tblIssueTicketMaster.CountyId " +
                            " LEFT OUTER JOIN tblMunicipalityMaster ON tblMunicipalityMaster.MunicipalityId = tblIssueTicketMaster.MunicipalityId" +
                            " where  upper(ReportingPerson) = upper('" + _User.Trim() + "')   ";
            SqlDataReader reader = db.ReadDB(qstr);

            

            while (reader.Read())
            {
                var pIssueTicket = new IssueTicket();

                pIssueTicket.TicketId = Convert.ToInt32(reader["TicketId"]);
                pIssueTicket.ReportingPerson = reader["ReportingPerson"] == DBNull.Value ? "" : (string)reader["ReportingPerson"];
                pIssueTicket.ReportingDescription = reader["ReportingDescription"] == DBNull.Value ? "" : (string)reader["ReportingDescription"];
                pIssueTicket.Status = reader["Status"] == DBNull.Value ? "" : (string)reader["Status"];
                pIssueTicket.Address = reader["Address"] == DBNull.Value ? "" : (string)reader["Address"];
                pIssueTicket.City = reader["City"] == DBNull.Value ? "" : (string)reader["City"];
                pIssueTicket.ToCraftmanType = reader["ToCraftmanType"] == DBNull.Value ? "" : (string)reader["ToCraftmanType"];
                pIssueTicket.Pincode = reader["Pincode"] == DBNull.Value ? "" : reader["Pincode"].ToString();
                pIssueTicket.CountyId = Convert.ToInt32(reader["CountyId"]);
                pIssueTicket.MunicipalityId = Convert.ToInt32(reader["MunicipalityId"]);
                pIssueTicket.CountyName = reader["CountyName"] == DBNull.Value ? "" : reader["CountyName"].ToString();
                pIssueTicket.MunicipalityName = reader["MunicipalityName"] == DBNull.Value ? "" : reader["MunicipalityName"].ToString();
                pIssueTicket.ReviewStarRating = reader["ReviewStarRating"] == DBNull.Value ? null : Convert.ToInt32(reader["ReviewStarRating"]);
                pIssueTicket.ReviewComment = reader["ReviewComment"] == DBNull.Value ? "" : reader["ReviewComment"].ToString();
                pIssueTicket.CompanyComment = reader["CompanyComment"] == DBNull.Value ? "" : reader["CompanyComment"].ToString();
                pIssueTicket.ClosingOTP = reader["ClosingOTP"] == DBNull.Value ? null : Convert.ToInt32(reader["ClosingOTP"]);

                pIssueTicket.CreatedOn = reader["CreatedOn"] == DBNull.Value ? null : (DateTime)reader["CreatedOn"];
                pIssueTicket.UpdatedOn = reader["UpdatedOn"] == DBNull.Value ? null : (DateTime)reader["UpdatedOn"];

                IssueTicketList.Add(pIssueTicket);
            }

            reader.Close();

            foreach (IssueTicket issueTicket in IssueTicketList)
            {
                qstr = "select ImageId, TicketId, ImageName, ImagePath " +
                    " FROM tblIssueTicketImages " +
                    " where  TicketId=" + issueTicket.TicketId + "   ";

                reader = db.ReadDB(qstr);

                issueTicket.TicketImages = new List<IssueTicketImage>();

                while (reader.Read())
                {
                    var pIssueTicketImage = new IssueTicketImage();

                    pIssueTicketImage.TicketId = issueTicket.TicketId;
                    pIssueTicketImage.ImageId = Convert.ToInt32(reader["ImageId"]);
                    pIssueTicketImage.ImageName = reader["ImageName"] == DBNull.Value ? "" : (string)reader["ImageName"];
                    pIssueTicketImage.ImagePath = reader["ImagePath"] == DBNull.Value ? "" : (string)reader["ImagePath"];

                    if (pIssueTicketImage.ImagePath != "")
                    {
                        if (System.IO.File.Exists(pIssueTicketImage.ImagePath))
                        {
                            pIssueTicketImage.ImageContentType = CommonFunction.GetContentType(pIssueTicketImage.ImagePath);
                            //pServiceMaster.ImageFileBytes = System.IO.File.ReadAllBytes(pServiceMaster.ImagePath);
                            pIssueTicketImage.ImageBase64String = Convert.ToBase64String(System.IO.File.ReadAllBytes(pIssueTicketImage.ImagePath));
                        }
                    }

                    issueTicket.TicketImages.Add(pIssueTicketImage);
                }

                reader.Close();

                qstr = "select ImageId, TicketId, ImageName, ImagePath " +
                        " FROM tblIssueTicketWorkImages " +
                        " where  TicketId=" + issueTicket.TicketId + "   ";

                reader = db.ReadDB(qstr);


                issueTicket.TicketWorkImages = new List<IssueTicketImage>();

                while (reader.Read())
                {
                    var pIssueTicketImage = new IssueTicketImage();

                    pIssueTicketImage.TicketId = issueTicket.TicketId;
                    pIssueTicketImage.ImageId = Convert.ToInt32(reader["ImageId"]);
                    pIssueTicketImage.ImageName = reader["ImageName"] == DBNull.Value ? "" : (string)reader["ImageName"];
                    pIssueTicketImage.ImagePath = reader["ImagePath"] == DBNull.Value ? "" : (string)reader["ImagePath"];

                    if (pIssueTicketImage.ImagePath != "")
                    {
                        if (System.IO.File.Exists(pIssueTicketImage.ImagePath))
                        {
                            pIssueTicketImage.ImageContentType = CommonFunction.GetContentType(pIssueTicketImage.ImagePath);
                            //pServiceMaster.ImageFileBytes = System.IO.File.ReadAllBytes(pServiceMaster.ImagePath);
                            pIssueTicketImage.ImageBase64String = Convert.ToBase64String(System.IO.File.ReadAllBytes(pIssueTicketImage.ImagePath));
                        }
                    }

                    issueTicket.TicketWorkImages.Add(pIssueTicketImage);
                }

                reader.Close();
            }



            return IssueTicketList;
        }

        public static ArrayList GetTicketsForCompany(int CompanyId, int? CountyId, int? MunicipalityId)
        {
            ArrayList IssueTicketList = new ArrayList();

            DBAccess db = new DBAccess();
            Response strReturn = new Response();

            string qstr = " SELECT DISTINCT TicketId, ReportingPerson, Address, City, ReportingDescription, Status,ToCraftmanType,Pincode, " +
                " tblIssueTicketMaster.CountyId, tblIssueTicketMaster.MunicipalityId, tblIssueTicketMaster.CreatedOn, tblIssueTicketMaster.UpdatedOn, " +
                " tblIssueTicketMaster.ReviewComment, tblIssueTicketMaster.ReviewStarRating, tblIssueTicketMaster.CompanyComment, ClosingOTP, " +
                " tblCountyMaster.CountyName, tblMunicipalityMaster.MunicipalityName " +
                " FROM tblIssueTicketMaster " +
                " INNER JOIN( " +
                " SELECT CountyId, MunicipalityId " +
                " FROM tblCompanyCountyRel  WHERE tblCompanyCountyRel.pCompId = " + CompanyId + " ) AS tRel " +
                " ON tRel.CountyId = tblIssueTicketMaster.CountyId AND tRel.MunicipalityId = tblIssueTicketMaster.MunicipalityId " +
                " LEFT OUTER JOIN tblCountyMaster ON tblIssueTicketMaster.CountyId = tblCountyMaster.CountyId " +
                " LEFT OUTER JOIN tblMunicipalityMaster ON tblIssueTicketMaster.MunicipalityId = tblMunicipalityMaster.MunicipalityId  " +
                " WHERE (" + (CountyId == null ? 0 : CountyId) + " = 0 OR tblIssueTicketMaster.CountyId = " + (CountyId == null ? 0 : CountyId) + " )" +
                    " AND (" + (MunicipalityId == null ? 0 : MunicipalityId) + " = 0 OR tblIssueTicketMaster.MunicipalityId = " + (MunicipalityId == null ? 0 : MunicipalityId) + " )" +
                    " AND ( upper(tblIssueTicketMaster.Status) = upper('" + TicketStatus.Created.ToString() + "') )";


            SqlDataReader reader = db.ReadDB(qstr);

            while (reader.Read())
            {
                var pIssueTicket = new IssueTicket();
                pIssueTicket.TicketId = Convert.ToInt32(reader["TicketId"]);
                pIssueTicket.ReportingPerson = reader["ReportingPerson"] == DBNull.Value ? "" : (string)reader["ReportingPerson"];
                pIssueTicket.ReportingDescription = reader["ReportingDescription"] == DBNull.Value ? "" : (string)reader["ReportingDescription"];
                pIssueTicket.Status = reader["Status"] == DBNull.Value ? "" : (string)reader["Status"];
                pIssueTicket.Address = reader["Address"] == DBNull.Value ? "" : (string)reader["Address"];
                pIssueTicket.City = reader["City"] == DBNull.Value ? "" : (string)reader["City"];
                pIssueTicket.ToCraftmanType = reader["ToCraftmanType"] == DBNull.Value ? "" : (string)reader["ToCraftmanType"];
                pIssueTicket.Pincode = reader["Pincode"] == DBNull.Value ? "" : reader["Pincode"].ToString();
                pIssueTicket.CountyId = Convert.ToInt32(reader["CountyId"]);
                pIssueTicket.MunicipalityId = Convert.ToInt32(reader["MunicipalityId"]);
                pIssueTicket.CountyName = reader["CountyName"] == DBNull.Value ? "" : reader["CountyName"].ToString();
                pIssueTicket.MunicipalityName = reader["MunicipalityName"] == DBNull.Value ? "" : reader["MunicipalityName"].ToString();
                pIssueTicket.ReviewStarRating = reader["ReviewStarRating"] == DBNull.Value ? null : Convert.ToInt32(reader["ReviewStarRating"]);
                pIssueTicket.ReviewComment = reader["ReviewComment"] == DBNull.Value ? "" : reader["ReviewComment"].ToString();
                pIssueTicket.CompanyComment = reader["CompanyComment"] == DBNull.Value ? "" : reader["CompanyComment"].ToString();
                pIssueTicket.ClosingOTP = reader["ClosingOTP"] == DBNull.Value ? null : Convert.ToInt32(reader["ClosingOTP"]);

                pIssueTicket.CreatedOn = reader["CreatedOn"] == DBNull.Value ? null : (DateTime)reader["CreatedOn"];
                pIssueTicket.UpdatedOn = reader["UpdatedOn"] == DBNull.Value ? null : (DateTime)reader["UpdatedOn"];

                IssueTicketList.Add(pIssueTicket);
            }

            reader.Close();


            foreach (IssueTicket issueTicket in IssueTicketList)
            {
                qstr = "select ImageId, TicketId, ImageName, ImagePath " +
                    " FROM tblIssueTicketImages " +
                    " where  TicketId=" + issueTicket.TicketId + "   ";

                reader = db.ReadDB(qstr);


                issueTicket.TicketImages = new List<IssueTicketImage>();

                while (reader.Read())
                {
                    var pIssueTicketImage = new IssueTicketImage();

                    pIssueTicketImage.TicketId = issueTicket.TicketId;
                    pIssueTicketImage.ImageId = Convert.ToInt32(reader["ImageId"]);
                    pIssueTicketImage.ImageName = reader["ImageName"] == DBNull.Value ? "" : (string)reader["ImageName"];
                    pIssueTicketImage.ImagePath = reader["ImagePath"] == DBNull.Value ? "" : (string)reader["ImagePath"];

                    if (pIssueTicketImage.ImagePath != "")
                    {
                        if (System.IO.File.Exists(pIssueTicketImage.ImagePath))
                        {
                            pIssueTicketImage.ImageContentType = CommonFunction.GetContentType(pIssueTicketImage.ImagePath);
                            //pServiceMaster.ImageFileBytes = System.IO.File.ReadAllBytes(pServiceMaster.ImagePath);
                            pIssueTicketImage.ImageBase64String = Convert.ToBase64String(System.IO.File.ReadAllBytes(pIssueTicketImage.ImagePath));
                        }
                    }

                    issueTicket.TicketImages.Add(pIssueTicketImage);
                }

                reader.Close();

                qstr = "select ImageId, TicketId, ImageName, ImagePath " +
                        " FROM tblIssueTicketWorkImages " +
                        " where  TicketId=" + issueTicket.TicketId + "   ";

                reader = db.ReadDB(qstr);


                issueTicket.TicketWorkImages = new List<IssueTicketImage>();

                while (reader.Read())
                {
                    var pIssueTicketImage = new IssueTicketImage();

                    pIssueTicketImage.TicketId = issueTicket.TicketId;
                    pIssueTicketImage.ImageId = Convert.ToInt32(reader["ImageId"]);
                    pIssueTicketImage.ImageName = reader["ImageName"] == DBNull.Value ? "" : (string)reader["ImageName"];
                    pIssueTicketImage.ImagePath = reader["ImagePath"] == DBNull.Value ? "" : (string)reader["ImagePath"];

                    if (pIssueTicketImage.ImagePath != "")
                    {
                        if (System.IO.File.Exists(pIssueTicketImage.ImagePath))
                        {
                            pIssueTicketImage.ImageContentType = CommonFunction.GetContentType(pIssueTicketImage.ImagePath);
                            //pServiceMaster.ImageFileBytes = System.IO.File.ReadAllBytes(pServiceMaster.ImagePath);
                            pIssueTicketImage.ImageBase64String = Convert.ToBase64String(System.IO.File.ReadAllBytes(pIssueTicketImage.ImagePath));
                        }
                    }

                    issueTicket.TicketWorkImages.Add(pIssueTicketImage);
                }

                reader.Close();
            }


            return IssueTicketList;
        }

    }
}
