
using CraftMan_WebApi.DataAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace CraftMan_WebApi.Models
{
    public static class ErrorLogger
    {
        private static IWebHostEnvironment _webHostEnvironment;

        public static void Configure(IWebHostEnvironment env)
        {
            _webHostEnvironment = env;
        }


        public static void LogError(Exception ex, [CallerMemberName] string methodName = "")
        {
            try
            {
                DBAccess db = new DBAccess();

                string qstr = @"INSERT INTO ErrorLogs (MethodName, ErrorMessage, StackTrace, LogDate) 
                                 VALUES ('" + methodName + "', '" + ex.Message + "' , '" + ex.StackTrace + "', GETDATE())";

                db.ExecuteNonQuery(qstr);
            }
            catch (Exception logEx)
            {
                string logFilePath = _webHostEnvironment != null
                ? Path.Combine(_webHostEnvironment.WebRootPath, "logs", "error_log.txt")
                : "error_log.txt";

                Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));

                File.AppendAllText(logFilePath,
                $"{DateTime.Now}: Error in LogError Method - {logEx.Message}{Environment.NewLine}");
            }
        }
    }

}
