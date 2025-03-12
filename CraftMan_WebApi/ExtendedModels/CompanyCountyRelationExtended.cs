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

        public static Response InsertNewRelation(CompanyCountyRelation _CompanyCountyRelation)
        {
            Response strReturn = new Response();

            try
            {
                if (_CompanyCountyRelation.ValidateInsertRelation(_CompanyCountyRelation).StatusCode > 0)
                {
                    strReturn.StatusMessage = "Company relation already exists...";
                    strReturn.StatusCode = 1;
                }
                else
                {
                    int i = CompanyCountyRelation.InsertNewRelation(_CompanyCountyRelation);

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

        public static Response InsertNewRelations(int CompanyId, string[]? CountyIdList, string[]? MunicipalityIdList)
        {
            Response strReturn = new Response();

            try
            {
                //if (_CompanyCountyRelation.ValidateInsertRelation(_CompanyCountyRelation).StatusCode > 0)
                //{
                //    strReturn.StatusMessage = "Company relation already exists...";
                //    strReturn.StatusCode = 1;
                //}
                //else
                //{

                List<MunicipalityMaster> MunicipalityList = MunicipalityMaster.GetMunicipalityList(MunicipalityIdList);

                if (MunicipalityList != null && CountyIdList != null && CountyIdList.Any())
                {
                    CountyIdList = CountyIdList?
                                .Where(id => Convert.ToInt32(id) != 0 && MunicipalityList != null && !MunicipalityList.Select(s => s.CountyId).Contains(Convert.ToInt32(id)))
                                .ToArray();
                }

                List<CompanyCountyRelation> _CompanyCountyRelations = new List<CompanyCountyRelation>();

                if (CountyIdList != null && CountyIdList.Any())
                {
                    foreach (string id in CountyIdList)
                    {
                        _CompanyCountyRelations.Add(new CompanyCountyRelation { pCompId = CompanyId, CountyId = Convert.ToInt32(id) });
                    }
                }

                if (MunicipalityList != null && MunicipalityList.Any())
                {
                    foreach (MunicipalityMaster pMunicipalityMaster in MunicipalityList)
                    {
                        _CompanyCountyRelations
                        .Add(new CompanyCountyRelation
                        { pCompId = CompanyId, CountyId = pMunicipalityMaster.CountyId, MunicipalityId = pMunicipalityMaster.MunicipalityId }
                        );
                    }
                }

                int i = CompanyCountyRelation.InsertNewRelations(_CompanyCountyRelations);

                if (i > 0)
                {
                    strReturn.StatusCode = 1;
                    strReturn.StatusMessage = "County relation added successfully";
                }
                else
                {
                    strReturn.StatusMessage = "County relation not added.";
                }
                //}
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
                int i = CompanyCountyRelation.DeleteRelation(_CompanyCountyRelation);

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

        public static Response DeleteRelations(int CompanyId)
        {
            Response strReturn = new Response();

            try
            {
                int i = CompanyCountyRelation.DeleteRelations(CompanyId);

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
