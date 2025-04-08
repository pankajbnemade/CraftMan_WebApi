using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace CraftMan_WebApi.Models
{
    public class ImageSettings
    {
        public string DefaultImageUrl { get; set; }
        public string BaseUrl { get; set; }
        public string StoragePath { get; set; }

        public ImageSettings()
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            DefaultImageUrl = configuration["ImageSettings:DefaultImageUrl"];
            BaseUrl= configuration["ImageSettings:BaseUrl"];
            StoragePath = configuration["ImageSettings:StoragePath"];
        }

    }
}
