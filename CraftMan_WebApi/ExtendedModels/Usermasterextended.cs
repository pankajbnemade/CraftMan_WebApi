
using CraftMan_WebApi.Models; 
namespace CraftMan_WebApi.ExtendedModels
{
    public class Usermasterextended   
    {        
        public static Response RegistrationForUser(UserMaster _User ) 
        {
            try { Response strReturn = new Response();
                 
                return UserMaster.insertUser( _User);
            }
            catch (Exception ex) { throw; }
        }
        public static Response LoginValidateForUser(LoginUser _User) {
            try
            {                
                return UserMaster.LoginValidateForUser(_User);              

                }
            
            catch (Exception ex) { throw;  }
        }
    }
}
