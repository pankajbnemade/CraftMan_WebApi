using CraftMan_WebApi.Models;
using System.Collections;
namespace CraftMan_WebApi.ExtendedModels
{
    public class CountyMasterExtended
    {
        public static CountyMaster GetCountyDetailByCountyId(int CountyId)
        {
            return CountyMaster.GetCountyDetail(CountyId);


        }
        
        public static ArrayList GetCountyList()
        {
            return CountyMaster.GetCountyList();
        }

        public static Response NewCounty(CountyMaster _CountyMaster)
        {
            Response strReturn = new Response();

            try
            {
                if (_CountyMaster.ValidateCounty(_CountyMaster).StatusCode > 0)
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
            catch (Exception ex) { throw; }

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
                else if (_CountyMaster.ValidateUpdateCounty(_CountyMaster).StatusCode > 0)
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
                throw ; 
            }

            return strReturn;
        }

    }
}
