namespace CraftMan_WebApi.Models
{
    public class LoginComp
    {
        public string Password { get; set; }
        public bool Active { get; set; }
        public string EmailId { get; set; }
        //public string? FcmToken { get; set; } // New Field for notification

    }
}
