//#################### ADDED IN 18th JAN ############################

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scholarly.DAL;
using Scholarly.Models;
using System.Security.Claims;

public class LoginController : Controller
{
    private readonly DatabaseContext _context;
    private readonly IPasswordHasher<user> _passwordHasher;

    public LoginController(DatabaseContext context, IPasswordHasher<user> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(); // Return the login page
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Find user by username
            var user = await _context.user.FirstOrDefaultAsync(u => u.Username == model.Username);

            if (user != null)
            {
                // Verify password
                var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);
                if (result == PasswordVerificationResult.Success)
                {
                    // Create claims for user identity
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Role, user.Roles)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    // Sign in the user
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        claimsPrincipal,
                        new AuthenticationProperties
                        {
                            IsPersistent = true, // Keep the user signed in across requests
                            ExpiresUtc = DateTime.UtcNow.AddMinutes(60) // Session timeout
                        });

                    // Redirect based on role
                    if (user.Roles == "Student")
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else if (user.Roles == "Teacher")
                    {
                        return RedirectToAction("Index", "TeacherDashboard");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            // Add error if login failed
            ModelState.AddModelError("", "Invalid username or password.");
        }

        return View(model); // Return login view with errors
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Login");
    }
}






//#################################### 17th JAN ##########################################

//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Identity;
//using Scholarly.Models;
//using Scholarly.DAL;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Authentication;
//using System.Security.Claims;

//namespace Scholarly.Controllers
//{
//    public class LoginController : Controller
//    {
//        private readonly DatabaseContext _context; // Database context for querying data
//        private readonly IPasswordHasher<user> _passwordHasher; // Password hasher for password verification

//        // Constructor to inject dependencies
//        public LoginController(DatabaseContext context, IPasswordHasher<user> passwordHasher)
//        {
//            _context = context;
//            _passwordHasher = passwordHasher;
//        }

//        // GET: Login
//        public IActionResult Index()
//        {
//            return View();
//        }

//        // ###### ONLY FOR TESTING PURPOSES ######
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Index(LoginViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var user = await _context.user.FirstOrDefaultAsync(u => u.Username == model.Username);

//                if (user != null)
//                {
//                    var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);

//                    if (result == PasswordVerificationResult.Success)
//                    {
//                        var claims = new List<Claim>
//                        {
//                        new Claim(ClaimTypes.Name, user.Username),
//                        new Claim(ClaimTypes.Role, user.Roles)
//                        };

//                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
//                        var authProperties = new AuthenticationProperties
//                        {
//                            IsPersistent = true
//                        };

//                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

//                        if (user.Roles == "Student")
//                        {
//                            //return RedirectToAction("Index", "Dashboard");
//                            return View("~/Views/Dashboard/Index.cshtml");
//                        }
//                        else if (user.Roles == "Teacher")
//                        {
//                            return RedirectToAction("TeacherDashboard", "Home");
//                        }
//                        else
//                        {
//                            return RedirectToAction("Index", "Home");
//                        }
//                    }
//                }

//                ModelState.AddModelError("", "Invalid login attempt.");
//            }

//            return View();
//        }


//    }
//}

