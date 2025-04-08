using CraftMan_WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.IO;

namespace CraftMan_WebApi.ExtendedModels
{
    public class Companymasterextended
    {
        public static CompanyMaster GetCompanyDetail(string EmailId)
        {
            try
            {
                var pCompanyMaster = CompanyMaster.GetCompanyDetail(EmailId);

                if (pCompanyMaster.pCompId != null)
                {
                    ArrayList CountyRelationList = CompanyCountyRelation.GetRelationDetailByCompany(Convert.ToInt32(pCompanyMaster.pCompId));

                    ArrayList CompanyServiceList = CompanyServices.GetServicesByCompany(Convert.ToInt32(pCompanyMaster.pCompId));

                    if (CompanyServiceList != null && CompanyServiceList.Count != 0)
                    {
                        pCompanyMaster.ServiceIdList = CompanyServiceList.Cast<CompanyServices>()
                                                      .Select(service => service.ServiceId.ToString())
                                                      .Distinct()
                                                      .ToArray();

                        pCompanyMaster.ServiceList = CompanyServiceList.Cast<CompanyServices>().ToList();
                    }

                    if (CountyRelationList != null && CountyRelationList.Count != 0)
                    {
                        pCompanyMaster.CountyIdList = CountyRelationList.Cast<CompanyCountyRelation>()
                                                      .Select(relation => relation.CountyId.ToString())
                                                      .Distinct()
                                                      .ToArray();

                        pCompanyMaster.MunicipalityIdList = CountyRelationList.Cast<CompanyCountyRelation>()
                                                        .Where(w => w.MunicipalityId != 0)
                                                        .Select(relation => relation.MunicipalityId.ToString())
                                                        .Distinct()
                                                        .ToArray();

                        pCompanyMaster.CountyList = CountyRelationList.Cast<CompanyCountyRelation>()
                                                    .Select(county => new CountyMaster()
                                                    {
                                                        CountyId = county.CountyId,
                                                        CountyName = county.CountyName
                                                    })
                                                    .Distinct()
                                                    .ToList();

                        pCompanyMaster.MunicipalityList = CountyRelationList.Cast<CompanyCountyRelation>()
                                                        .Where(w => w.MunicipalityId != 0)
                                                        .Select(municipality => new MunicipalityMaster()
                                                        {
                                                            MunicipalityId = municipality.MunicipalityId == null ? 0 : Convert.ToInt32(municipality.MunicipalityId),
                                                            MunicipalityName = municipality.MunicipalityName,
                                                            CountyId = municipality.CountyId,
                                                            CountyName = municipality.CountyName
                                                        })
                                                        .Distinct()
                                                        .ToList();

                        pCompanyMaster.CountyRelationList = CountyRelationList.Cast<CompanyCountyRelation>().Distinct().ToList();
                    }

                }

                return pCompanyMaster;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
        }

        public static ArrayList GetCompany24X7ForUser(Int32 userId)
        {
            try
            {
                return CompanyMaster.GetCompany24X7ForUser(userId);
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

                        Console.WriteLine(uploadFolder);

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

                    Console.WriteLine("InsertCompany");

                    if (i > 0)
                    {
                        _CompanyMaster.pCompId = i;

                        strReturn.StatusCode = Convert.ToInt32(_CompanyMaster.pCompId);
                        strReturn.StatusMessage = "Company Registered Successfully";

                        CompanyServices.InsertNewServices(Convert.ToInt32(_CompanyMaster.pCompId), _CompanyMaster.ServiceIdList);
                        Console.WriteLine(_CompanyMaster.ServiceIdList);

                        Console.WriteLine("InsertNewServices");

                        CompanyCountyRelationExtended.InsertNewRelations(Convert.ToInt32(_CompanyMaster.pCompId), _CompanyMaster.CountyIdList, _CompanyMaster.MunicipalityIdList);

                        Console.WriteLine(_CompanyMaster.CountyIdList);
                        Console.WriteLine(_CompanyMaster.MunicipalityIdList);
                        Console.WriteLine("InsertNewRelations");

                    }
                    else
                    {
                        strReturn.StatusMessage = "Company not registered";
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

        public static Response GeneratePasswordResetToken(string email)
        {
            return CompanyResetPassword.GeneratePasswordResetToken(email); ;
        }

        public static Response ResetPassword(ResetPasswordModel model)
        {
            return CompanyResetPassword.ResetPassword(model);
        }

        public static Response UpdateCompanyIs24X7(int companyId, bool is24X7)
        {
            Response strReturn = new Response();

            try
            {
                int i = CompanyMaster.UpdateCompanyIs24X7(companyId, is24X7);

                if (i > 0)
                {
                    strReturn.StatusCode = companyId;
                    strReturn.StatusMessage = "Company Is24X7 status updated successfully";
                }
                else
                {
                    strReturn.StatusMessage = "Company Is24X7 status not updated.";
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }

            return strReturn;
        }

        public static Response UpdateActive(int companyId, string active)
        {
            Response strReturn = new Response();

            try
            {
                int i = CompanyMaster.UpdateActive(companyId, active);

                if (i > 0)
                {
                    strReturn.StatusCode = companyId;
                    strReturn.StatusMessage = "Company active status updated successfully";
                }
                else
                {
                    strReturn.StatusMessage = "Company active status not updated.";
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }

            return strReturn;
        }

        public static Response UpdateCompany(CompanyMaster _CompanyMaster)
        {
            Response strReturn = new Response();

            try
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

                int i = CompanyMaster.UpdateCompany(_CompanyMaster);

                if (i > 0)
                {
                    strReturn.StatusCode = Convert.ToInt32(_CompanyMaster.pCompId);
                    strReturn.StatusMessage = "Company updated successfully";

                    CompanyServices.DeleteServices(Convert.ToInt32(_CompanyMaster.pCompId));

                    CompanyServices.InsertNewServices(Convert.ToInt32(_CompanyMaster.pCompId), _CompanyMaster.ServiceIdList);

                    CompanyCountyRelationExtended.DeleteRelations(Convert.ToInt32(_CompanyMaster.pCompId));
                    CompanyCountyRelationExtended.InsertNewRelations(Convert.ToInt32(_CompanyMaster.pCompId), _CompanyMaster.CountyIdList, _CompanyMaster.MunicipalityIdList);
                }
                else
                {
                    strReturn.StatusMessage = "Company not registered";
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
