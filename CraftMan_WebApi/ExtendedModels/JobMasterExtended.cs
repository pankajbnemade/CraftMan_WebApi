using CraftMan_WebApi.Models;
using System.Collections;
namespace CraftMan_WebApi.ExtendedModels
{
    public class JobMasterExtended
    {
        public static Response NewJob(JobMaster _Job)
        {
            Response strReturn = new Response();
            try
            {
                JobMaster objJM = new JobMaster();
                if (objJM.validatejob(_Job).StatusCode > 0)
                {
                    strReturn.StatusMessage = "Job already exists...";
                    strReturn.StatusCode = 1;
                }
                else
                {
                    int i = JobMaster.InsertJob(_Job);
                    if (i > 0)
                    {
                        strReturn.StatusCode = 1;
                        strReturn.StatusMessage = "Job Registered Successfully";
                    }
                    else
                    { strReturn.StatusMessage = "Job not registered"; }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
            return strReturn;
        }
        public static ArrayList JobList()
        {
            try
            {
                return JobMaster.JobList();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
        }
    }
}
