using CraftMan_WebApi.Models;
using CraftMan_WebApi.ExtendedModels;
using Microsoft.AspNetCore.Mvc;
 

namespace CraftMan_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    { 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("UserSignUp")]
        public Response RegisterUser(UserMaster _User)
        {   
            return Usermasterextended.RegistrationForUser(_User);         
        }      

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("UserSignIn")]
        public Response LoginUser(LoginUser _User)
        {            
        
          return Usermasterextended.LoginValidateForUser(_User);

        }
    }
}
