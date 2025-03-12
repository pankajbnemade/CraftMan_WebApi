using CraftMan_WebApi.Models;
using CraftMan_WebApi.ExtendedModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using Microsoft.AspNetCore.Http.HttpResults;
namespace CraftMan_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        [HttpPost]
        [Route("CompanySignUp")]
        public Response Register([FromBody] CompanyMaster _Company)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Response { StatusCode = 0, StatusMessage = "Company model invalid." };
                }

                Console.WriteLine("Register");

                return Companymasterextended.RegistrationCompany(_Company);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
        }

        [HttpGet]
        [Route("GetCompanyDetail")]
        public CompanyMaster GetCompanyDetail(string EmailId)
        {
            try
            {
                return Companymasterextended.GetCompanyDetail(EmailId);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
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
        public Response LoginCompany([FromBody] LoginComp _Company)
        {
            return Companymasterextended.LoginValidateForCompany(_Company);
        }

        //[HttpPost("forgot-password")]
        //public IActionResult ForgotPassword([FromBody] string email)
        //{
        //    Response result = Companymasterextended.GeneratePasswordResetToken(email);
        //    return Ok(result);
        //}

        //[HttpPost("reset-password")]
        //public IActionResult ResetPassword([FromBody] ResetPasswordModel model)
        //{
        //    Response result = Companymasterextended.ResetPassword(model);
        //    return Ok(result);
        //}


    }
}
