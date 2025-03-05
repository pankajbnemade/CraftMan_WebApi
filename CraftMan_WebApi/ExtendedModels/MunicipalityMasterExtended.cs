using CraftMan_WebApi.Models;
using System.Collections;
namespace CraftMan_WebApi.ExtendedModels
{
    public class MunicipalityMasterExtended
    {
        public static MunicipalityMaster GetMunicipalityDetailByMunicipalityId(int MunicipalityId)
        {
            return MunicipalityMaster.GetMunicipalityDetail(MunicipalityId);
        }

        public static ArrayList GetMunicipalityList(int CountyId)
        {
            return MunicipalityMaster.GetMunicipalityList(CountyId);
        }

        public static ArrayList GetMunicipalityListByCompanyId(int CountyId, int CompanyId)
        {
            return MunicipalityMaster.GetMunicipalityListByCompanyId(CountyId, CompanyId);
        }

        public static Response NewMunicipality(MunicipalityMaster _MunicipalityMaster)
        {
            Response strReturn = new Response();

            try
            {
                if (MunicipalityMaster.ValidateMunicipality(_MunicipalityMaster)==true)
                {
                    strReturn.StatusMessage = "Municipality name already exists...";
                    strReturn.StatusCode = 1;
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
            catch (Exception ex) { throw; }

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
                    strReturn.StatusCode = 1;
                }
                else if (MunicipalityMaster.ValidateUpdateMunicipality(_MunicipalityMaster) == true)
                {
                    strReturn.StatusMessage = "Municipality name already exists...";
                    strReturn.StatusCode = 1;
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
                throw;
            }

            return strReturn;
        }

    }
}
