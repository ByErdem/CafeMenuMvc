using System;

namespace CafeMenuMvc.Entity.Concrete
{
    public class UserRegisterDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string SaltPassword { get; set; } = Guid.NewGuid().ToString();
    }
}