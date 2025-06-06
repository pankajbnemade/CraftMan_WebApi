﻿using CraftMan_WebApi.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace CraftMan_WebApi.Models
{
    public class IssueTicketImage
    {
        public int ImageId { get; set; }
        public int TicketId { get; set; }
        public string? ImageName { get; set; }
        public string? ImagePath { get; set; }
        public string? ImageContentType { get; set; }
    }
}
