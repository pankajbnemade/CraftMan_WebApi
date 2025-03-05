using CraftMan_WebApi.Models;
using CraftMan_WebApi.ExtendedModels;
using Microsoft.AspNetCore.Mvc;

using System.Data;
using System;
using System.Collections;
using static System.Net.Mime.MediaTypeNames;

namespace CraftMan_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueTicketController : Controller
    {
        [HttpPost]
        [Route("IssueTicket")]
        public Response IssueTicket(IssueTicket _IssueTicket)
        {
            return IssueTicketExtended.IssueNewTicket(_IssueTicket);
        }

        [HttpGet]
        [Route("GetTickets")]
        public ArrayList GetTicketsByUser(string Username)
        {
            return IssueTicketExtended.GetTicketdetailsByUser(Username);
        }

        [HttpGet]
        [Route("GetTicket")]
        public IssueTicket GetTicketByUser(int TicketId)
        {
            return IssueTicketExtended.GetTicketdetailByTicketId(TicketId);
        }

        [HttpGet]
        [Route("GetTicketsForCompany")]
        public ArrayList GetTicketsForCompany(int CompanyId, int? CountyId, int? MunicipalityId)
        {
            return IssueTicketExtended.GetTicketsForCompany(CompanyId, CountyId, MunicipalityId);
        }

        [HttpPost]
        [Route("UpdateTicketReview")]
        public Response UpdateTicketReview(IssueTicketReview _IssueTicketReview)
        {
            return IssueTicketExtended.UpdateTicketReview(_IssueTicketReview);
        }

        [HttpPost]
        [Route("UpdateCompanyComment")]
        public Response UpdateCompanyComment(IssueTicketCompanyComment _IssueTicketCompanyComment)
        {
            return IssueTicketExtended.UpdateCompanyComment(_IssueTicketCompanyComment);
        }

        [HttpPost]
        [Route("UpdateTicketStatus")]
        public Response UpdateTicketStatus(IssueTicketUpdateStatus _IssueTicketUpdateStatus)
        {
            return IssueTicketExtended.UpdateTicketStatus(_IssueTicketUpdateStatus);
        }

    }
}
