using CraftMan_WebApi.Models;
using System.Collections;
namespace CraftMan_WebApi.ExtendedModels
{
    public class CompanyCountyRelationExtended
    {
        public static ArrayList GetRelationDetailByCompany(int CompanyId)
        {
            try
            {
                return CompanyCountyRelation.GetRelationDetailByCompany(CompanyId);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
        }

        public static Response NewRelation(CompanyCountyRelation _CompanyCountyRelation)
        {
            Response strReturn = new Response();

            try
            {
                if (_CompanyCountyRelation.ValidateInsertRecord(_CompanyCountyRelation).StatusCode > 0)
                {
                    strReturn.StatusMessage = "Company relation already exists...";
                    strReturn.StatusCode = 1;
                }
                else
                {
                    int i = CompanyCountyRelation.InsertNewRecord(_CompanyCountyRelation);

                    if (i > 0)
                    {
                        strReturn.StatusCode = 1;
                        strReturn.StatusMessage = "Company relation added successfully";
                    }
                    else
                    { strReturn.StatusMessage = "Company relation not added."; }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }

            return strReturn;
        }

        public static Response DeleteRelation(CompanyCountyRelation _CompanyCountyRelation)
        {
            Response strReturn = new Response();

            try
            {
                int i = CompanyCountyRelation.DeleteRecord(_CompanyCountyRelation);

                if (i > 0)
                {
                    strReturn.StatusCode = 1;
                    strReturn.StatusMessage = "Company relation deleted successfully";
                }
                else
                {
                    strReturn.StatusMessage = "Company relation not deleted. This May be relation not exists...";
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
