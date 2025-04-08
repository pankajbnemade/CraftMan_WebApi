using CraftMan_WebApi.Models;
using CraftMan_WebApi.ExtendedModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using Microsoft.AspNetCore.Authorization;
namespace CraftMan_WebApi.Controllers
{

    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JobTypeController : Controller
    {
         
        [HttpPost]
        [Route("NewJob")]
        public Response NewJobEntry(JobMaster _job)
        {

            return JobMasterExtended.NewJob(_job);
        }
        [HttpGet]        
        public ArrayList GetJobTypes()
        {              
            return JobMasterExtended.JobList(); ;
        }
    }
}
