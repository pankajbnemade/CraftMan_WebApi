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
    public class IssueTicketController : Controller
    {
       [HttpPost]
        [Route("IssueTicket")]
        public Response IssueTicket(IssueTicket _IssueTicket)
        {
            //Response rstr= new Response();
            //foreach (var item in _IssueTicket.ImageData)
            //{
            //    if (item.FileName == null || item.FileName.Length == 0)
            //    {
            //        rstr.StatusMessage = "File not selected";
            //        return rstr;
            //    }
            //    var path = Path.Combine("E:\\developement\\CraftMan_WebApi\\CraftMan_WebApi", "Images/", item.FileName);
            //    using (FileStream stream = new FileStream(path, FileMode.Create))
            //    {
            //         item.CopyToAsync(stream);
            //        stream.Close();
            //    }

            //}

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
        [Route("GetTicketsByCompany")]
        public ArrayList GetTicketsByCompany(int CompanyId, int CountyId, int MunicipalityId)
        {
            return IssueTicketExtended.GetTicketsByCompany(CompanyId, CountyId, MunicipalityId);
        }

    }
}
