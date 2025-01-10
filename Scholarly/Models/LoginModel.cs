using System.ComponentModel.DataAnnotations;  // Required for validation annotations

namespace Scholarly.Models  // Replace 'YourAppName' with your actual project namespace
{
    
    public class LoginModel
    {
            public string Username { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }  // Role selected during registration
    }

}


//######################################### OLD CODE ##########################################//

//public class LoginModel
//{
//        //[Required(ErrorMessage = "Username is required.")]
        //[Display(Name = "Username")]
        //public string Username { get; set; } = string.Empty;

//[Required(ErrorMessage = "Password is required.")]
//[DataType(DataType.Password)]  // Hides password as typed
//[Display(Name = "Password")]
//public string Password { get; set; } = string.Empty;


