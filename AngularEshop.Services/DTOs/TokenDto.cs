using System;

namespace AngularEshop.Services.DTOs
{
    public class TokenDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}