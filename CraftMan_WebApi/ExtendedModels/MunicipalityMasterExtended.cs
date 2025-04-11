using CraftMan_WebApi.Models;
using System.Collections;
namespace CraftMan_WebApi.ExtendedModels
{
    public class MunicipalityMasterExtended
    {
        public static MunicipalityMaster GetMunicipalityDetailByMunicipalityId(int MunicipalityId)
        {
            try
            {
                return MunicipalityMaster.GetMunicipalityDetail(MunicipalityId);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
        }

        public static ArrayList GetMunicipalityList(int CountyId)
        {
            try
            {
                return MunicipalityMaster.GetMunicipalityList(CountyId);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
        }

        public static ArrayList GetMunicipalityListByCompanyId(int CountyId, int CompanyId)
        {
            try
            {
                return MunicipalityMaster.GetMunicipalityListByCompanyId(CountyId, CompanyId);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
        }

        public static Response NewMunicipality(MunicipalityMaster _MunicipalityMaster)
        {
            Response strReturn = new Response();

            try
            {
                if (MunicipalityMaster.ValidateMunicipality(_MunicipalityMaster)==true)
                {
                    strReturn.StatusMessage = "Municipality name already exists...";
                    strReturn.StatusCode = 0;
                }
                else
                {
                    int i = MunicipalityMaster.InsertMunicipality(_MunicipalityMaster);

                    if (i > 0)
                    {
                        strReturn.StatusCode = 1;
                        strReturn.StatusMessage = "Municipality added successfully";
                    }
                    else
                    { strReturn.StatusMessage = "Municipality not added."; }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }

            return strReturn;
        }

        public static Response UpdateMunicipality(MunicipalityMaster _MunicipalityMaster)
        {
            Response strReturn = new Response();

            try
            {
                if (MunicipalityMaster.GetMunicipalityDetail(_MunicipalityMaster.MunicipalityId).MunicipalityId == 0)
                {
                    strReturn.StatusMessage = "Municipality details not exists for update...";
                    strReturn.StatusCode = 0;
                }
                else if (MunicipalityMaster.ValidateUpdateMunicipality(_MunicipalityMaster) == true)
                {
                    strReturn.StatusMessage = "Municipality name already exists...";
                    strReturn.StatusCode = 0;
                }
                else
                {
                    int i = MunicipalityMaster.UpdateMunicipality(_MunicipalityMaster);

                    if (i > 0)
                    {
                        strReturn.StatusCode = 1;
                        strReturn.StatusMessage = "Municipality updated successfully";
                    }
                    else
                    {
                        strReturn.StatusMessage = "Municipality not updated.";
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
