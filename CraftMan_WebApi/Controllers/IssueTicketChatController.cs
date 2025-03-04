using CraftMan_WebApi.Models;
using CraftMan_WebApi.ExtendedModels;
using Microsoft.AspNetCore.Mvc;

using System.Data;
using System;
using System.Collections;

namespace CraftMan_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueTicketChatController : Controller
    {
        [HttpPost]
        [Route("NewTicketChat")]
        public Response InsertIssueTicketChat(IssueTicketChat _IssueTicketChat)
        {
            return IssueTicketChatExtended.InsertIssueTicketChat(_IssueTicketChat);
        }

        [HttpGet]
        [Route("GetChatMessages")]
        public ArrayList GetChatMessagesByTicket(int TicketId)
        {
            return IssueTicketChatExtended.GetChatMessagesByTicketId(TicketId);
        }

    }
}
