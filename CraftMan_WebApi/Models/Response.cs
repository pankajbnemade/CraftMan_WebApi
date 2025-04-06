namespace CraftMan_WebApi.Models
{
    public class Response
    {
        public  int StatusCode { get; set; }
        public String StatusMessage { get; set; }
        public String JWTToken { get; set; }
    }
}
