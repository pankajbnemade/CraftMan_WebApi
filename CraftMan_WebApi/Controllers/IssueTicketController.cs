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

            response.StatusCode = 47;

            List<string> tokenList = IssueTicketExtended.GetCompanyDeviceTokenList(response.StatusCode);

            if (tokenList.Any())
            {
                await _notificationService.SendNotificationAsync(tokenList, "New Job Card", "A new job card was created.");
            }

            return response;
        }

        [HttpGet]
        [Route("GetTicketsByUser")]
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

    }
}
