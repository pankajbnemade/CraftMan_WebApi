using CraftMan_WebApi.Models;
using CraftMan_WebApi.ExtendedModels;
using Microsoft.AspNetCore.Mvc;
 

namespace CraftMan_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("GetUserDetail")]
        public UserMaster GetUserDetail(string EmailId)
        {
            return UserMaster.GetUserDetail(EmailId);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("UserSignUp")]
        public Response RegisterUser([FromBody] UserMaster _User)
        {   
            return Usermasterextended.RegistrationForUser(_User);         
        }      

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("UserSignIn")]
        public Response LoginUser([FromBody] LoginUser _User)
        {            
        
          return Usermasterextended.LoginValidateForUser(_User);

        }


        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword([FromBody] string email)
        {
            Response result = Usermasterextended.GeneratePasswordResetToken(email);
            return Ok(result);
        }

        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword([FromBody] ResetPasswordModel model)
        {
            Response result = Usermasterextended.ResetPassword(model);
            return Ok(result);
        }


    }
}
