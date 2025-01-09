using System.ComponentModel.DataAnnotations;  // Required for validation annotations

namespace Scholarly.Models  // Replace 'YourAppName' with your actual project namespace
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [Display(Name = "Username")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]  // Hides password as typed
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;
    }
}