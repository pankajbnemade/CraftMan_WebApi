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

                try
                {
                    string value = (_DeviceTokenModel.pCompId == null ? "" : "pCompId : " + _DeviceTokenModel.pCompId.ToString())
                            + (_DeviceTokenModel.Token == null ? "" : " Token : " + _DeviceTokenModel.Token)
                            + (_DeviceTokenModel.Platform == null ? "" : " Platform : " + _DeviceTokenModel.Platform);

                    ErrorLogger.LogErrorMethod("SaveNewDeviceToken", value);
                }
                catch (Exception ex)
                {
                }

                if (DeviceToken.ValidateToken(_DeviceTokenModel) == true)
                {
                    strReturn.StatusMessage = "Token already registered...";
                    strReturn.StatusCode = 0;
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
