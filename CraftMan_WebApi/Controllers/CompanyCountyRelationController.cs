using CraftMan_WebApi.ExtendedModels;
using CraftMan_WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace CraftMan_WebApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyCountyRelationController : Controller
    {
        [HttpPost]
        [Route("NewRelation")]
        public Response NewRelation([FromBody] CompanyCountyRelation _CompanyCountyRelation)
        {
            return CompanyCountyRelationExtended.InsertNewRelation(_CompanyCountyRelation);
        }

        [HttpPost]
        [Route("DeleteRelation")]
        public Response DeleteRelation(CompanyCountyRelation _CompanyCountyRelation)
        {
            return CompanyCountyRelationExtended.DeleteRelation(_CompanyCountyRelation);
        }

        [HttpGet]
        [Route("GetRelationDetailByCompany")]
        public ArrayList GetRelationDetailByCompany(int CompanyId)
        {
            return CompanyCountyRelationExtended.GetRelationDetailByCompany(CompanyId);
        }
    }
}
