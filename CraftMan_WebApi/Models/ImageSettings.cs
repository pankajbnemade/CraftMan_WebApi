using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace CraftMan_WebApi.Models
{
    public class ImageSettings
    {
        public string BaseUrl { get; set; }
        public string StoragePath { get; set; }

        public ImageSettings()
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            BaseUrl= configuration["ImageSettings:BaseUrl"];
            StoragePath = configuration["ImageSettings:StoragePath"];
        }

    }
}
