using Microsoft.AspNetCore.Identity;

namespace TechnuPur.Model
{
    public enum Role
    {
        Admin,
        User
    }
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Role Role { get; set; } = Role.User;
       
      

    }
}
