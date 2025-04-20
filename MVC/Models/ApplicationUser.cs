using Microsoft.AspNetCore.Identity;

namespace MVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
