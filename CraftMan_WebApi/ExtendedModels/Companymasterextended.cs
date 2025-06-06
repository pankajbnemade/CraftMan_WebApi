﻿using CraftMan_WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.Drawing.Imaging;
using System.Drawing;
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
                                                        .Where(w => w.MunicipalityId != 0 && w.MunicipalityId != null)
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
                                                        .Where(w => w.MunicipalityId != 0 && w.MunicipalityId != null)
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

        public static ArrayList GetCompanyList(int? countyId, int? municipalityId, int? serviceId)
        {
            try
            {
                return CompanyMaster.GetCompanyList(countyId, municipalityId, serviceId);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
        }

        public static ArrayList GetCompany24X7ForUser(Int32 userId, int? serviceId)
        {
            try
            {
                return CompanyMaster.GetCompany24X7ForUser(userId, serviceId);
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
                if (CompanyMaster.ValidateCompany(_CompanyMaster).StatusCode > 0)
                {
                    strReturn.StatusMessage = "Already exists...";
                    strReturn.StatusCode = 0;
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

                        using (var imageStream = _CompanyMaster.LogoImage.OpenReadStream())

                        using (var originalImage = Image.FromStream(imageStream))
                        {
                            // Resize logic if needed
                            int maxDimension = 1024;
                            int newWidth = originalImage.Width;
                            int newHeight = originalImage.Height;

                            if (originalImage.Width > maxDimension || originalImage.Height > maxDimension)
                            {
                                float ratioX = (float)maxDimension / originalImage.Width;
                                float ratioY = (float)maxDimension / originalImage.Height;
                                float ratio = Math.Min(ratioX, ratioY);

                                newWidth = (int)(originalImage.Width * ratio);
                                newHeight = (int)(originalImage.Height * ratio);
                            }

                            using (var resizedImage = new Bitmap(originalImage, new Size(newWidth, newHeight)))
                            {
                                var jpegEncoder = ImageCodecInfo.GetImageDecoders()
                                    .FirstOrDefault(codec => codec.FormatID == ImageFormat.Jpeg.Guid);

                                var encoderParameters = new EncoderParameters(1);
                                encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 60L); // 60% quality

                                resizedImage.Save(imagePath, jpegEncoder, encoderParameters);
                            }
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

                        strReturn.StatusCode = Convert.ToInt32(_CompanyMaster.pCompId);
                        strReturn.StatusMessage = "Registered Successfully.";

                        CompanyServices.InsertNewServices(Convert.ToInt32(_CompanyMaster.pCompId), _CompanyMaster.ServiceIdList);

                        CompanyCountyRelationExtended.InsertNewRelations(Convert.ToInt32(_CompanyMaster.pCompId), _CompanyMaster.CountyIdList, _CompanyMaster.MunicipalityIdList);
                    }
                    else
                    {
                        strReturn.StatusMessage = "Not registered.";
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
                    strReturn.StatusMessage = "Is24X7 status updated successfully.";
                }
                else
                {
                    strReturn.StatusMessage = "Is24X7 status not updated.";
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }

            return strReturn;
        }

        //public static Response UpdateActive(int companyId, string active)
        //{
        //    Response strReturn = new Response();

        //    try
        //    {
        //        int i = CompanyMaster.UpdateActive(companyId, active);

        //        if (i > 0)
        //        {
        //            strReturn.StatusCode = companyId;
        //            strReturn.StatusMessage = "Company active status updated successfully";
        //        }
        //        else
        //        {
        //            strReturn.StatusMessage = "Company active status not updated.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLogger.LogError(ex);
        //        throw new ApplicationException("An error occurred.", ex);
        //    }

        //    return strReturn;
        //}

        public static Response UpdateCompany(CompanyMasterUpdateModel _CompanyMaster)
        {
            Response strReturn = new Response();

            try
            {
                if (CompanyMaster.ValidateCompanyUpdate(_CompanyMaster).StatusCode > 0)
                {
                    strReturn.StatusMessage = "Already exists for emailId / User name...";
                    strReturn.StatusCode = 0;
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

                        using (var imageStream = _CompanyMaster.LogoImage.OpenReadStream())

                        using (var originalImage = Image.FromStream(imageStream))
                        {
                            // Resize logic if needed
                            int maxDimension = 1024;
                            int newWidth = originalImage.Width;
                            int newHeight = originalImage.Height;

                            if (originalImage.Width > maxDimension || originalImage.Height > maxDimension)
                            {
                                float ratioX = (float)maxDimension / originalImage.Width;
                                float ratioY = (float)maxDimension / originalImage.Height;
                                float ratio = Math.Min(ratioX, ratioY);

                                newWidth = (int)(originalImage.Width * ratio);
                                newHeight = (int)(originalImage.Height * ratio);
                            }

                            using (var resizedImage = new Bitmap(originalImage, new Size(newWidth, newHeight)))
                            {
                                var jpegEncoder = ImageCodecInfo.GetImageDecoders()
                                    .FirstOrDefault(codec => codec.FormatID == ImageFormat.Jpeg.Guid);

                                var encoderParameters = new EncoderParameters(1);
                                encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 60L); // 60% quality

                                resizedImage.Save(imagePath, jpegEncoder, encoderParameters);
                            }
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
                        CompanyServices.DeleteServices(Convert.ToInt32(_CompanyMaster.pCompId));

                        CompanyServices.InsertNewServices(Convert.ToInt32(_CompanyMaster.pCompId), _CompanyMaster.ServiceIdList);

                        CompanyCountyRelationExtended.DeleteRelations(Convert.ToInt32(_CompanyMaster.pCompId));
                        CompanyCountyRelationExtended.InsertNewRelations(Convert.ToInt32(_CompanyMaster.pCompId), _CompanyMaster.CountyIdList, _CompanyMaster.MunicipalityIdList);

                        strReturn.StatusCode = Convert.ToInt32(_CompanyMaster.pCompId);
                        strReturn.StatusMessage = "Updated successfully.";

                    }
                    else
                    {
                        strReturn.StatusMessage = "Not registered";
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
