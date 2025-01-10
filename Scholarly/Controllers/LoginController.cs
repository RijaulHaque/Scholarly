//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Scholarly.DAL;
//using Scholarly.Models;  // Adjust with your actual namespace for Models

//namespace Scholarly.Controllers.LoginController
//{
//    public class LoginController : Controller
//    {
//        public DatabaseContext db;

//        public LoginController(DatabaseContext _db)
//        {
//            db = _db;
//        }
//        // GET: Login
//        public ActionResult Index()
//        {
//            return View();
//        }

//        // POST: Login
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Index(LoginModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                // Basic authentication logic (replace with actual logic as needed)
//                if (IsValidUser(model.Username, model.Password))
//                {
//                    Users user = new()
//                    {
//                        User_Id  = Guid.NewGuid(),
//                        Email = model.Username,
//                        Password = model.Password

//                    };  // Create a user object

//                    db.user.Add(user);  // Add user to the database 
//                    db.SaveChanges();

//                    // Redirect to dashboard or homepage
//                    return RedirectToAction("Index", "Home");  // Redirect to the main page after login
//                }
//                else
//                {
//                    // Add error message if invalid login
//                    ModelState.AddModelError("", "Invalid login attempt.");
//                }
//            }

//            // Return the same view if validation fails
//            return View(model);
//        }

//        // This is just a placeholder method for authentication logic.
//        // Replace this with real logic such as checking against a database
//        private bool IsValidUser(string username, string password)
//        {
//            // Example hardcoded validation, replace with actual DB checks
//            return username == "admin" && password == "password";  // Example only!
//        }


//        public IActionResult Register() { return View(); }
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Register(LoginModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                Users user = new()
//                {
//                    User_Id = Guid.NewGuid(),
//                    Email = model.Username,
//                    Password = model.Password,
//                    FirstName = "first",
//                    LastName =  "last"

//                };  // Create a user object


//                //Role student = db.role.Where(m=>m.role_name == "Student").Include(x=>x.users_obj).FirstOrDefault();
//                //if(student.users_obj != null && student.users_obj.Any())
//                //{
//                //    student.users_obj.Add(user);
//                //}
//                //else
//                //{
//                //    List<Users> users = new();
//                //    users.Add(user);
//                //    student.users_obj = users;
//                //}


//                //db.Entry(student).State = EntityState.Modified;

//                //db.user.Add(user);  // Add user to the database 
//                db.SaveChanges();

//                return RedirectToAction("Index", "Home");
//            }

//            // Return the same view if validation fails
//            return View(model);
//        }
//    }
//}



///////########################## NEWLY ADDED CODE ##################################


using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Scholarly.DAL;
using Scholarly.Models;

namespace Scholarly.Controllers
{
    public class LoginController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IPasswordHasher<Users> _passwordHasher;

        public LoginController(DatabaseContext context, IPasswordHasher<Users> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        // GET: Login
        public IActionResult Index()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // Find the user by username
                var user = await _context.user.FirstOrDefaultAsync(u => u.Username == model.Username);

                if (user != null)
                {
                    // Verify the password using the stored hash
                    var result = _passwordHasher.VerifyHashedPassword(user, user.Password, model.Password);

                    if (result == PasswordVerificationResult.Success)
                    {
                        // Successful login: Redirect based on user role
                        if (user.Roles == "Student")
                        {
                            // Redirect student to their dashboard or profile
                            return RedirectToAction("StudentDashboard", "Home");
                        }
                        else if (user.Roles == "Teacher")
                        {
                            // Redirect teacher to their dashboard or profile
                            return RedirectToAction("TeacherDashboard", "Home");
                        }
                        else
                        {
                            // Redirect to a default page or error page if the role is unknown
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }

                // If login fails, show an error message
                ModelState.AddModelError("", "Invalid login attempt.");
            }

            // Return to the same view with validation errors
            return View(model);
        }

        // Register GET method
        public IActionResult Register()
        {
            return View();
        }

        // Register POST method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if the username already exists in the database
                var existingUser = await _context.user
                    .FirstOrDefaultAsync(u => u.Username == model.Username);

                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Username already in use.");
                    return View(model);
                }

                // Hash the password before storing it
                var hashedPassword = _passwordHasher.HashPassword(null, model.Password);

                // Create a new user object based on role selection
                var user = new Users
                {
                    Username = model.Username,
                    Password = hashedPassword,
                    Roles = model.Role // Example: 'Student' or 'Teacher'
                };

                _context.user.Add(user);  // Add user to the database
                await _context.SaveChangesAsync();  // Save changes asynchronously

                // After creating user, create their Student or Teacher profile if necessary
                if (model.Role == "Student")
                {
                    var student = new Students
                    {
                        Name = model.Username,  // You may want to gather more student information here
                        Year = 1  // Example default, adjust as needed
                    };

                    _context.student.Add(student);
                    await _context.SaveChangesAsync();
                }
                else if (model.Role == "Teacher")
                {
                    var teacher = new Teachers
                    {
                        Name = model.Username  // You may want to gather more teacher information here
                    };

                    _context.Teacher.Add(teacher);
                    await _context.SaveChangesAsync();
                }

                // Redirect to login page after successful registration
                return RedirectToAction("Index", "Home");
            }

            // If model validation fails, return to the registration view
            return View(model);
        }
    }
}


