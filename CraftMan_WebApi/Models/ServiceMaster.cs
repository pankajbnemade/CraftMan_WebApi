using CraftMan_WebApi.DataAccessLayer;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.Data.SqlClient;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using CraftMan_WebApi.Helper;

namespace CraftMan_WebApi.Models
{
    public class ServiceMaster
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public IFormFile? ServiceImage { get; set; }
        public string? ImageName { get; set; }
        public string? ImagePath { get; set; }
        public string? ImageContentType { get; set; }

        public static ServiceMaster GetServiceDetail(int? ServiceId)
        {
            DBAccess db = new DBAccess();
            Response strReturn = new Response();

            string qstr = " SELECT  ServiceId, ServiceName, ImageName, ImagePath  FROM  dbo.tblServiceMaster where ServiceId="
                        + ServiceId.ToString() + "  ";

            SqlDataReader reader = db.ReadDB(qstr);

            var pServiceMaster = new ServiceMaster();

            ImageSettings pImageSettings = new ImageSettings();

            while (reader.Read())
            {
                pServiceMaster.ServiceId = Convert.ToInt32(reader["ServiceId"]);
                pServiceMaster.ServiceName = reader["ServiceName"] == DBNull.Value ? "" : (string)reader["ServiceName"];
                pServiceMaster.ImageName = reader["ImageName"] == DBNull.Value ? "" : (string)reader["ImageName"];
                pServiceMaster.ImagePath = reader["ImagePath"] == DBNull.Value ? "" : (string)reader["ImagePath"];

                if (pServiceMaster.ImagePath != "")
                {
                    pServiceMaster.ImagePath= pServiceMaster.ImagePath.Replace("\\", "/");

                    if (System.IO.File.Exists(pServiceMaster.ImagePath))
                    {
                        pServiceMaster.ImageContentType = CommonFunction.GetContentType(pServiceMaster.ImagePath);

                        pServiceMaster.ImagePath = pServiceMaster.ImagePath.Replace(pImageSettings.StoragePath, pImageSettings.BaseUrl);
                    }
                }

            }

            reader.Close();

            return pServiceMaster;
        }
        public static ArrayList GetServiceList()
        {
            ArrayList ServiceList = new ArrayList();

            DBAccess db = new DBAccess();
            Response strReturn = new Response();

            string qstr = " SELECT  ServiceId, ServiceName, ImageName, ImagePath FROM  dbo.tblServiceMaster";

            SqlDataReader reader = db.ReadDB(qstr);

            ImageSettings pImageSettings = new ImageSettings();

            while (reader.Read())
            {
                var pServiceMaster = new ServiceMaster();

                pServiceMaster.ServiceId = Convert.ToInt32(reader["ServiceId"]);
                pServiceMaster.ServiceName = reader["ServiceName"] == DBNull.Value ? "" : (string)reader["ServiceName"];
                pServiceMaster.ImageName = reader["ImageName"] == DBNull.Value ? "" : (string)reader["ImageName"];
                pServiceMaster.ImagePath = reader["ImagePath"] == DBNull.Value ? "" : (string)reader["ImagePath"];

                if (pServiceMaster.ImagePath != "")
                {
                    pServiceMaster.ImagePath = pServiceMaster.ImagePath.Replace("\\", "/");

                    if (System.IO.File.Exists(pServiceMaster.ImagePath))
                    {
                        pServiceMaster.ImageContentType = CommonFunction.GetContentType(pServiceMaster.ImagePath);

                        pServiceMaster.ImagePath = pServiceMaster.ImagePath.Replace(pImageSettings.StoragePath, pImageSettings.BaseUrl);
                    }
                }

                ServiceList.Add(pServiceMaster);
            }

            reader.Close();

            return ServiceList;
        }

        public static Boolean ValidateService(ServiceMaster _Service)
        {
            DBAccess db = new DBAccess();

            Response strReturn = new Response();

            string qstr = " select ServiceName from dbo.tblServiceMaster where upper(ServiceName) = upper('" + _Service.ServiceName + "')";

            SqlDataReader reader = db.ReadDB(qstr);

            while (reader.Read())
            {
                return true;
            }

            reader.Close();

            return false;
        }

        public static Boolean ValidateUpdateService(ServiceMaster _Service)
        {
            DBAccess db = new DBAccess();

            Response strReturn = new Response();

            string qstr = " select ServiceName from dbo.tblServiceMaster where upper(ServiceName) = upper('" + _Service.ServiceName + "') and ServiceId != " + _Service.ServiceId.ToString() + "";

            SqlDataReader reader = db.ReadDB(qstr);

            while (reader.Read())
            {
                return true;
            }

            reader.Close();

            return false;
        }

        public static int InsertService(ServiceMaster _Service)
        {
            try
            {
                string qstr = " INSERT into dbo.tblServiceMaster(ServiceName, ImageName, ImagePath)  VALUES('" + _Service.ServiceName + "'" + ",'" + _Service.ImageName + "','" + _Service.ImagePath + "') ";

                DBAccess db = new DBAccess();
                int i = db.ExecuteNonQuery(qstr);

                return i;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
        }

        public static int UpdateService(ServiceMaster _Service)
        {
            string qstr = " UPDATE dbo.tblServiceMaster " +
                            " SET  " +
                            "   ServiceName = '" + _Service.ServiceName + "', " +
                            "   ImageName = " + (_Service.ImageName == "" ? "ImageName," : "'" + _Service.ImageName + "', ") +
                            "   ImagePath = " + (_Service.ImagePath == "" ? "ImagePath" : "'" + _Service.ImagePath + "'") +
                            "   WHERE " +
                            "   ServiceId = " + _Service.ServiceId + "  ";

            DBAccess db = new DBAccess();
            return db.ExecuteNonQuery(qstr);
        }

    }
}