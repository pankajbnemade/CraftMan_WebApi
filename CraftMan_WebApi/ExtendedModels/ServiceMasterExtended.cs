using CraftMan_WebApi.Models;
using System.Collections;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing;
using System.Drawing.Imaging;
using Org.BouncyCastle.Asn1.X509;


namespace CraftMan_WebApi.ExtendedModels
{
    public class ServiceMasterExtended
    {
        public static ServiceMaster GetServiceDetailByServiceId(int ServiceId)
        {
            try
            {
                return ServiceMaster.GetServiceDetail(ServiceId);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
        }

        public static ArrayList GetServiceList()
        {
            try
            {
                return ServiceMaster.GetServiceList();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
        }

        public static Response NewService(ServiceMaster _ServiceMaster)
        {
            Response strReturn = new Response();

            try
            {
                if (ServiceMaster.ValidateService(_ServiceMaster) == true)
                {
                    strReturn.StatusMessage = "Service name already exists...";
                    strReturn.StatusCode = 0;
                }
                else
                {
                    if (_ServiceMaster.ServiceImage != null)
                    {
                        string uploadFolder = @"C:\CraftManImages\ServiceImages";

                        if (!Directory.Exists(uploadFolder))
                            Directory.CreateDirectory(uploadFolder);

                        string extension = Path.GetExtension(_ServiceMaster.ServiceImage.FileName).ToLowerInvariant();
                        string originalName = Path.GetFileNameWithoutExtension(_ServiceMaster.ServiceImage.FileName);

                        string imageName = Guid.NewGuid().ToString() + Path.GetExtension(_ServiceMaster.ServiceImage.FileName);
                        string imagePath = Path.Combine(uploadFolder, imageName);

                        using (var imageStream = _ServiceMaster.ServiceImage.OpenReadStream())
                        {
                            //if (extension == ".svg")
                            //{
                            // Just save the SVG as-is
                            using (var fileStream = File.Create(imagePath))
                            {
                                imageStream.CopyTo(fileStream);
                            }
                            //}
                            //else
                            //{

                            //    using (var originalImage = Image.FromStream(imageStream))
                            //    {
                            //        // Resize logic if needed
                            //        int maxDimension = 1024;
                            //        int newWidth = originalImage.Width;
                            //        int newHeight = originalImage.Height;

                            //        if (originalImage.Width > maxDimension || originalImage.Height > maxDimension)
                            //        {
                            //            float ratioX = (float)maxDimension / originalImage.Width;
                            //            float ratioY = (float)maxDimension / originalImage.Height;
                            //            float ratio = Math.Min(ratioX, ratioY);

                            //            newWidth = (int)(originalImage.Width * ratio);
                            //            newHeight = (int)(originalImage.Height * ratio);
                            //        }

                            //        using (var resizedImage = new Bitmap(originalImage, new Size(newWidth, newHeight)))
                            //        {
                            //            var jpegEncoder = ImageCodecInfo.GetImageDecoders()
                            //                .FirstOrDefault(codec => codec.FormatID == ImageFormat.Jpeg.Guid);

                            //            var encoderParameters = new EncoderParameters(1);
                            //            encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 60L); // 60% quality

                            //            resizedImage.Save(imagePath, jpegEncoder, encoderParameters);
                            //        }
                            //    }

                            //}
                        }

                        _ServiceMaster.ImageName = originalName;
                        _ServiceMaster.ImagePath = imagePath;
                    }
                    else
                    {
                        _ServiceMaster.ImageName = "";
                        _ServiceMaster.ImagePath = "";
                    }


                    int i = ServiceMaster.InsertService(_ServiceMaster);

                    if (i > 0)
                    {
                        strReturn.StatusCode = 1;
                        strReturn.StatusMessage = "Service added successfully";
                    }
                    else
                    { strReturn.StatusMessage = "Service not added."; }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }

            return strReturn;
        }


        public static Response UpdateService(ServiceMaster _ServiceMaster)
        {
            Response strReturn = new Response();

            try
            {
                if (ServiceMaster.GetServiceDetail(_ServiceMaster.ServiceId).ServiceId == 0)
                {
                    strReturn.StatusMessage = "Service details not exists for update...";
                    strReturn.StatusCode = 1;
                }
                else if (ServiceMaster.ValidateUpdateService(_ServiceMaster) == true)
                {
                    strReturn.StatusMessage = "Service name already exists...";
                    strReturn.StatusCode = 1;
                }
                else
                {
                    if (_ServiceMaster.ServiceImage != null)
                    {
                        string uploadFolder = @"C:\CraftManImages\ServiceImages";

                        if (!Directory.Exists(uploadFolder))
                            Directory.CreateDirectory(uploadFolder);

                        string extension = Path.GetExtension(_ServiceMaster.ServiceImage.FileName).ToLowerInvariant();

                        string originalName = Path.GetFileNameWithoutExtension(_ServiceMaster.ServiceImage.FileName);

                        string imageName = Guid.NewGuid().ToString() + Path.GetExtension(_ServiceMaster.ServiceImage.FileName);
                        string imagePath = Path.Combine(uploadFolder, imageName);

                        using (var imageStream = _ServiceMaster.ServiceImage.OpenReadStream())
                        {
                            //if (extension == ".svg")
                            //{
                            // Just save the SVG as-is
                            using (var fileStream = File.Create(imagePath))
                            {
                                imageStream.CopyTo(fileStream);
                            }
                            //}
                            //else
                            //{

                            //    using (var originalImage = Image.FromStream(imageStream))
                            //    {
                            //        // Resize logic if needed
                            //        int maxDimension = 1024;
                            //        int newWidth = originalImage.Width;
                            //        int newHeight = originalImage.Height;

                            //        if (originalImage.Width > maxDimension || originalImage.Height > maxDimension)
                            //        {
                            //            float ratioX = (float)maxDimension / originalImage.Width;
                            //            float ratioY = (float)maxDimension / originalImage.Height;
                            //            float ratio = Math.Min(ratioX, ratioY);

                            //            newWidth = (int)(originalImage.Width * ratio);
                            //            newHeight = (int)(originalImage.Height * ratio);
                            //        }

                            //        using (var resizedImage = new Bitmap(originalImage, new Size(newWidth, newHeight)))
                            //        {
                            //            var jpegEncoder = ImageCodecInfo.GetImageDecoders()
                            //                .FirstOrDefault(codec => codec.FormatID == ImageFormat.Jpeg.Guid);

                            //            var encoderParameters = new EncoderParameters(1);
                            //            encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 60L); // 60% quality

                            //            resizedImage.Save(imagePath, jpegEncoder, encoderParameters);
                            //        }
                            //    }

                            //}
                        }

                        _ServiceMaster.ImageName = originalName;
                        _ServiceMaster.ImagePath = imagePath;
                    }
                    else
                    {
                        _ServiceMaster.ImageName = "";
                        _ServiceMaster.ImagePath = "";
                    }

                    int i = ServiceMaster.UpdateService(_ServiceMaster);

                    if (i > 0)
                    {
                        strReturn.StatusCode = 1;
                        strReturn.StatusMessage = "Service updated successfully";
                    }
                    else
                    {
                        strReturn.StatusMessage = "Service not updated.";
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
