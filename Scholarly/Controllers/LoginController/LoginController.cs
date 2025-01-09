using Microsoft.AspNetCore.Mvc;
using Scholarly.Models;  // Adjust with your actual namespace for Models

namespace Scholarly.Controllers.LoginController
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // Basic authentication logic (replace with actual logic as needed)
                if (IsValidUser(model.Username, model.Password))
                {
                    // Redirect to dashboard or homepage
                    return RedirectToAction("Index", "Home");  // Redirect to the main page after login
                }
                else
                {
                    // Add error message if invalid login
                    ModelState.AddModelError("", "Invalid login attempt.");
                }
            }

            // Return the same view if validation fails
            return View(model);
        }

        // This is just a placeholder method for authentication logic.
        // Replace this with real logic such as checking against a database
        private bool IsValidUser(string username, string password)
        {
            // Example hardcoded validation, replace with actual DB checks
            return username == "admin" && password == "password";  // Example only!
        }
    }
}