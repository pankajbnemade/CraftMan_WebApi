using CraftMan_WebApi.Models;
using CraftMan_WebApi.ExtendedModels;
using Microsoft.AspNetCore.Mvc;

using System.Data;
using System;
using System.Collections;
using Microsoft.AspNetCore.Authorization;

namespace CraftMan_WebApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MunicipalityController : Controller
    {
        [HttpPost]
        [Route("NewMunicipality")]
        public Response NewMunicipality([FromBody] MunicipalityMaster _MunicipalityMaster)
        {
            return MunicipalityMasterExtended.NewMunicipality(_MunicipalityMaster);
        }

        [HttpPost]
        [Route("UpdateMunicipality")]
        public Response UpdateMunicipality([FromBody] MunicipalityMaster _MunicipalityMaster)
        {
            return MunicipalityMasterExtended.UpdateMunicipality(_MunicipalityMaster);
        }

        [HttpGet]
        [Route("GetMunicipalityDetail")]
        public MunicipalityMaster GetMunicipalityDetail(int MunicipalityId)
        {
            return MunicipalityMasterExtended.GetMunicipalityDetailByMunicipalityId(MunicipalityId);
        }

        [HttpGet]
        [Route("GetMunicipalityList")]
        public ArrayList GetMunicipalityList(int CountyId)
        {
            return MunicipalityMasterExtended.GetMunicipalityList(CountyId);
        }
        [HttpGet]
        [Route("GetMunicipalityListByCompany")]
        public ArrayList GetMunicipalityListByCompany(int CountyId, int CompanyId)
        {
            return MunicipalityMasterExtended.GetMunicipalityListByCompanyId(CountyId,CompanyId);
        }

    }
}
