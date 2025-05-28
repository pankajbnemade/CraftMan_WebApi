using CraftMan_WebApi.DataAccessLayer;
using CraftMan_WebApi.Helper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CraftMan_WebApi.Models
{
    public class UserMasterUpdateModel
    {
        public string Username { get; set; }
        public int UserId { get; set; } // Primary key
        public int? LocationId { get; set; } // Nullable integer
        public string MobileNumber { get; set; }
        public string? ContactPerson { get; set; }
        public string EmailId { get; set; }
        public int CountyId { get; set; }
        public int MunicipalityId { get; set; }

    }
}
