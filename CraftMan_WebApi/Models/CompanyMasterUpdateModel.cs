using CraftMan_WebApi.DataAccessLayer;
using CraftMan_WebApi.Helper;
using Microsoft.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;

namespace CraftMan_WebApi.Models
{
    public class CompanyMasterUpdateModel
    {
        public int pCompId { get; set; }
        public int? LocationId { get; set; }
        public string MobileNumber { get; set; }
        public string? ContactPerson { get; set; }
        public string EmailId { get; set; }
        public bool? Is24X7 { get; set; }
        public string CompanyName { get; set; }
        public string? CompanyRegistrationNumber { get; set; }
        public string? CompanyPresentation { get; set; }
        public string? CompetenceDescription { get; set; }
        public string? CompanyReferences { get; set; }
        public IFormFile? LogoImage { get; set; }
        public string? LogoImageName { get; set; }
        public string? LogoImagePath { get; set; }
        public string[]? CountyIdList { get; set; }
        public string[]? MunicipalityIdList { get; set; }
        public string[]? ServiceIdList { get; set; }

    }
}