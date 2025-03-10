using CraftMan_WebApi.DataAccessLayer;
using Microsoft.Data.SqlClient;
using System.Collections;

namespace CraftMan_WebApi.Helper
{
    public class CommonFunction
    {
        public static string GetContentType(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLower();
            return extension switch
            {
                ".png" => "image/png",
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".bmp" => "image/bmp",
                ".gif" => "image/gif",
                ".webp" => "image/webp",
                _ => "application/octet-stream" // Default for unknown types
            };
        }

        public static void SendResetEmail(string email, string token)
        {
            string resetLink = $"https://yourapp.com/reset-password?token={token}";
            string subject = "Password Reset Request";
            string body = $"Click the link below to reset your password:\n{resetLink}\n\nThis link is valid for 1 hour.";

            // Use an SMTP client to send an email
            //EmailHelper.SendEmail(email, subject, body);
        }

    }
}