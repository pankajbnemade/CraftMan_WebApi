using CraftMan_WebApi.DataAccessLayer;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.Data.SqlClient;
using System.Collections;

namespace CraftMan_WebApi.Models
{
    public class ServiceMaster
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public byte[]? ImageData { get; set; }

        public static ServiceMaster GetServiceDetail(int? ServiceId)
        {
            DBAccess db = new DBAccess();
            Response strReturn = new Response();

            string qstr = " SELECT  ServiceId, ServiceName, ImageData FROM  dbo.tblServiceMaster where ServiceId="
                        + ServiceId.ToString() + "  ";

            SqlDataReader reader = db.ReadDB(qstr);

            var pServiceMaster = new ServiceMaster();

            while (reader.Read())
            {
                pServiceMaster.ServiceId = Convert.ToInt32(reader["ServiceId"]);
                pServiceMaster.ServiceName = (string)reader["ServiceName"];
                pServiceMaster.ImageData = reader["ImageData"] as byte[];
            }

            reader.Close();

            return pServiceMaster;
        }
        public static ArrayList GetServiceList()
        {
            ArrayList ServiceList = new ArrayList();

            DBAccess db = new DBAccess();
            Response strReturn = new Response();

            string qstr = " SELECT  ServiceId, ServiceName, ImageData FROM  dbo.tblServiceMaster";

            SqlDataReader reader = db.ReadDB(qstr);

            while (reader.Read())
            {
                var pServiceMaster = new ServiceMaster();

                pServiceMaster.ServiceId = Convert.ToInt32(reader["ServiceId"]);
                pServiceMaster.ServiceName = (string)reader["ServiceName"];

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
            string qstr = " INSERT into dbo.tblServiceMaster(ServiceName, ImageData)  VALUES('" + _Service.ServiceName + "'" + ",'" + _Service.ImageData + "') ";

            DBAccess db = new DBAccess();
            int i = db.ExecuteNonQuery(qstr);

            return i;
        }

        public static int UpdateService(ServiceMaster _Service)
        {
            string qstr = " UPDATE dbo.tblServiceMaster " +
                            " SET  " +
                            "   ServiceName = '" + _Service.ServiceName + "'" +
                            "   WHERE " +
                            "   ServiceId = " + _Service.ServiceId + "  ";

            DBAccess db = new DBAccess();
            return db.ExecuteNonQuery(qstr);
        }

    }
}