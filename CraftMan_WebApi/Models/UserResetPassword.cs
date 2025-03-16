using CraftMan_WebApi.DataAccessLayer;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.Data.SqlClient;
using System.Collections;
using System.IO;
using CraftMan_WebApi.Helper;

namespace CraftMan_WebApi.Models
{
    public class UserResetPassword
    {
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string? PasswordResetToken { get; set; } // Token for resetting password
        public DateTime? ResetTokenExpiry { get; set; } // Expiry time for reset token

        public static Response GeneratePasswordResetToken(string email)
        {
            Response response = new Response();
            DBAccess db = new DBAccess();

            string checkUserQuery = $"SELECT pkey_UId FROM tblUserMaster WHERE EmailId = '{email}'";
            int userExists = db.ExecuteScalar(checkUserQuery);

            if (userExists > 0)
            {
                //string token = Guid.NewGuid().ToString(); // Generate unique token
                string token = new Random().Next(100000, 999999).ToString();
                DateTime expiryTime = DateTime.Now.AddHours(1); // Token valid for 1 hour

                string updateQuery = $"UPDATE tblUserMaster SET PasswordResetToken = '{token}', ResetTokenExpiry = '{expiryTime}' WHERE EmailId = '{email}'";
                db.ExecuteNonQuery(updateQuery);

                EmailHelper.SendResetEmail(email, token);

                response.StatusCode = 1;
                response.StatusMessage = "Password reset link sent to email.";
            }
            else
            {
                response.StatusCode = 0;
                response.StatusMessage = "Email not found!";
            }

            return response;
        }
        

        public static Response ResetPassword(string token, string newPassword)
        {
            Response response = new Response();
            DBAccess db = new DBAccess();

            string query = $"SELECT pkey_UId FROM tblUserMaster WHERE PasswordResetToken = '{token}' AND ResetTokenExpiry > GETDATE()";
            int userId = db.ExecuteScalar(query);

            if (userId > 0)
            {
                string updateQuery = $"UPDATE tblUserMaster SET Password = '{newPassword}', PasswordResetToken = NULL, ResetTokenExpiry = NULL WHERE PasswordResetToken = '{token}'";
                db.ExecuteNonQuery(updateQuery);

                response.StatusCode = 1;
                response.StatusMessage = "Password has been reset successfully.";
            }
            else
            {
                response.StatusCode = 0;
                response.StatusMessage = "Invalid or expired token.";
            }

            return response;
        }

    }
}