using CraftMan_WebApi.Models;
using CraftMan_WebApi.ExtendedModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
namespace CraftMan_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        [HttpPost]
        [Route("CompanySignUp")]
        public Response Register(CompanyMaster _Company)
        {
            return Companymasterextended.RegistrationCompany(_Company);
        }

        [HttpGet]
        [Route("GetCompanyDetail")]
        public CompanyMaster GetCompanyDetail(string Username)
        {
            return Companymasterextended.GetCompanyDetail(Username);
        }
        [HttpGet]
        [Route("GetCompanyJobList")]
        public string[] GetCompanyJobList(string Username)
        {
            return Companymasterextended.GetCompanyDetail(Username).JobList;
        }

        [HttpGet]
        [Route("GetTotalJobRequest")]
        public int GetTotalJobRequest(string Username)
        {
            return Companymasterextended.GetTotalcnt(Username);
        }

        [HttpGet]
        [Route("GetCompanyEmpDetail")]
        public ArrayList GetCompanyEmpDetail(string Username)
        {
            return Companymasterextended.GetCompEmployeeList(Username);
        }

        [HttpGet]
        [Route("GetActivecountnoofcraftsman")]
        public int GetCompanyEmpDetailcnt(string Username)
        {
            return Companymasterextended.GetCompEmployeeList(Username).Count;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("CompanySignIn")]
        public Response LoginCompany(LoginComp _Company)
        {
            return Companymasterextended.LoginValidateForCompany(_Company);
        }
    }
}
