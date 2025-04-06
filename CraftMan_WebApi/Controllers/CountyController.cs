using CraftMan_WebApi.Models;
using CraftMan_WebApi.ExtendedModels;
using Microsoft.AspNetCore.Mvc;

using System.Data;
using System;
using System.Collections;
using Microsoft.AspNetCore.Authorization;

namespace CraftMan_WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CountyController : Controller
    {
        [HttpPost]
        [Route("NewCounty")]
        public Response NewCounty([FromBody] CountyMaster _CountyMaster)
        {
            return CountyMasterExtended.NewCounty(_CountyMaster);
        }

        [HttpPost]
        [Route("UpdateCounty")]
        public Response UpdateCounty([FromBody] CountyMaster _CountyMaster)
        {
            return CountyMasterExtended.UpdateCounty(_CountyMaster);
        }

        [HttpGet]
        [Route("GetCountyDetail")]
        public CountyMaster GetCountyDetail(int CountyId)
        {
            return CountyMasterExtended.GetCountyDetailByCountyId(CountyId);
        }

        [HttpGet]
        [Route("GetCountyList")]
        public ArrayList GetCountyList()
        {
            return CountyMasterExtended.GetCountyList();
        }

        [HttpGet]
        [Route("GetCountyListByCompany")]
        public ArrayList GetCountyListByCompany(int CompanyId)
        {
            return CountyMasterExtended.GetCountyListByCompanyId(CompanyId);
        }
    }
}
