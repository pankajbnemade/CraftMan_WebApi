using CraftMan_WebApi.DataAccessLayer;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.Data.SqlClient;
using System.Collections;

namespace CraftMan_WebApi.Models
{
    public class IssueTicketChat
    {
        public int ChatId { get; set; }
        public DateTime ChatDateTime { get; set; } = DateTime.UtcNow;
        public int TicketId { get; set; }
        public int? CompanyId { get; set; }
        public int? UserId { get; set; }
        public string? Message { get; set; }
        public string? CompanyUserName { get; set; }
        public string? UserName { get; set; }

        public static int InsertIssueTicketChat(IssueTicketChat _IssueTicketChat)
        {
            var qstr = "INSERT INTO tblIssueTicketChat (TicketId, CompanyId, UserId, Message, ChatDateTime) " +
                    "VALUES ('" + _IssueTicketChat.TicketId + "','" + _IssueTicketChat.CompanyId +
                    "','" + _IssueTicketChat.UserId + "','" + _IssueTicketChat.Message +
                    "',GETDATE())";

            DBAccess db = new DBAccess();

            return db.ExecuteScalar(qstr);
        }

        public static ArrayList GetChatMessagesByTicketId(int TicketId)
        {
            ArrayList pIssueTicketList = new ArrayList();

            DBAccess db = new DBAccess();
            Response strReturn = new Response();

            string qstr = " SELECT    ChatId, ChatDateTime, TicketId, CompanyId, UserId, Message, " +
                        " tblCompanyMaster.Username AS CompanyUserName,  tblUserMaster.Username As UserName" +
                        " FROM    tblIssueTicketChat" +
                        " LEFT OUTER JOIN tblUserMaster on tblIssueTicketChat.UserId = tblUserMaster.pkey_UId" +
                        " LEFT OUTER JOIN tblCompanyMaster on tblIssueTicketChat.CompanyId = tblCompanyMaster.pCompId" +
                        " WHERE TicketId = " + TicketId +
                        " ORDER BY ChatDateTime";

            SqlDataReader reader = db.ReadDB(qstr);

            while (reader.Read())
            {
                var pIssueTicketChat = new IssueTicketChat();

                pIssueTicketChat.ChatId = Convert.ToInt32(reader["ChatId"]);
                pIssueTicketChat.ChatDateTime = (DateTime)reader["ChatDateTime"];
                pIssueTicketChat.TicketId = Convert.ToInt32(reader["TicketId"]);
                pIssueTicketChat.CompanyId = Convert.ToInt32(reader["CompanyId"]);
                pIssueTicketChat.UserId = Convert.ToInt32(reader["UserId"]);
                pIssueTicketChat.Message = (string)reader["Message"];
                pIssueTicketChat.CompanyUserName = (string)(reader["CompanyUserName"] == DBNull.Value ? "" : reader["CompanyUserName"]);
                pIssueTicketChat.UserName = (string)(reader["UserName"] == DBNull.Value ? "" : reader["UserName"]);

                pIssueTicketList.Add(pIssueTicketChat);
            }

            reader.Close();

            return pIssueTicketList;
        }

    }
}