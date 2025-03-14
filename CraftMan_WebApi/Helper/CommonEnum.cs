using CraftMan_WebApi.DataAccessLayer;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.Data.SqlClient;
using System.Collections;

namespace CraftMan_WebApi.Helper
{
    public enum TicketStatus
    {
        Created,       // 0
        Accepted, // 1
        Completed,     // 2
        Inprogress   // 3
    }
}