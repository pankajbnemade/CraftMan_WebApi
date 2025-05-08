using CraftMan_WebApi.DataAccessLayer;
using CraftMan_WebApi.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace CraftMan_WebApi.Models
{
    public class IssueTicketForCompanyFilter
    {
        public int CompanyId { get; set; }
        public int? CountyId { get; set; }
        public int? MunicipalityId { get; set; }
        public int? ServiceId { get; set; }
        public string? Status { get; set; }
    }
}
