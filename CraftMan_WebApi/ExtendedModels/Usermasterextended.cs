
using CraftMan_WebApi.Models; 
namespace CraftMan_WebApi.ExtendedModels
{
    public class Usermasterextended   
    {
        public static UserMaster GetUserDetail(string EmailId)
        {
            try
            {
                return UserMaster.GetUserDetail(EmailId);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
        }

        public static Response RegistrationForUser(UserMaster _User ) 
        {
            try 
            { 
                Response strReturn = new Response();
                 
                return UserMaster.InsertUser( _User);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
        }

        public static Response LoginValidateForUser(LoginUser _User) {
            try
            {                
                return UserMaster.LoginValidateForUser(_User);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
        }

        public static Response GeneratePasswordResetToken(string email)
        {
            return UserResetPassword.GeneratePasswordResetToken(email); ;
        }

        public static Response ResetPassword(ResetPasswordModel model)
        {
            return UserResetPassword.ResetPassword(model);
        }
    }
}
