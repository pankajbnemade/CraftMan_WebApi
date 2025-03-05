using CraftMan_WebApi.Models;
using System.Collections;
namespace CraftMan_WebApi.ExtendedModels
{
    public class CountyMasterExtended
    {
        public static CountyMaster GetCountyDetailByCountyId(int CountyId)
        {
            try
            {
                return CountyMaster.GetCountyDetail(CountyId);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
        }

        public static ArrayList GetCountyList()
        {
            try
            {
                return CountyMaster.GetCountyList();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
        }
        public static ArrayList GetCountyListByCompanyId(int CompanyId)
        {
            try
            {
                return CountyMaster.GetCountyListByCompanyId(CompanyId);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
        }

        public static Response NewCounty(CountyMaster _CountyMaster)
        {
            Response strReturn = new Response();

            try
            {
                if (CountyMaster.ValidateCounty(_CountyMaster) == true)
                {
                    strReturn.StatusMessage = "County name already exists...";
                    strReturn.StatusCode = 1;
                }
                else
                {
                    int i = CountyMaster.InsertCounty(_CountyMaster);

                    if (i > 0)
                    {
                        strReturn.StatusCode = 1;
                        strReturn.StatusMessage = "County added successfully";
                    }
                    else
                    { strReturn.StatusMessage = "County not added."; }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }

            return strReturn;
        }


        public static Response UpdateCounty(CountyMaster _CountyMaster)
        {
            Response strReturn = new Response();

            try
            {
                if (CountyMaster.GetCountyDetail(_CountyMaster.CountyId).CountyId == 0)
                {
                    strReturn.StatusMessage = "County details not exists for update...";
                    strReturn.StatusCode = 1;
                }
                else if (CountyMaster.ValidateUpdateCounty(_CountyMaster) == true)
                {
                    strReturn.StatusMessage = "County name already exists...";
                    strReturn.StatusCode = 1;
                }
                else
                {
                    int i = CountyMaster.UpdateCounty(_CountyMaster);

                    if (i > 0)
                    {
                        strReturn.StatusCode = 1;
                        strReturn.StatusMessage = "County updated successfully";
                    }
                    else
                    {
                        strReturn.StatusMessage = "County not updated.";
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
