﻿
namespace CafeMenuMvc.Entity.Dtos
{
    public class UserLoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string SecretKey { get; set; }
    }
}