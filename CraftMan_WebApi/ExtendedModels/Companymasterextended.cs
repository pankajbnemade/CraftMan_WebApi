using CraftMan_WebApi.Models;
using System;
using System.Collections;

namespace CraftMan_WebApi.ExtendedModels
{
    public class Companymasterextended
    {
        public static CompanyMaster GetCompanyDetail(string Username)
        {
            try
            {
                return CompanyMaster.GetCompanyDetail(Username);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
        }
        public static int GetTotalcnt(string Username)
        {
            try
            {
                return CompanyMaster.GetTotalcnt(Username);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
        }
        public static int GetActivecountnoofcraftsman(string Username)
        {
            try
            {
                return CompanyMaster.GetTotalcnt(Username);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
        }


        public static ArrayList GetCompEmployeeList(string _user)
        {
            try
            {
                return CompanyEmp.GetCompEmplist(_user);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
        }

        public static Response RegistrationCompany(CompanyMaster _Company)
        {
            Response strReturn = new Response();
            try
            {
                if (_Company.ValidateCompany(_Company).StatusCode > 0)
                {
                    strReturn.StatusMessage = "Company already exists...";
                    strReturn.StatusCode = 1;
                }
                else
                {
                    int i = CompanyMaster.InsertCompany(_Company);//joblist added

                    if (i > 0)
                    {
                        strReturn.StatusCode = 1;
                        strReturn.StatusMessage = "Company Registered Successfully";
                    }
                    else
                    { strReturn.StatusMessage = "Company not registered"; }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
            return strReturn;
        }
        public static Response LoginValidateForCompany(LoginComp _comp)
        {
            try
            {
                Response strReturn = new Response();
                CompanyMaster objCM = new CompanyMaster();
                objCM.Password = _comp.Password;
                objCM.EmailId = _comp.EmailId;
                if (objCM.ValidateCompany(objCM).StatusCode > 0)
                {
                    strReturn.StatusMessage = "Valid Company ";
                    strReturn.StatusCode = 1;
                }
                else
                {
                    strReturn.StatusMessage = "InValid Company ";
                    strReturn.StatusCode = 0;
                }
                return strReturn;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
        }
    }
}
