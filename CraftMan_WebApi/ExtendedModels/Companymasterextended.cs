using CraftMan_WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.IO;

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

        public static Response RegistrationCompany(CompanyMaster _CompanyMaster)
        {
            Response strReturn = new Response();
            try
            {
                if (_CompanyMaster.ValidateCompany(_CompanyMaster).StatusCode > 0)
                {
                    strReturn.StatusMessage = "Company already exists...";
                    strReturn.StatusCode = 1;
                }
                else
                {
                    if (_CompanyMaster.LogoImage != null)
                    {
                        string uploadFolder = @"C:\CraftManImages\CompanyImages";

                        if (!Directory.Exists(uploadFolder))
                            Directory.CreateDirectory(uploadFolder);

                        string originalName = Path.GetFileNameWithoutExtension(_CompanyMaster.LogoImage.FileName);

                        string imageName = Guid.NewGuid().ToString() + Path.GetExtension(_CompanyMaster.LogoImage.FileName);
                        string imagePath = Path.Combine(uploadFolder, imageName);

                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            _CompanyMaster.LogoImage.CopyTo(stream);
                        }

                        _CompanyMaster.LogoImageName = originalName;
                        _CompanyMaster.LogoImagePath = imagePath;
                    }
                    else
                    {
                        _CompanyMaster.LogoImageName = "";
                        _CompanyMaster.LogoImagePath = "";
                    }



                    int i = CompanyMaster.InsertCompany(_CompanyMaster);//joblist added

                    if (i > 0)
                    {
                        _CompanyMaster.pCompId = i;

                        strReturn.StatusCode = _CompanyMaster.pCompId;
                        strReturn.StatusMessage = "Company Registered Successfully";

                        CompanyServices.InsertNewServices(_CompanyMaster.pCompId, _CompanyMaster.ServiceList);

                        CompanyCountyRelationExtended.NewRelations(_CompanyMaster.pCompId, _CompanyMaster.CountyList, _CompanyMaster.MunicipalityList);
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

        public static Response LoginValidateForCompany(LoginComp _User)
        {
            try
            {
                return CompanyMaster.LoginValidateForCompanyUser(_User);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
        }

        //public static Response LoginValidateForCompany(LoginComp _comp)
        //{
        //    try
        //    {
        //        Response strReturn = new Response();
        //        CompanyMaster objCM = new CompanyMaster();
        //        objCM.Password = _comp.Password;
        //        objCM.EmailId = _comp.EmailId;
        //        if (objCM.ValidateCompany(objCM).StatusCode > 0)
        //        {
        //            strReturn.StatusMessage = "Valid Company ";
        //            strReturn.StatusCode = 1;
        //        }
        //        else
        //        {
        //            strReturn.StatusMessage = "InValid Company ";
        //            strReturn.StatusCode = 0;
        //        }
        //        return strReturn;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLogger.LogError(ex);
        //        throw new ApplicationException("An error occurred.", ex);
        //    }
        //}


        public static Response GeneratePasswordResetToken(string email)
        {
            return CompanyResetPassword.GeneratePasswordResetToken(email); ;
        }

        public static Response ResetPassword(ResetPasswordModel model)
        {
            return CompanyResetPassword.ResetPassword(model.Token, model.NewPassword);
        }

    }
}
