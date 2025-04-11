using CraftMan_WebApi.Models;

namespace CraftMan_WebApi.ExtendedModels
{
    public class DeviceTokenExtended
    {
        public static Response SaveNewDeviceToken(DeviceTokenModel _DeviceTokenModel)
        {
            Response strReturn = new Response();

            try
            {
                if (DeviceToken.ValidateToken(_DeviceTokenModel) == true)
                {
                    strReturn.StatusMessage = "Token already registered...";
                    strReturn.StatusCode = 1;
                }
                else
                {
                    int i = DeviceToken.SaveNewDeviceToken(_DeviceTokenModel);

                    if (i > 0)
                    {
                        strReturn.StatusCode = i;
                        strReturn.StatusMessage = "Token registered successfully";
                    }
                    else
                    {
                        strReturn.StatusMessage = "Token not registered.";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }

            return strReturn;
        }


    }
}
