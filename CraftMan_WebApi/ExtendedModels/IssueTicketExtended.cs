using CraftMan_WebApi.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Collections;
namespace CraftMan_WebApi.ExtendedModels
{
    public class IssueTicketExtended
    {
        public static ArrayList GetTicketdetailsByUser(string _user)
        {
            return IssueTicket.GetTicketsByUser(_user);
        }

        public static IssueTicket GetTicketdetailByTicketId(int TicketId)
        {
            return IssueTicket.GetTicketByTicketId(TicketId);
        }

        public static ArrayList GetTicketsByCompany(int CompanyId, int CountyId, int MunicipalityId)
        {
            return IssueTicket.GetTicketsByCompany(CompanyId, CountyId, MunicipalityId);
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

                    strReturn.StatusCode = 1;
                    strReturn.StatusMessage = "Issue Registered Successfully";

                    if (_IssueTicket.Images != null && _IssueTicket.Images.Count > 0)
                    {
                        string uploadFolder = @"C:\UploadedImages";

                        if (!Directory.Exists(uploadFolder))
                            Directory.CreateDirectory(uploadFolder);

                        List<IssueTicketImage> uploadedImages = new List<IssueTicketImage>();

                        foreach (var image in _IssueTicket.Images)
                        {
                            string originalName = Path.GetFileNameWithoutExtension(image.FileName);

                            string imageName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                            string imagePath = Path.Combine(uploadFolder, imageName);

                            using (var stream = new FileStream(imagePath, FileMode.Create))
                            {
                                image.CopyTo(stream);
                            }

                            IssueTicketImage _IssueTicketImage = new IssueTicketImage();

                            _IssueTicketImage.TicketId = _IssueTicket.TicketId;
                            _IssueTicketImage.ImageName = originalName;
                            _IssueTicketImage.ImagePath = imagePath;

                            uploadedImages.Add(_IssueTicketImage);
                        }

                        foreach (IssueTicketImage _IssueTicketImage in uploadedImages)
                        {
                            i = IssueTicket.InsertTicketImage(_IssueTicketImage);

                            if (i == 0)
                            {
                                strReturn.StatusMessage = "Issue Registered Successfully. But images are not saved.";
                            }
                        }
                    }
                }
                else
                {
                    strReturn.StatusMessage = "Issue not registered.This may be because of user registering same issue again";
                }

            }
            catch (Exception ex) {
                ErrorLogger.LogError(ex);

                strReturn.StatusCode = 0;
                strReturn.StatusMessage = "An error occurred while processing the request.";
            }

            return strReturn;
        }
    }
}
