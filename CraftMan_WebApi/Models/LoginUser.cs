﻿namespace CraftMan_WebApi.Models
{
    public class LoginUser
    {
        public string Password { get; set; }
        public bool Active { get; set; }
        public string EmailId { get; set; }
    }
}
