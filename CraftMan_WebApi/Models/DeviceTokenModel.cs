namespace CraftMan_WebApi.Models
{
    public class DeviceTokenModel
    {
        public int pCompId { get; set; }
        public string Token { get; set; }  
        public string Platform { get; set; } // "android" or "ios"
    }
}
