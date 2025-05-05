using CraftMan_WebApi.Models;
using CraftMan_WebApi.ExtendedModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
namespace CraftMan_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(ILogger<CompanyController> logger)
        {
            _logger = logger;
        }


        [HttpPost]
        [Route("CompanySignUp")]
        public Response Register([FromForm] CompanyMaster _CompanyMaster)
        {
            try
            {
                if (_CompanyMaster == null)
                {
                    _logger.LogWarning("Received null CompanyMaster object.");
                }

                // Log the received data as JSON
                _logger.LogInformation("Received CompanyMaster: {CompanyMasterData}", JsonSerializer.Serialize(_CompanyMaster));

                if (!ModelState.IsValid)
                {
                    return new Response { StatusCode = 0, StatusMessage = "Company model invalid." };
                }

                Console.WriteLine("Register");

                return Companymasterextended.RegistrationCompany(_CompanyMaster);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("UpdateCompany")]
        public Response UpdateCompany([FromForm] CompanyMasterUpdateModel _Company)
        {
            return Companymasterextended.UpdateCompany(_Company);
        }




        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("CompanySignIn")]
        public Response LoginCompany([FromBody] LoginComp _Company)
        {
            return Companymasterextended.LoginValidateForCompany(_Company);
        }

        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword([FromBody] string email)
        {
            Response result = Companymasterextended.GeneratePasswordResetToken(email);
            return Ok(result);
        }

        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword([FromBody] ResetPasswordModel model)
        {
            Response result = Companymasterextended.ResetPassword(model);
            return Ok(result);
        }



        //[Authorize]
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



        //[Authorize]
        [HttpPost]
        [Route("UpdateCompanyIs24X7")]
        public Response UpdateCompanyIs24X7(int companyId, bool is24X7)
        {
            return Companymasterextended.UpdateCompanyIs24X7(companyId, is24X7);
        }


        //[Authorize]
        [HttpGet]
        [Route("GetCompany24X7ForUser")]
        public ArrayList GetCompany24X7ForUser(Int32 userId, int? serviceId)
        {
            return Companymasterextended.GetCompany24X7ForUser(userId, serviceId);
        }

        //[Authorize]
        [HttpGet]
        [Route("GetCompanyList")]
        public ArrayList GetCompanyList(int? countyId, int? municipalityId, int? serviceId)
        {
            return Companymasterextended.GetCompanyList(countyId, municipalityId, serviceId);
        }






        //[HttpPost]
        //[Route("UpdateActive")]
        //public Response UpdateActive(int companyId, string active)
        //{
        //    return Companymasterextended.UpdateActive(companyId, active);
        //}

        //[HttpPost]
        //[Route("UpdateCompany")]
        //public Response UpdateCompany([FromForm] CompanyMaster _Company)
        //{
        //    return Companymasterextended.UpdateCompany(_Company);
        //}

    }
}
