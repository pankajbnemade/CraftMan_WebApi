﻿using CraftMan_WebApi.DataAccessLayer;
using CraftMan_WebApi.Helper;
using CraftMan_WebApi.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Collections;
using System.Drawing.Imaging;
using System.Drawing;
namespace CraftMan_WebApi.ExtendedModels
{
    public class IssueTicketExtended
    {
        public static ArrayList GetTicketdetailsByUser(IssueTicketForUserFilter filter)
        {
            try
            {
                return IssueTicket.GetTicketsByUser(filter);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
        }

        public static IssueTicket GetTicketdetailByTicketId(int TicketId)
        {
            try
            {
                return IssueTicket.GetTicketByTicketId(TicketId);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
        }

        public static ArrayList GetTicketsForCompany(IssueTicketForCompanyFilter filter)
        {
            try
            {
                return IssueTicket.GetTicketsForCompany(filter);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
        }

        public static Response IssueNewTicket(IssueTicket _IssueTicket)
        {
            Response strReturn = new Response();

            try
            {
                int i = IssueTicket.InsertTicket(_IssueTicket);

                if (i > 0)
                {
                    _IssueTicket.TicketId = i;

                    strReturn.StatusCode = i;
                    strReturn.StatusMessage = "Job request registered successfully.";

                    if (_IssueTicket.Images != null && _IssueTicket.Images.Count > 0)
                    {
                        string uploadFolder = @"C:\CraftManImages\TicketImages";

                        if (!Directory.Exists(uploadFolder))
                            Directory.CreateDirectory(uploadFolder);

                        List<IssueTicketImage> uploadedImages = new List<IssueTicketImage>();

                        foreach (var image in _IssueTicket.Images)
                        {
                            string originalName = Path.GetFileNameWithoutExtension(image.FileName);
                            string compressedImageName = Guid.NewGuid().ToString() + ".jpg"; // Save as JPEG
                            string imagePath = Path.Combine(uploadFolder, compressedImageName);

                            using (var imageStream = image.OpenReadStream())
                            using (var originalImage = Image.FromStream(imageStream))
                            {
                                // Resize logic
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
                                    // Set JPEG compression
                                    var jpegEncoder = ImageCodecInfo.GetImageDecoders()
                                        .FirstOrDefault(codec => codec.FormatID == ImageFormat.Jpeg.Guid);

                                    var encoderParameters = new EncoderParameters(1);
                                    encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 60L); // Adjust quality here

                                    // Save as compressed JPEG
                                    resizedImage.Save(imagePath, jpegEncoder, encoderParameters);
                                }
                            }

                            // Save image info to list
                            IssueTicketImage _IssueTicketImage = new IssueTicketImage
                            {
                                TicketId = _IssueTicket.TicketId,
                                ImageName = originalName,
                                ImagePath = imagePath
                            };

                            uploadedImages.Add(_IssueTicketImage);
                        }

                        //foreach (var image in _IssueTicket.Images)
                        //{
                        //    string originalName = Path.GetFileNameWithoutExtension(image.FileName);

                        //    string imageName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                        //    string imagePath = Path.Combine(uploadFolder, imageName);

                        //    using (var stream = new FileStream(imagePath, FileMode.Create))
                        //    {
                        //        image.CopyTo(stream);
                        //    }

                        //    IssueTicketImage _IssueTicketImage = new IssueTicketImage();

                        //    _IssueTicketImage.TicketId = _IssueTicket.TicketId;
                        //    _IssueTicketImage.ImageName = originalName;
                        //    _IssueTicketImage.ImagePath = imagePath;

                        //    uploadedImages.Add(_IssueTicketImage);
                        //}

                        foreach (IssueTicketImage _IssueTicketImage in uploadedImages)
                        {
                            i = IssueTicket.InsertTicketImage(_IssueTicketImage);

                            if (i == 0)
                            {
                                strReturn.StatusMessage = "Job request registered successfully. But images are not uploaded.";
                            }
                        }
                    }
                }
                else
                {
                    strReturn.StatusMessage = "Not registered. This may be because of user registering same request again.";
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }

            return strReturn;
        }

        public static Response UpdateTicketReview(IssueTicketReview _IssueTicketReview)
        {
            Response strReturn = new Response();

            try
            {
                int i = IssueTicket.UpdateTicketReview(_IssueTicketReview);

                if (i > 0)
                {
                    strReturn.StatusCode = 1;
                    strReturn.StatusMessage = "Star rating updated successfully.";
                }
                else
                {
                    strReturn.StatusMessage = "Star rating not updated.";
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }

            return strReturn;
        }

        public static Response UpdateCompanyComment(IssueTicketCompanyComment _IssueTicketCompanyComment)
        {
            Response strReturn = new Response();

            try
            {
                IssueTicket pIssueTicket = IssueTicket.GetTicketByTicketId(_IssueTicketCompanyComment.TicketId);

                if (pIssueTicket != null && pIssueTicket.TicketId != 0)
                {
                    if (pIssueTicket.Status != TicketStatus.Inprogress.ToString())
                    {
                        strReturn.StatusCode = 0;
                        strReturn.StatusMessage = "Comment not updated. Current job request status should be " + TicketStatus.Inprogress.ToString() + ".";
                    }
                    else
                    {
                        int i = IssueTicket.UpdateCompanyComment(_IssueTicketCompanyComment);

                        if (i > 0)
                        {
                            strReturn.StatusCode = 1;
                            strReturn.StatusMessage = "Comment updated successfully";

                            if (_IssueTicketCompanyComment.Images != null && _IssueTicketCompanyComment.Images.Count > 0)
                            {
                                string uploadFolder = @"C:\CraftManImages\TicketImages";

                                if (!Directory.Exists(uploadFolder))
                                    Directory.CreateDirectory(uploadFolder);

                                List<IssueTicketImage> uploadedImages = new List<IssueTicketImage>();

                                foreach (var image in _IssueTicketCompanyComment.Images)
                                {
                                    string originalName = Path.GetFileNameWithoutExtension(image.FileName);
                                    string compressedImageName = Guid.NewGuid().ToString() + ".jpg"; // Save all as JPG
                                    string imagePath = Path.Combine(uploadFolder, compressedImageName);

                                    using (var imageStream = image.OpenReadStream())
                                    using (var originalImage = Image.FromStream(imageStream))
                                    {
                                        // Resize if larger than 1024px width
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
                                                                .FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);

                                            var encoderParameters = new EncoderParameters(1);
                                            encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 60L); // Compress to 60% quality

                                            resizedImage.Save(imagePath, jpegEncoder, encoderParameters);
                                        }
                                    }

                                    IssueTicketImage _IssueTicketImage = new IssueTicketImage
                                    {
                                        TicketId = _IssueTicketCompanyComment.TicketId,
                                        ImageName = originalName,
                                        ImagePath = imagePath
                                    };

                                    uploadedImages.Add(_IssueTicketImage);
                                }


                                //foreach (var image in _IssueTicketCompanyComment.Images)
                                //{
                                //    string originalName = Path.GetFileNameWithoutExtension(image.FileName);

                                //    string imageName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                                //    string imagePath = Path.Combine(uploadFolder, imageName);

                                //    using (var stream = new FileStream(imagePath, FileMode.Create))
                                //    {
                                //        image.CopyTo(stream);
                                //    }

                                //    IssueTicketImage _IssueTicketImage = new IssueTicketImage();

                                //    _IssueTicketImage.TicketId = _IssueTicketCompanyComment.TicketId;
                                //    _IssueTicketImage.ImageName = originalName;
                                //    _IssueTicketImage.ImagePath = imagePath;

                                //    uploadedImages.Add(_IssueTicketImage);
                                //}

                                foreach (IssueTicketImage _IssueTicketImage in uploadedImages)
                                {
                                    i = IssueTicket.InsertTicketWorkImage(_IssueTicketImage);

                                    if (i == 0)
                                    {
                                        strReturn.StatusMessage = "Comment updated Successfully. But images are not uploaded.";
                                    }
                                }
                            }

                        }
                        else
                        {
                            strReturn.StatusMessage = "Comment not updated.";
                        }
                    }
                }
                else
                {
                    strReturn.StatusMessage = "Record not exists.";
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }

            return strReturn;
        }

        public static Response UpdateTicketStatus(IssueTicketUpdateStatus _IssueTicketUpdateStatus)
        {
            Response strReturn = new Response();

            try
            {
                IssueTicket pIssueTicket = IssueTicket.GetTicketByTicketId(_IssueTicketUpdateStatus.TicketId);

                if (pIssueTicket == null)
                {
                    strReturn.StatusCode = 0;
                    strReturn.StatusMessage = "Invalid details.";
                }
                else if (pIssueTicket.TicketId == 0)
                {
                    strReturn.StatusCode = 0;
                    strReturn.StatusMessage = "Invalid details.";
                }
                else if (Enum.IsDefined(typeof(TicketStatus), _IssueTicketUpdateStatus.Status) == false)
                {
                    strReturn.StatusCode = 0;
                    strReturn.StatusMessage = "Status not updated. Invalid Status " + _IssueTicketUpdateStatus.Status + ".";
                }
                else if (TicketStatus.Completed.ToString() == _IssueTicketUpdateStatus.Status
                     && IssueTicket.ValidateClosingOTP(_IssueTicketUpdateStatus) == false)
                {
                    strReturn.StatusCode = 0;
                    strReturn.StatusMessage = "Invalid OTP. Please check and try again.";
                }
                else if (TicketStatus.Completed.ToString() == _IssueTicketUpdateStatus.Status
                    && pIssueTicket.Status != TicketStatus.Inprogress.ToString())
                {
                    strReturn.StatusCode = 0;
                    strReturn.StatusMessage = "Status not updated. Current status should be " + TicketStatus.Inprogress.ToString() + ".";
                }
                else if (TicketStatus.Accepted.ToString() == _IssueTicketUpdateStatus.Status
                    && pIssueTicket.Status != TicketStatus.Created.ToString())
                {
                    strReturn.StatusCode = 0;
                    strReturn.StatusMessage = "Status not updated. Current status should be " + TicketStatus.Created.ToString() + ".";
                }
                else if (TicketStatus.Accepted.ToString() == _IssueTicketUpdateStatus.Status
                    && (_IssueTicketUpdateStatus.CompanyId == 0 || _IssueTicketUpdateStatus.CompanyId == null))
                {
                    strReturn.StatusCode = 0;
                    strReturn.StatusMessage = "Status not updated. Invalid partner.";
                }
                else if (TicketStatus.Accepted.ToString() == _IssueTicketUpdateStatus.Status
                     && IssueTicket.ValidateAcceptedOTP(_IssueTicketUpdateStatus) == false)
                {
                    strReturn.StatusCode = 0;
                    strReturn.StatusMessage = "Invalid OTP. Please check and try again.";
                }

                else if (TicketStatus.Inprogress.ToString() == _IssueTicketUpdateStatus.Status
                    && pIssueTicket.Status != TicketStatus.Accepted.ToString())
                {
                    strReturn.StatusCode = 0;
                    strReturn.StatusMessage = "Status not updated. Current status should be " + TicketStatus.Accepted.ToString() + ".";
                }
                else
                {
                    int i = IssueTicket.UpdateTicketStatus(_IssueTicketUpdateStatus);

                    if (i > 0)
                    {
                        strReturn.StatusCode = 1;
                        strReturn.StatusMessage = "Status updated successfully.";
                    }
                    else
                    {
                        strReturn.StatusMessage = "Status not updated.";
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

        public static List<string> GetCompanyDeviceTokenList(Int32 ticketId)
        {
            try
            {
                return IssueTicket.GetCompanyDeviceTokenList(ticketId);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
        }


        public static ArrayList GetCompanyListForTicket(List<string> tokenList)
        {
            try
            {
                return IssueTicket.GetCompanyListForTicket(tokenList);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
        }

    }
}
