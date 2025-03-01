using CraftMan_WebApi.Models;
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
        public static Response IssueNewTicket(IssueTicket _IssueTicket)
        {
            Response strReturn = new Response();
            try
            { 
                int i = IssueTicket.InsertTicket(_IssueTicket);
                if (i > 0)
                    {
                        strReturn.StatusCode = 1;
                        strReturn.StatusMessage = "Issue Registered Successfully";
                    }
                    else
                    { strReturn.StatusMessage = "Issue not registered.This may be because of user registering same issue again"; }                
            }
            catch (Exception ex) { throw; }
            return strReturn;
        }
    }
}
