﻿using CraftMan_WebApi.DataAccessLayer;
using Microsoft.Data.SqlClient;
using System;
using System.Collections;

namespace CraftMan_WebApi.Models
{
    public class CompanyServices
    {
        public int pCompId { get; set; }
        public int ServiceId { get; set; }
        public string? CompanyName { get; set; }
        public string? ServiceName { get; set; }

        public static ArrayList GetServicesByCompany(int CompanyId)
        {
            ArrayList CompanyServiceList = new ArrayList();

            DBAccess db = new DBAccess();
            Response strReturn = new Response();

            string qstr = " SELECT distinct tblServiceMaster.ServiceName, tblCompanyMaster.CompanyName, tblCompanyServices.pCompId, tblCompanyServices.ServiceId " +
                " FROM   tblCompanyServices " +
                " INNER JOIN  tblCompanyMaster ON tblCompanyServices.pCompId = tblCompanyMaster.pCompId " +
                " LEFT OUTER JOIN  tblServiceMaster ON tblCompanyServices.ServiceId = tblServiceMaster.ServiceId " +
                " WHERE tblCompanyServices.pCompId = " + CompanyId + "  ";

            SqlDataReader reader = db.ReadDB(qstr);

            while (reader.Read())
            {
                var pCompanyServices = new CompanyServices();

                pCompanyServices.pCompId = Convert.ToInt32(reader["pCompId"]);
                pCompanyServices.ServiceId = Convert.ToInt32(reader["ServiceId"]);
                pCompanyServices.CompanyName = reader["CompanyName"] == DBNull.Value ? "" : (string)reader["CompanyName"];
                pCompanyServices.ServiceName = reader["ServiceName"] == DBNull.Value ? "" : (string)reader["ServiceName"];

                CompanyServiceList.Add(pCompanyServices);
            }

            reader.Close();
            reader.Dispose();

            return CompanyServiceList;
        }

        public Response ValidateInsertService(CompanyServices _CompanyServices)
        {
            string qstr = " select pCompId from tblCompanyServices " +
                        " where tblCompanyServices.pCompId=" + _CompanyServices.pCompId +
                        " and tblCompanyServices.ServiceId = " + _CompanyServices.ServiceId;

            DBAccess db = new DBAccess();

            return db.validate(qstr);
        }

        public static int InsertNewService(CompanyServices _CompanyServices)
        {
            string qstr = " INSERT into tblCompanyServices(pCompId, ServiceId)  " +
                            " VALUES(" + _CompanyServices.pCompId + "," + _CompanyServices.ServiceId + ") ";

            DBAccess db = new DBAccess();

            int i = db.ExecuteNonQuery(qstr);

            return i;
        }

        public static int InsertNewServices(int CompanyId, string[]? ServicesIdList)
        {
            try
            {
                if (ServicesIdList != null && ServicesIdList.Any())
                {
                    string qstr = "INSERT INTO tblCompanyServices (pCompId, ServiceId) VALUES ";

                    List<string> valuesList = new List<string>();

                    foreach (string Id in ServicesIdList)
                    {
                        string ServiceId = Id.Trim().Trim('"');

                        string values = $"({CompanyId}, {ServiceId})";
                        valuesList.Add(values);
                    }

                    qstr += string.Join(",", valuesList);

                    DBAccess db = new DBAccess();
                    return db.ExecuteNonQuery(qstr);
                }

                return 0;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
        }
        
        public static int DeleteService(CompanyServices _CompanyServices)
        {
            string qstr = " DELETE FROM dbo.tblCompanyServices " +
                        " where tblCompanyServices.pCompId=" + _CompanyServices.pCompId +
                        " and tblCompanyServices.ServiceId = " + _CompanyServices.ServiceId;

            DBAccess db = new DBAccess();

            return db.ExecuteNonQuery(qstr);
        }

        public static int DeleteServices(int CompanyId)
        {
            string qstr = " DELETE FROM dbo.tblCompanyServices " +
                        " where tblCompanyServices.pCompId=" + CompanyId;

            DBAccess db = new DBAccess();

            return db.ExecuteNonQuery(qstr);
        }

    }
}