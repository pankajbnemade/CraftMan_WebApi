﻿using CraftMan_WebApi.DataAccessLayer;
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
            _IssueTicketChat.CompanyId = _IssueTicketChat.CompanyId == 0 ? null : _IssueTicketChat.CompanyId;
            _IssueTicketChat.UserId = _IssueTicketChat.UserId == 0 ? null : _IssueTicketChat.UserId;

            string companyIdVal = _IssueTicketChat.CompanyId.HasValue ? _IssueTicketChat.CompanyId.Value.ToString() : "NULL";

            string userIdVal = _IssueTicketChat.UserId.HasValue ? _IssueTicketChat.UserId.Value.ToString() : "NULL";

            var qstr = "INSERT INTO tblIssueTicketChat (TicketId, CompanyId, UserId, Message, ChatDateTime) " +
                    "VALUES (" + _IssueTicketChat.TicketId + "," + companyIdVal +
                    "," + userIdVal + ",'" + _IssueTicketChat.Message +
                    "',GETDATE())";

            DBAccess db = new DBAccess();

            return db.ExecuteScalar(qstr);
        }

        public static ArrayList GetChatMessagesByTicketId(int TicketId, int CompanyId)
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
                        " AND tblIssueTicketChat.CompanyId = " + CompanyId +
                        " ORDER BY ChatDateTime";

            SqlDataReader reader = db.ReadDB(qstr);

            while (reader.Read())
            {
                var pIssueTicketChat = new IssueTicketChat();

                pIssueTicketChat.ChatId = Convert.ToInt32(reader["ChatId"]);
                pIssueTicketChat.ChatDateTime = (DateTime)reader["ChatDateTime"];
                pIssueTicketChat.TicketId = Convert.ToInt32(reader["TicketId"]);
                pIssueTicketChat.CompanyId = reader["CompanyId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CompanyId"]);
                pIssueTicketChat.UserId = reader["UserId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["UserId"]);
                pIssueTicketChat.Message = (string)(reader["Message"] == DBNull.Value ? "" : reader["Message"]);
                pIssueTicketChat.CompanyUserName = (string)(reader["CompanyUserName"] == DBNull.Value ? "" : reader["CompanyUserName"]);
                pIssueTicketChat.UserName = (string)(reader["UserName"] == DBNull.Value ? "" : reader["UserName"]);

                pIssueTicketList.Add(pIssueTicketChat);
            }

            reader.Close();
            reader.Dispose();

            return pIssueTicketList;
        }

        public static ArrayList GetChatListByTicketId(int TicketId)
        {
            ArrayList pIssueTicketList = new ArrayList();

            DBAccess db = new DBAccess();
            Response strReturn = new Response();

            string qstr = " SELECT  DISTINCT   tblIssueTicketChat.TicketId, " +
                        " tblIssueTicketChat.CompanyId, tblCompanyMaster.Username AS CompanyUserName" +
                        " FROM    tblIssueTicketChat" +
                        " LEFT OUTER JOIN tblCompanyMaster on tblIssueTicketChat.CompanyId = tblCompanyMaster.pCompId" +
                        " WHERE tblIssueTicketChat.TicketId = " + TicketId +
                        " ORDER BY tblIssueTicketChat.CompanyId";

            SqlDataReader reader = db.ReadDB(qstr);

            while (reader.Read())
            {
                var pIssueTicketChat = new IssueTicketChat();

                pIssueTicketChat.TicketId = Convert.ToInt32(reader["TicketId"]);
                pIssueTicketChat.CompanyId = reader["CompanyId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CompanyId"]);
                pIssueTicketChat.CompanyUserName = (string)(reader["CompanyUserName"] == DBNull.Value ? "" : reader["CompanyUserName"]);

                pIssueTicketList.Add(pIssueTicketChat);
            }

            reader.Close();
            reader.Dispose();

            return pIssueTicketList;
        }


    }
}