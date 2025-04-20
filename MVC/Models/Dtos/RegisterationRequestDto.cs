using System.ComponentModel.DataAnnotations;

namespace MVC.Models.Dtos
{
    public class RegisterationRequestDto
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [PasswordValidation]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
