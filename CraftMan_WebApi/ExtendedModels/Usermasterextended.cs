
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

        public static Response RegistrationForUser(UserMaster _User)
        {
            Response strReturn = new Response();

            try
            {
                if (UserMaster.ValidateUser(_User).StatusCode > 0)
                {
                    strReturn.StatusMessage = "User already exists...";
                    strReturn.StatusCode = 0;
                }
                else
                {
                    int i = UserMaster.InsertUser(_User);

                    if (i > 0)
                    {
                        strReturn.StatusCode = i;
                        strReturn.StatusMessage = "User Registered Successfully.";
                    }
                    else
                    {
                        strReturn.StatusMessage = "User not registered.";
                    }
                }

                return strReturn;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
        }

        public static Response LoginValidateForUser(LoginUser _User)
        {
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


        public static Response UpdateUser(UserMasterUpdateModel _User)
        {
            Response strReturn = new Response();

            try
            {
                if (UserMaster.ValidateUserUpdate(_User).StatusCode > 0)
                {
                    strReturn.StatusMessage = "User already exists for emailId / User name...";
                    strReturn.StatusCode = 0;
                }
                else
                {
                    int i = UserMaster.UpdateUser(_User);

                    if (i > 0)
                    {
                        strReturn.StatusCode = _User.UserId;
                        strReturn.StatusMessage = "User updated successfully.";
                    }
                    else
                    {
                        strReturn.StatusMessage = "User not updated.";
                    }
                }

                return strReturn;
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
