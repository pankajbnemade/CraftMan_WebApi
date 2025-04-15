using CraftMan_WebApi.ExtendedModels;
using CraftMan_WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace CraftMan_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeviceTokenController : ControllerBase
    {
        [HttpPost]
        [Route("SaveDeviceToken")]
        public Response SaveDeviceToken([FromBody] DeviceTokenModel _DeviceTokenModel)
        {
            return DeviceTokenExtended.SaveNewDeviceToken(_DeviceTokenModel);
        }

    }

}



//private readonly IConfiguration _config;

//public DeviceTokenController(IConfiguration config)
//{
//    _config = config;
//}

//[HttpPost("save")]
//public IActionResult SaveDeviceToken([FromBody] DeviceToken request)
//{
//    using SqlConnection conn = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
//    conn.Open();

//    string checkQuery = "SELECT COUNT(*) FROM CompanyUserDevices WHERE CompanyUserId = @CompanyUserId AND DeviceToken = @DeviceToken";
//    using SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
//    checkCmd.Parameters.AddWithValue("@CompanyUserId", request.CompanyUserId);
//    checkCmd.Parameters.AddWithValue("@DeviceToken", request.Token);

//    int exists = (int)checkCmd.ExecuteScalar();
//    if (exists == 0)
//    {
//        string insertQuery = @"
//        INSERT INTO CompanyUserDevices (CompanyUserId, DeviceToken, Platform, RegisteredOn)
//        VALUES (@CompanyUserId, @DeviceToken, @Platform, @RegisteredOn)";
//        using SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
//        insertCmd.Parameters.AddWithValue("@CompanyUserId", request.CompanyUserId);
//        insertCmd.Parameters.AddWithValue("@DeviceToken", request.Token);
//        insertCmd.Parameters.AddWithValue("@Platform", request.Platform);
//        insertCmd.Parameters.AddWithValue("@RegisteredOn", DateTime.UtcNow);
//        insertCmd.ExecuteNonQuery();
//    }

//    return Ok();
//}