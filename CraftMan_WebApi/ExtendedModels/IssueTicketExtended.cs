using CraftMan_WebApi.DataAccessLayer;
using CraftMan_WebApi.Helper;
using CraftMan_WebApi.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Collections;
namespace CraftMan_WebApi.ExtendedModels
{
    public class IssueTicketExtended
    {
        public static ArrayList GetTicketdetailsByUser(string _user)
        {
            try
            {
                return IssueTicket.GetTicketsByUser(_user);
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

        public static ArrayList GetTicketsForCompany(int CompanyId, int? CountyId, int? MunicipalityId)
        {
            try
            {
                return IssueTicket.GetTicketsForCompany(CompanyId, CountyId, MunicipalityId);
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
                    strReturn.StatusMessage = "Issue Registered Successfully";

                    if (_IssueTicket.Images != null && _IssueTicket.Images.Count > 0)
                    {
                        string uploadFolder = @"C:\CraftManImages\TicketImages";

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
                                strReturn.StatusMessage = "Issue Registered Successfully. But images are not uploaded.";
                            }
                        }
                    }
                }
                else
                {
                    strReturn.StatusMessage = "Issue not registered.This may be because of user registering same issue again";
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
                    strReturn.StatusMessage = "Ticket star rating updated successfully";
                }
                else
                {
                    strReturn.StatusMessage = "Ticket star rating not updated.";
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
                        strReturn.StatusMessage = "Company comment not updated. Current ticket status should be " + TicketStatus.Inprogress.ToString();
                    }
                }

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

                            string imageName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                            string imagePath = Path.Combine(uploadFolder, imageName);

                            using (var stream = new FileStream(imagePath, FileMode.Create))
                            {
                                image.CopyTo(stream);
                            }

                            IssueTicketImage _IssueTicketImage = new IssueTicketImage();

                            _IssueTicketImage.TicketId = _IssueTicketCompanyComment.TicketId;
                            _IssueTicketImage.ImageName = originalName;
                            _IssueTicketImage.ImagePath = imagePath;

                            uploadedImages.Add(_IssueTicketImage);
                        }

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
                    strReturn.StatusMessage = "Invalid ticket details";
                }

                if (pIssueTicket.TicketId == 0)
                {
                    strReturn.StatusCode = 0;
                    strReturn.StatusMessage = "Invalid ticket details";
                }

                if (Enum.IsDefined(typeof(TicketStatus), _IssueTicketUpdateStatus.Status) == false)
                {
                    strReturn.StatusCode = 0;
                    strReturn.StatusMessage = "Ticket status not updated. Invalid Status " + pIssueTicket.Status;
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
                    strReturn.StatusMessage = "Ticket status not updated. Current ticket status should be " + TicketStatus.Inprogress.ToString();
                }
                else if (TicketStatus.Accepted.ToString() == _IssueTicketUpdateStatus.Status
                    && pIssueTicket.Status != TicketStatus.Created.ToString())
                {
                    strReturn.StatusCode = 0;
                    strReturn.StatusMessage = "Ticket status not updated. Current ticket status should be " + TicketStatus.Created.ToString();
                }
                else if (TicketStatus.Inprogress.ToString() == _IssueTicketUpdateStatus.Status
                    && pIssueTicket.Status != TicketStatus.Accepted.ToString())
                {
                    strReturn.StatusCode = 0;
                    strReturn.StatusMessage = "Ticket status not updated. Current ticket status should be " + TicketStatus.Accepted.ToString();
                }
                else
                {
                    int i = IssueTicket.UpdateTicketStatus(_IssueTicketUpdateStatus);

                    if (i > 0)
                    {
                        strReturn.StatusCode = 1;
                        strReturn.StatusMessage = "Status updated successfully";
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

    }
}
