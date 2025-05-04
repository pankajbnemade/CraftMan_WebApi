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
        public int? AcceptedOTP { get; set; }
        public int? ClosingOTP { get; set; }
        public int? CompanyId { get; set; }
        public string? CompanyEmailId { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyMobileNumber { get; set; }
        public int? UserId { get; set; }
        public string? UserEmailId { get; set; }
        public string? UserName { get; set; }
        public string? UserMobileNumber { get; set; }

        public List<IFormFile>? Images { get; set; }
        public List<IssueTicketImage>? TicketImages { get; set; }
        public List<IssueTicketImage>? TicketWorkImages { get; set; }

        public static Boolean validateticket(IssueTicket _IssueTicket)
        {
            DBAccess db = new DBAccess();

            Response strReturn = new Response();

            string qstr = @"select TicketId from tblIssueTicketMaster 
                            where ToCraftmanType = '" + _IssueTicket.ToCraftmanType + "' and ReportingPerson='" + _IssueTicket.ReportingPerson + "'   ";

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
            reader.Dispose();

            return false;
        }

        public static int InsertTicket(IssueTicket _IssueTicket)
        {
            try
            {
                Random random = new Random();

                int acceptedOTP = random.Next(1000, 10000);

                string qstr = " INSERT INTO tblIssueTicketMaster   (ReportingPerson, ReportingDescription, OperationId, Status, " +
                            " ToCraftmanType, Address, City, Pincode, CountyId, MunicipalityId, AcceptedOTP, CreatedOn) " +
                            " VALUES ( '" + _IssueTicket.ReportingPerson.Trim() + "', '" + _IssueTicket.ReportingDescription + "', '" + _IssueTicket.OperationId +
                            "','" + _IssueTicket.Status + "','" + _IssueTicket.ToCraftmanType + "','" + _IssueTicket.Address + "','" + _IssueTicket.City + "','" +
                            _IssueTicket.Pincode + "'," + _IssueTicket.CountyId + "," + _IssueTicket.MunicipalityId +
                            ", " + acceptedOTP + " ," + " getdate()" + ")";

                DBAccess db = new DBAccess();

                return db.ExecuteScalar(qstr);
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
                            "   Status = '" + _IssueTicketUpdateStatus.Status + "'";

            if (_IssueTicketUpdateStatus.Status == TicketStatus.Accepted.ToString())
            {
                qstr = qstr + " , CompanyId =  " + Convert.ToInt32(_IssueTicketUpdateStatus.CompanyId);
            }

            qstr = qstr + "   WHERE " +
                    " TicketId = " + _IssueTicketUpdateStatus.TicketId + "  ";

            DBAccess db = new DBAccess();

            return db.ExecuteNonQuery(qstr);
        }

        public static Boolean ValidateClosingOTP(IssueTicketUpdateStatus _IssueTicketUpdateStatus)
        {
            DBAccess db = new DBAccess();

            Response strReturn = new Response();

            string qstr = " select ISNULL(ClosingOTP, 0) AS ClosingOTP from tblIssueTicketMaster where  TicketId=" + _IssueTicketUpdateStatus.TicketId;

            SqlDataReader reader = db.ReadDB(qstr);

            while (reader.Read())
            {
                if (_IssueTicketUpdateStatus.OTP == Convert.ToInt32(reader["ClosingOTP"]) && Convert.ToInt32(reader["ClosingOTP"]) != 0)
                {
                    return true;
                }
            }

            reader.Close();
            reader.Dispose();

            return false;
        }

        public static Boolean ValidateAcceptedOTP(IssueTicketUpdateStatus _IssueTicketUpdateStatus)
        {
            DBAccess db = new DBAccess();

            Response strReturn = new Response();

            string qstr = " select ISNULL(AcceptedOTP, 0) AS AcceptedOTP from tblIssueTicketMaster where  TicketId=" + _IssueTicketUpdateStatus.TicketId;

            SqlDataReader reader = db.ReadDB(qstr);

            while (reader.Read())
            {
                if (_IssueTicketUpdateStatus.OTP == Convert.ToInt32(reader["AcceptedOTP"]) && Convert.ToInt32(reader["AcceptedOTP"]) != 0)
                {
                    return true;
                }
            }

            reader.Close();
            reader.Dispose();

            return false;
        }

        public static IssueTicket GetTicketByTicketId(int TicketId)
        {

            DBAccess db = new DBAccess();
            Response strReturn = new Response();

            string qstr = "select tblIssueTicketMaster.TicketId, tblIssueTicketMaster.ReportingPerson, tblIssueTicketMaster.Address, tblIssueTicketMaster.City, " +
                            " tblIssueTicketMaster.ReportingDescription,tblIssueTicketMaster.Status,tblIssueTicketMaster.ToCraftmanType,tblIssueTicketMaster.Pincode, " +
                            " tblIssueTicketMaster.CountyId, tblIssueTicketMaster.MunicipalityId, tblIssueTicketMaster.CreatedOn, tblIssueTicketMaster.UpdatedOn, " +
                            " tblIssueTicketMaster.ReviewComment, tblIssueTicketMaster.ReviewStarRating, tblIssueTicketMaster.CompanyComment, AcceptedOTP, ClosingOTP, " +
                            " tblCountyMaster.CountyName, tblMunicipalityMaster.MunicipalityName, " +
                            " tblIssueTicketMaster.CompanyId, tblCompanyMaster.EmailId as CompanyEmailId, tblCompanyMaster.MobileNumber as CompanyMobileNumber, tblCompanyMaster.CompanyName, " +
                            " tblUserMaster.pkey_UId AS UserId, tblUserMaster.EmailId AS UserEmailId, tblUserMaster.MobileNumber AS UserMobileNumber, tblUserMaster.Username" +
                            " FROM tblIssueTicketMaster " +
                            " LEFT OUTER JOIN tblCountyMaster ON tblCountyMaster.CountyId = tblIssueTicketMaster.CountyId " +
                            " LEFT OUTER JOIN tblMunicipalityMaster ON tblMunicipalityMaster.MunicipalityId = tblIssueTicketMaster.MunicipalityId" +
                            " LEFT OUTER JOIN tblCompanyMaster ON tblCompanyMaster.pCompId = tblIssueTicketMaster.CompanyId " +
                            " LEFT OUTER JOIN tblUserMaster ON upper(tblUserMaster.Username) = upper(tblIssueTicketMaster.ReportingPerson) " +
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
                pIssueTicket.AcceptedOTP = reader["AcceptedOTP"] == DBNull.Value ? null : Convert.ToInt32(reader["AcceptedOTP"]);
                pIssueTicket.ClosingOTP = reader["ClosingOTP"] == DBNull.Value ? null : Convert.ToInt32(reader["ClosingOTP"]);

                pIssueTicket.CompanyId = reader["CompanyId"] == DBNull.Value ? null : Convert.ToInt32(reader["CompanyId"]);
                pIssueTicket.CompanyMobileNumber = reader["CompanyMobileNumber"] == DBNull.Value ? "" : (string)reader["CompanyMobileNumber"];
                pIssueTicket.CompanyEmailId = reader["CompanyEmailId"] == DBNull.Value ? "" : (string)reader["CompanyEmailId"];
                pIssueTicket.CompanyName = reader["CompanyName"] == DBNull.Value ? "" : (string)reader["CompanyName"];

                pIssueTicket.UserId = reader["UserId"] == DBNull.Value ? null : Convert.ToInt32(reader["UserId"]);
                pIssueTicket.UserMobileNumber = reader["UserMobileNumber"] == DBNull.Value ? "" : (string)reader["UserMobileNumber"];
                pIssueTicket.UserEmailId = reader["UserEmailId"] == DBNull.Value ? "" : (string)reader["UserEmailId"];
                pIssueTicket.UserName = reader["UserName"] == DBNull.Value ? "" : (string)reader["UserName"];

                pIssueTicket.CreatedOn = reader["CreatedOn"] == DBNull.Value ? null : (DateTime)reader["CreatedOn"];
                pIssueTicket.UpdatedOn = reader["UpdatedOn"] == DBNull.Value ? null : (DateTime)reader["UpdatedOn"];
            }

            reader.Close();
            reader.Dispose();


            qstr = "select ImageId, TicketId, ImageName, ImagePath " +
                    " FROM tblIssueTicketImages " +
                    " where  TicketId=" + TicketId + "   ";

            reader = db.ReadDB(qstr);


            ImageSettings pImageSettings = new ImageSettings();

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
                    pIssueTicketImage.ImagePath = pIssueTicketImage.ImagePath.Replace("\\", "/");

                    if (System.IO.File.Exists(pIssueTicketImage.ImagePath))
                    {
                        pIssueTicketImage.ImageContentType = CommonFunction.GetContentType(pIssueTicketImage.ImagePath);
                        //pServiceMaster.ImageFileBytes = System.IO.File.ReadAllBytes(pServiceMaster.ImagePath);
                        //pIssueTicketImage.ImageBase64String = Convert.ToBase64String(System.IO.File.ReadAllBytes(pIssueTicketImage.ImagePath));

                        pIssueTicketImage.ImagePath = pIssueTicketImage.ImagePath.Replace(pImageSettings.StoragePath, pImageSettings.BaseUrl);
                    }
                    else
                    {
                        pIssueTicketImage.ImageContentType = CommonFunction.GetContentType(pImageSettings.DefaultImageUrl);
                        pIssueTicketImage.ImagePath = pImageSettings.DefaultImageUrl;
                    }
                }


                pIssueTicket.TicketImages.Add(pIssueTicketImage);
            }

            reader.Close();
            reader.Dispose();

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
                    pIssueTicketImage.ImagePath = pIssueTicketImage.ImagePath.Replace("\\", "/");

                    if (System.IO.File.Exists(pIssueTicketImage.ImagePath))
                    {
                        pIssueTicketImage.ImageContentType = CommonFunction.GetContentType(pIssueTicketImage.ImagePath);

                        pIssueTicketImage.ImagePath = pIssueTicketImage.ImagePath.Replace(pImageSettings.StoragePath, pImageSettings.BaseUrl);
                    }
                    else
                    {
                        pIssueTicketImage.ImageContentType = CommonFunction.GetContentType(pImageSettings.DefaultImageUrl);
                        pIssueTicketImage.ImagePath = pImageSettings.DefaultImageUrl;
                    }
                }

                pIssueTicket.TicketWorkImages.Add(pIssueTicketImage);
            }

            reader.Close();
            reader.Dispose();

            return pIssueTicket;


        }

        public static ArrayList GetTicketsByUser(IssueTicketForUserFilter filter)
        {
            ArrayList IssueTicketList = new ArrayList();

            DBAccess db = new DBAccess();
            Response strReturn = new Response();

            string qstr = "select TicketId,ReportingPerson,Address,City, ReportingDescription,Status,ToCraftmanType,Pincode, " +
                            " tblIssueTicketMaster.CountyId, tblIssueTicketMaster.MunicipalityId,  tblIssueTicketMaster.CreatedOn, tblIssueTicketMaster.UpdatedOn, " +
                            " tblIssueTicketMaster.ReviewComment, tblIssueTicketMaster.ReviewStarRating, tblIssueTicketMaster.CompanyComment, AcceptedOTP, ClosingOTP, " +
                            " tblCountyMaster.CountyName, tblMunicipalityMaster.MunicipalityName, " +
                            " tblIssueTicketMaster.CompanyId, tblCompanyMaster.EmailId as CompanyEmailId, tblCompanyMaster.MobileNumber as CompanyMobileNumber, tblCompanyMaster.CompanyName, " +
                            " tblUserMaster.pkey_UId AS UserId, tblUserMaster.EmailId AS UserEmailId, tblUserMaster.MobileNumber AS UserMobileNumber, tblUserMaster.Username" +
                            " FROM tblIssueTicketMaster " +
                            " LEFT OUTER JOIN tblCountyMaster ON tblCountyMaster.CountyId = tblIssueTicketMaster.CountyId " +
                            " LEFT OUTER JOIN tblMunicipalityMaster ON tblMunicipalityMaster.MunicipalityId = tblIssueTicketMaster.MunicipalityId" +
                            " LEFT OUTER JOIN tblCompanyMaster ON tblCompanyMaster.pCompId = tblIssueTicketMaster.CompanyId " +
                            " LEFT OUTER JOIN tblUserMaster ON upper(tblUserMaster.Username) = upper(tblIssueTicketMaster.ReportingPerson) " +
                            " WHERE  tblUserMaster.pkey_UId = " + filter.UserId;


            if (filter.ServiceId != null && filter.ServiceId != 0)
            {
                ServiceMaster serviceMaster = ServiceMaster.GetServiceDetail(filter.ServiceId);

                if (serviceMaster != null)
                {
                    qstr = qstr + " AND tblIssueTicketMaster.ToCraftmanType like '%" + serviceMaster.ServiceName + "%' ";
                }
            }


            if (filter.Status != null && filter.Status != "" )
            {
                if (filter.Status.ToUpper() != "ALL")
                {
                    qstr = qstr + " AND (upper(tblIssueTicketMaster.Status) = upper('" + filter.Status + "'))";
                }
            }


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
                pIssueTicket.AcceptedOTP = reader["AcceptedOTP"] == DBNull.Value ? null : Convert.ToInt32(reader["AcceptedOTP"]);
                pIssueTicket.ClosingOTP = reader["ClosingOTP"] == DBNull.Value ? null : Convert.ToInt32(reader["ClosingOTP"]);

                pIssueTicket.CompanyId = reader["CompanyId"] == DBNull.Value ? null : Convert.ToInt32(reader["CompanyId"]);
                pIssueTicket.CompanyMobileNumber = reader["CompanyMobileNumber"] == DBNull.Value ? "" : (string)reader["CompanyMobileNumber"];
                pIssueTicket.CompanyEmailId = reader["CompanyEmailId"] == DBNull.Value ? "" : (string)reader["CompanyEmailId"];
                pIssueTicket.CompanyName = reader["CompanyName"] == DBNull.Value ? "" : (string)reader["CompanyName"];

                pIssueTicket.UserId = reader["UserId"] == DBNull.Value ? null : Convert.ToInt32(reader["UserId"]);
                pIssueTicket.UserMobileNumber = reader["UserMobileNumber"] == DBNull.Value ? "" : (string)reader["UserMobileNumber"];
                pIssueTicket.UserEmailId = reader["UserEmailId"] == DBNull.Value ? "" : (string)reader["UserEmailId"];
                pIssueTicket.UserName = reader["UserName"] == DBNull.Value ? "" : (string)reader["UserName"];

                pIssueTicket.CreatedOn = reader["CreatedOn"] == DBNull.Value ? null : (DateTime)reader["CreatedOn"];
                pIssueTicket.UpdatedOn = reader["UpdatedOn"] == DBNull.Value ? null : (DateTime)reader["UpdatedOn"];

                IssueTicketList.Add(pIssueTicket);
            }

            reader.Close();
            reader.Dispose();

            ImageSettings pImageSettings = new ImageSettings();

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
                        pIssueTicketImage.ImagePath = pIssueTicketImage.ImagePath.Replace("\\", "/");

                        if (System.IO.File.Exists(pIssueTicketImage.ImagePath))
                        {
                            pIssueTicketImage.ImageContentType = CommonFunction.GetContentType(pIssueTicketImage.ImagePath);

                            pIssueTicketImage.ImagePath = pIssueTicketImage.ImagePath.Replace(pImageSettings.StoragePath, pImageSettings.BaseUrl);
                        }
                        else
                        {
                            pIssueTicketImage.ImageContentType = CommonFunction.GetContentType(pImageSettings.DefaultImageUrl);
                            pIssueTicketImage.ImagePath = pImageSettings.DefaultImageUrl;
                        }
                    }

                    issueTicket.TicketImages.Add(pIssueTicketImage);
                }

                reader.Close();
                reader.Dispose();

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
                        pIssueTicketImage.ImagePath = pIssueTicketImage.ImagePath.Replace("\\", "/");

                        if (System.IO.File.Exists(pIssueTicketImage.ImagePath))
                        {
                            pIssueTicketImage.ImageContentType = CommonFunction.GetContentType(pIssueTicketImage.ImagePath);

                            pIssueTicketImage.ImagePath = pIssueTicketImage.ImagePath.Replace(pImageSettings.StoragePath, pImageSettings.BaseUrl);
                        }
                        else
                        {
                            pIssueTicketImage.ImageContentType = CommonFunction.GetContentType(pImageSettings.DefaultImageUrl);
                            pIssueTicketImage.ImagePath = pImageSettings.DefaultImageUrl;
                        }
                    }

                    issueTicket.TicketWorkImages.Add(pIssueTicketImage);
                }

                reader.Close();
                reader.Dispose();
            }



            return IssueTicketList;
        }

        public static ArrayList GetTicketsForCompany(IssueTicketForCompanyFilter filter)
        {
            ArrayList IssueTicketList = new ArrayList();

            DBAccess db = new DBAccess();

            Response strReturn = new Response();

            string qstr = " SELECT DISTINCT TicketId, ReportingPerson, Address, City, ReportingDescription, Status,ToCraftmanType,Pincode, " +
                " tblIssueTicketMaster.CountyId, tblIssueTicketMaster.MunicipalityId, tblIssueTicketMaster.CreatedOn, tblIssueTicketMaster.UpdatedOn, " +
                " tblIssueTicketMaster.ReviewComment, tblIssueTicketMaster.ReviewStarRating, tblIssueTicketMaster.CompanyComment, AcceptedOTP, ClosingOTP, " +
                " tblCountyMaster.CountyName, tblMunicipalityMaster.MunicipalityName, " +
                " tblIssueTicketMaster.CompanyId, tblCompanyMaster.EmailId as CompanyEmailId, tblCompanyMaster.MobileNumber as CompanyMobileNumber, tblCompanyMaster.CompanyName, " +
                " tblUserMaster.pkey_UId AS UserId, tblUserMaster.EmailId AS UserEmailId, tblUserMaster.MobileNumber AS UserMobileNumber, tblUserMaster.Username" +
                " FROM tblIssueTicketMaster " +
                " INNER JOIN( " +
                " SELECT CountyId, MunicipalityId " +
                " FROM tblCompanyCountyRel  WHERE tblCompanyCountyRel.pCompId = " + filter.CompanyId + " ) AS tRel " +
                " ON tRel.CountyId = tblIssueTicketMaster.CountyId AND tRel.MunicipalityId = tblIssueTicketMaster.MunicipalityId " +
                " INNER JOIN( " +
                " SELECT ServiceName " +
                " FROM tblCompanyServices INNER JOIN tblServiceMaster on tblServiceMaster.ServiceId = tblCompanyServices.ServiceId WHERE tblCompanyServices.pCompId = " + filter.CompanyId + ") AS tServices " +
                " ON tblIssueTicketMaster.ToCraftmanType like '%' + tServices.ServiceName + '%' " +
                " LEFT OUTER JOIN tblCountyMaster ON tblIssueTicketMaster.CountyId = tblCountyMaster.CountyId " +
                " LEFT OUTER JOIN tblMunicipalityMaster ON tblIssueTicketMaster.MunicipalityId = tblMunicipalityMaster.MunicipalityId " +
                " LEFT OUTER JOIN tblCompanyMaster ON tblCompanyMaster.pCompId = tblIssueTicketMaster.CompanyId " +
                " LEFT OUTER JOIN tblUserMaster ON upper(tblUserMaster.Username) = upper(tblIssueTicketMaster.ReportingPerson) " +
                " WHERE (" + (filter.CountyId == null ? 0 : filter.CountyId) + " = 0 OR tblIssueTicketMaster.CountyId = " + (filter.CountyId == null ? 0 : filter.CountyId) + " )" +
                    " AND (" + (filter.MunicipalityId == null ? 0 : filter.MunicipalityId) + " = 0 OR tblIssueTicketMaster.MunicipalityId = " + (filter.MunicipalityId == null ? 0 : filter.MunicipalityId) + " )";


            if (filter.ServiceId != null && filter.ServiceId != 0)
            {
                ServiceMaster serviceMaster = ServiceMaster.GetServiceDetail(filter.ServiceId);

                if (serviceMaster != null)
                {
                    qstr = qstr + " AND tblIssueTicketMaster.ToCraftmanType like '%" + serviceMaster.ServiceName + "%' ";
                }
            }

            if (filter.Status.ToString().ToUpper() == TicketStatus.Created.ToString().ToUpper())
            {
                qstr = qstr + " AND (upper(tblIssueTicketMaster.Status) = upper('" + filter.Status + "'))";
            }

            if (filter.Status.ToString().ToUpper() != TicketStatus.Created.ToString().ToUpper())
            {
                qstr = qstr + " AND (upper(tblIssueTicketMaster.Status) = upper('" + filter.Status + "') AND isnull(tblIssueTicketMaster.CompanyId, 0) = " + filter.CompanyId + " ) ";
            }

            //-------------------

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
                pIssueTicket.AcceptedOTP = reader["AcceptedOTP"] == DBNull.Value ? null : Convert.ToInt32(reader["AcceptedOTP"]);
                pIssueTicket.ClosingOTP = reader["ClosingOTP"] == DBNull.Value ? null : Convert.ToInt32(reader["ClosingOTP"]);

                pIssueTicket.CompanyId = reader["CompanyId"] == DBNull.Value ? null : Convert.ToInt32(reader["CompanyId"]);
                pIssueTicket.CompanyMobileNumber = reader["CompanyMobileNumber"] == DBNull.Value ? "" : (string)reader["CompanyMobileNumber"];
                pIssueTicket.CompanyEmailId = reader["CompanyEmailId"] == DBNull.Value ? "" : (string)reader["CompanyEmailId"];
                pIssueTicket.CompanyName = reader["CompanyName"] == DBNull.Value ? "" : (string)reader["CompanyName"];

                pIssueTicket.UserId = reader["UserId"] == DBNull.Value ? null : Convert.ToInt32(reader["UserId"]);
                pIssueTicket.UserMobileNumber = reader["UserMobileNumber"] == DBNull.Value ? "" : (string)reader["UserMobileNumber"];
                pIssueTicket.UserEmailId = reader["UserEmailId"] == DBNull.Value ? "" : (string)reader["UserEmailId"];
                pIssueTicket.UserName = reader["UserName"] == DBNull.Value ? "" : (string)reader["UserName"];

                pIssueTicket.CreatedOn = reader["CreatedOn"] == DBNull.Value ? null : (DateTime)reader["CreatedOn"];
                pIssueTicket.UpdatedOn = reader["UpdatedOn"] == DBNull.Value ? null : (DateTime)reader["UpdatedOn"];

                IssueTicketList.Add(pIssueTicket);
            }

            reader.Close();
            reader.Dispose();

            ImageSettings pImageSettings = new ImageSettings();

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
                        pIssueTicketImage.ImagePath = pIssueTicketImage.ImagePath.Replace("\\", "/");

                        if (System.IO.File.Exists(pIssueTicketImage.ImagePath))
                        {
                            pIssueTicketImage.ImageContentType = CommonFunction.GetContentType(pIssueTicketImage.ImagePath);

                            pIssueTicketImage.ImagePath = pIssueTicketImage.ImagePath.Replace(pImageSettings.StoragePath, pImageSettings.BaseUrl);
                        }
                        else
                        {
                            pIssueTicketImage.ImageContentType = CommonFunction.GetContentType(pImageSettings.DefaultImageUrl);
                            pIssueTicketImage.ImagePath = pImageSettings.DefaultImageUrl;
                        }
                    }

                    issueTicket.TicketImages.Add(pIssueTicketImage);
                }

                reader.Close();
                reader.Dispose();

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
                        pIssueTicketImage.ImagePath = pIssueTicketImage.ImagePath.Replace("\\", "/");

                        if (System.IO.File.Exists(pIssueTicketImage.ImagePath))
                        {
                            pIssueTicketImage.ImageContentType = CommonFunction.GetContentType(pIssueTicketImage.ImagePath);

                            pIssueTicketImage.ImagePath = pIssueTicketImage.ImagePath.Replace(pImageSettings.StoragePath, pImageSettings.BaseUrl);
                        }
                        else
                        {
                            pIssueTicketImage.ImageContentType = CommonFunction.GetContentType(pImageSettings.DefaultImageUrl);
                            pIssueTicketImage.ImagePath = pImageSettings.DefaultImageUrl;
                        }
                    }

                    issueTicket.TicketWorkImages.Add(pIssueTicketImage);
                }

                reader.Close();
                reader.Dispose();
            }


            return IssueTicketList;
        }


        public static List<string> GetCompanyDeviceTokenList(Int32 TicketId)
        {
            Response strReturn = new Response();
            string qstr;
            string serviceIdList;
            SqlDataReader reader;
            List<string> tokenList = new();

            DBAccess db = new DBAccess();

            qstr = @" select DISTINCT tblServiceMaster.ServiceId
                    from tblIssueTicketMaster
                    LEFT OUTER JOIN tblServiceMaster ON tblIssueTicketMaster.ToCraftmanType like '%' + tblServiceMaster.ServiceName + '%'
                    where tblIssueTicketMaster.TicketId = " + TicketId;

            reader = db.ReadDB(qstr);

            serviceIdList = "";

            while (reader.Read())
            {
                if (serviceIdList == "")
                {
                    serviceIdList = (reader["ServiceId"] == DBNull.Value ? "" : reader["ServiceId"].ToString());
                }
                else
                {
                    serviceIdList = serviceIdList + "," + (reader["ServiceId"] == DBNull.Value ? "" : reader["ServiceId"].ToString());
                }
            }

            reader.Close();
            reader.Dispose();


            int userId = 0;

            qstr = @" select  tblUserMaster.pkey_UId
                    from	tblIssueTicketMaster
                    LEFT OUTER JOIN tblUserMaster ON tblIssueTicketMaster.ReportingPerson = tblUserMaster.Username
                    where tblIssueTicketMaster.TicketId = " + TicketId;

            reader = db.ReadDB(qstr);

            while (reader.Read())
            {
                userId = reader["pkey_UId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["pkey_UId"]);
            }

            reader.Close();
            reader.Dispose();

            qstr = @" SELECT	DISTINCT tblCompanyMaster.pCompId 
                    FROM   tblCompanyMaster  
                    INNER JOIN tblCompanyServices ON tblCompanyMaster.pCompId = tblCompanyServices.pCompId  
                    INNER JOIN tblCompanyCountyRel ON tblCompanyMaster.pCompId = tblCompanyCountyRel.pCompId  
                    INNER JOIN tblUserMaster ON tblUserMaster.CountyId = tblCompanyCountyRel.CountyId  
                    AND(tblUserMaster.MunicipalityId = tblCompanyCountyRel.MunicipalityId OR isnull(tblCompanyCountyRel.MunicipalityId, 0) = 0)  
                    WHERE tblUserMaster.pkey_UId = " + userId.ToString();

            if (serviceIdList != "")
            {
                qstr = qstr
                    + " AND tblCompanyServices.ServiceId in (" + serviceIdList + ") ";
            }

            reader = db.ReadDB(qstr);

            string companyIdList = "";

            while (reader.Read())
            {
                if (companyIdList == "")
                {
                    companyIdList = (reader["pCompId"] == DBNull.Value ? "" : reader["pCompId"].ToString());
                }
                else
                {
                    companyIdList = companyIdList + "," + (reader["pCompId"] == DBNull.Value ? "" : reader["pCompId"].ToString());
                }
            }

            reader.Close();
            reader.Dispose();

            if (companyIdList != "")
            {
                qstr = @" SELECT Id, pCompId, Token, Platform, RegisteredOn
                    FROM   tblCompanyUserDevices  
                    WHERE pCompId IN (" + companyIdList + ")";

                reader = db.ReadDB(qstr);

                //ArrayList DeviceTokenModelList = new ArrayList();


                while (reader.Read())
                {
                    //DeviceTokenModel pDeviceTokenModel = new DeviceTokenModel();

                    //pDeviceTokenModel.pCompId = reader["pCompId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["pCompId"]);
                    //pDeviceTokenModel.Token = reader["Token"] == DBNull.Value ? "" : (string)reader["Token"];
                    //pDeviceTokenModel.Platform = reader["Platform"] == DBNull.Value ? "" : (string)reader["Platform"];

                    //DeviceTokenModelList.Add(pDeviceTokenModel);

                    tokenList.Add(reader["Token"] == DBNull.Value ? "" : (string)reader["Token"]);
                }

                reader.Close();
                reader.Dispose();
            }

            return tokenList;
        }


    }
}
