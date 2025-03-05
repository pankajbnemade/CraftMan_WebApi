using CraftMan_WebApi.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Collections;
namespace CraftMan_WebApi.ExtendedModels
{
    public class IssueTicketChatExtended
    {
        public static ArrayList GetChatMessagesByTicketId(int TicketId)
        {
            try
            {
                return IssueTicketChat.GetChatMessagesByTicketId(TicketId);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                throw new ApplicationException("An error occurred.", ex);
            }
        }

        public static Response InsertIssueTicketChat(IssueTicketChat _IssueTicketChat)
        {
            Response strReturn = new Response();

            try
            {
                int i = IssueTicketChat.InsertIssueTicketChat(_IssueTicketChat);

                if (i > 0)
                {
                    strReturn.StatusCode = 1;
                    strReturn.StatusMessage = "Chat saved successfully.";
                }
                else
                {
                    strReturn.StatusMessage = "Chat not saved.";
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
