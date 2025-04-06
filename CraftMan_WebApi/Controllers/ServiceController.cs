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
    public class ServiceController : Controller
    {
        [HttpPost]
        [Route("NewService")]
        public Response NewService([FromForm] ServiceMaster _ServiceMaster)
        {
            return ServiceMasterExtended.NewService(_ServiceMaster);
        }

        [HttpPost]
        [Route("UpdateService")]
        public Response UpdateService([FromForm] ServiceMaster _ServiceMaster)
        {
            return ServiceMasterExtended.UpdateService(_ServiceMaster);
        }

        [HttpGet]
        [Route("GetServiceDetail")]
        public ServiceMaster GetServiceDetail(int ServiceId)
        {
            return ServiceMasterExtended.GetServiceDetailByServiceId(ServiceId);
        }

        [HttpGet]
        [Route("GetServiceList")]
        public ArrayList GetServiceList()
        {
            return ServiceMasterExtended.GetServiceList();
        }

    }
}
