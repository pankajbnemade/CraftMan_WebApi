using CraftMan_WebApi.Models;
using CraftMan_WebApi.ExtendedModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
namespace CraftMan_WebApi.Controllers
{
    

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
