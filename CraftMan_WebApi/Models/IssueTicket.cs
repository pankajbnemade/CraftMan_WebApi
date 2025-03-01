using CraftMan_WebApi.DataAccessLayer;
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

        // public List<IFormFile> ImageData { get; set; } //new imgs to add in folder of webapi solution
        public static Boolean validateticket(IssueTicket _IssueTicket) {

            DBAccess db = new DBAccess();
            Response strReturn = new Response();
            string qstr = "select TicketId,ReportingPerson,Address,City, ReportingDescription,Status,ToCraftmanType,Pincode from dbo.tblIssueTicketMaster where   ToCraftmanType ='"+ _IssueTicket.ToCraftmanType + "' and   ReportingPerson='" + _IssueTicket.ReportingPerson + "'   ";
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
                string qstr = " INSERT INTO [Craftman].[dbo].[tblIssueTicketMaster]     (  ReportingPerson, ReportingDescription, OperationId, Status, ToCraftmanType, Address, City, Pincode) VALUES     ( '" + _IssueTicket.ReportingPerson + "', '" + _IssueTicket.ReportingDescription + "', '" + _IssueTicket.OperationId + "','" + _IssueTicket.Status + "','" + _IssueTicket.ToCraftmanType + "','" + _IssueTicket.Address + "','" + _IssueTicket.City + "','" + _IssueTicket.Pincode + "')";

                DBAccess db = new DBAccess();

                return db.ExecuteNonQuery(qstr);
            }
            return 0;
        }
        public static IssueTicket GetTicketByUser(int TicketId)
        {
            
            DBAccess db = new DBAccess();
            Response strReturn = new Response();
            string qstr = "select TicketId,ReportingPerson,Address,City, ReportingDescription,Status,ToCraftmanType,Pincode from dbo.tblIssueTicketMaster where  TicketId=" + TicketId + "   ";
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
            }

            reader.Close();

            return pIssueTicket;

        }

        public static ArrayList GetTicketsByUser(string _User )
        {
            ArrayList IssueTicketList = new ArrayList();
            DBAccess db = new DBAccess();
            Response strReturn = new Response();
            string qstr = "select TicketId,ReportingPerson,Address,City, ReportingDescription,Status,ToCraftmanType,Pincode from dbo.tblIssueTicketMaster where  ReportingPerson='" + _User +"'   ";
            SqlDataReader reader = db.ReadDB(qstr);
          
            while (reader.Read())
            {
                var pIssueTicket = new IssueTicket();
                pIssueTicket.TicketId =Convert.ToInt32( reader["TicketId"]);
                pIssueTicket.ReportingPerson = (string)reader["ReportingPerson"];
                pIssueTicket.ReportingDescription = (string)reader["ReportingDescription"] ;
                pIssueTicket.Status = (string)reader["Status"];
                pIssueTicket.Address = (string)reader["Address"];
                pIssueTicket.City = (string)reader["City"];
                pIssueTicket.ToCraftmanType = (string)reader["ToCraftmanType"];
                pIssueTicket.Pincode =  reader["Pincode"].ToString();
                IssueTicketList.Add(pIssueTicket);
            }

            reader.Close();

            return IssueTicketList;

        }

    }
}
