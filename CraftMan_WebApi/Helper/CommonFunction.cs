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


    }
}