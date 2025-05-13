using CraftMan_WebApi.Models;
using CraftMan_WebApi.ExtendedModels;
using Microsoft.AspNetCore.Mvc;

using System.Data;
using System;
using System.Collections;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
using CraftMan_WebApi.Helper;

namespace CraftMan_WebApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class IssueTicketController : Controller
    {
        private readonly IConfiguration _config;
        private readonly FirebaseNotificationService _notificationService;
        public IssueTicketController(IConfiguration config)
        {
            _config = config;
            _notificationService = new FirebaseNotificationService(_config);
        }

        [HttpPost]
        [Route("IssueTicket")]
        public async Task<Response> IssueTicket([FromForm] IssueTicket _IssueTicket)
        {
            Response response = new Response();

            response = IssueTicketExtended.IssueNewTicket(_IssueTicket);

            int ticketId = response.StatusCode;

            List<string> tokenList = IssueTicketExtended.GetCompanyDeviceTokenList(ticketId);

            if (tokenList.Any())
            {
                await _notificationService.SendNotificationAsync(tokenList, "New Job Request", "A new job request was created.", ticketId);
            }

            return response;
        }

        [HttpGet]
        [Route("GetTicket")]
        public IssueTicket GetTicket(int TicketId)
        {
            return IssueTicketExtended.GetTicketdetailByTicketId(TicketId);
        }

        [HttpGet]
        [Route("GetTicketsByUser")]
        public ArrayList GetTicketsByUser([FromQuery] IssueTicketForUserFilter filter)
        {
            return IssueTicketExtended.GetTicketdetailsByUser(filter);
        }

        [HttpGet]
        [Route("GetTicketsForCompany")]
        public ArrayList GetTicketsForCompany([FromQuery] IssueTicketForCompanyFilter filter)
        {
            return IssueTicketExtended.GetTicketsForCompany(filter);
        }

        [HttpPost]
        [Route("UpdateTicketReview")]
        public Response UpdateTicketReview(IssueTicketReview _IssueTicketReview)
        {
            return IssueTicketExtended.UpdateTicketReview(_IssueTicketReview);
        }

        [HttpPost]
        [Route("UpdateCompanyComment")]
        public Response UpdateCompanyComment([FromForm] IssueTicketCompanyComment _IssueTicketCompanyComment)
        {
            return IssueTicketExtended.UpdateCompanyComment(_IssueTicketCompanyComment);
        }

        [HttpPost]
        [Route("UpdateTicketStatus")]
        public Response UpdateTicketStatus(IssueTicketUpdateStatus _IssueTicketUpdateStatus)
        {
            return IssueTicketExtended.UpdateTicketStatus(_IssueTicketUpdateStatus);
        }




        //Temp for testing

        [HttpGet]
        [Route("GetCompanyListForTicket")]
        public ArrayList GetCompanyListForTicket(int ticketId)
        {
            List<string> tokenList = IssueTicketExtended.GetCompanyDeviceTokenList(ticketId);

            if (tokenList == null || tokenList.Count == 0)
            {
                return new ArrayList(); 
            }

            return IssueTicketExtended.GetCompanyListForTicket(tokenList);
        }

    }
}
