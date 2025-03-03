using CraftMan_WebApi.Models;
using System.Collections;
namespace CraftMan_WebApi.ExtendedModels
{
    public class ServiceMasterExtended
    {
        public static ServiceMaster GetServiceDetailByServiceId(int ServiceId)
        {
            return ServiceMaster.GetServiceDetail(ServiceId);
        }

        public static ArrayList GetServiceList()
        {
            return ServiceMaster.GetServiceList();
        }

        public static Response NewService(ServiceMaster _ServiceMaster)
        {
            Response strReturn = new Response();

            try
            {
                if (_ServiceMaster.ValidateService(_ServiceMaster).StatusCode > 0)
                {
                    strReturn.StatusMessage = "Service name already exists...";
                    strReturn.StatusCode = 1;
                }
                else
                {
                    int i = ServiceMaster.InsertService(_ServiceMaster);

                    if (i > 0)
                    {
                        strReturn.StatusCode = 1;
                        strReturn.StatusMessage = "Service added successfully";
                    }
                    else
                    { strReturn.StatusMessage = "Service not added."; }
                }
            }
            catch (Exception ex) { throw; }

            return strReturn;
        }


        public static Response UpdateService(ServiceMaster _ServiceMaster)
        {
            Response strReturn = new Response();

            try
            {
                if (ServiceMaster.GetServiceDetail(_ServiceMaster.ServiceId).ServiceId == 0)
                {
                    strReturn.StatusMessage = "Service details not exists for update...";
                    strReturn.StatusCode = 1;
                }
                else if (_ServiceMaster.ValidateUpdateService(_ServiceMaster).StatusCode > 0)
                {
                    strReturn.StatusMessage = "Service name already exists...";
                    strReturn.StatusCode = 1;
                }
                else
                {
                    int i = ServiceMaster.UpdateService(_ServiceMaster);

                    if (i > 0)
                    {
                        strReturn.StatusCode = 1;
                        strReturn.StatusMessage = "Service updated successfully";
                    }
                    else
                    {
                        strReturn.StatusMessage = "Service not updated.";
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return strReturn;
        }

    }
}
