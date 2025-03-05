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
        Active,       // 0
        InProgress, // 1
        Closed,     // 2
        Resolved,   // 3
        Reopened    // 4
    }
}