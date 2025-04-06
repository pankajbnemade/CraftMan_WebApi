using CraftMan_WebApi.Models;
using CraftMan_WebApi.ExtendedModels;
using Microsoft.AspNetCore.Mvc;

using System.Data;
using System;
using System.Collections;
using Microsoft.AspNetCore.Authorization;

namespace CraftMan_WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class IssueTicketChatController : Controller
    {
        [HttpPost]
        [Route("NewTicketChat")]
        public Response InsertIssueTicketChat([FromBody] IssueTicketChat _IssueTicketChat)
        {
            return IssueTicketChatExtended.InsertIssueTicketChat(_IssueTicketChat);
        }

        [HttpGet]
        [Route("GetChatMessages")]
        public ArrayList GetChatMessagesByTicket(int TicketId, int CompanyId)
        {
            return IssueTicketChatExtended.GetChatMessagesByTicketId(TicketId, CompanyId);
        }

        [HttpGet]
        [Route("GetChatList")]
        public ArrayList GetChatList(int TicketId)
        {
            return IssueTicketChatExtended.GetChatListByTicketId(TicketId);
        }

    }
}
