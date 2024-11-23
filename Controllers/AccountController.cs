using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MockInterviews.Data;
using MockInterviews.Models;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MockInterviews.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Registration Page
        public IActionResult Register()
        {
            return View();
        }

        // POST: Register User
        [HttpPost]
        public IActionResult Register(User model)
        {
            if (!ModelState.IsValid)
            {
                // Check if email already exists
                if (_context.Users.Any(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("", "Email already registered.");
                    return View(model);
                }

                // Hash the password using PasswordHasher
                var passwordHasher = new PasswordHasher<User>();
                string passwordHash = passwordHasher.HashPassword(model, model.Password);

                // Create a new user
                var user = new User
                {
                    Email = model.Email,
                    Role = model.Role,
                    Password = passwordHash,
                    PasswordHash = passwordHash // Ensure PasswordHash is always populated
                };

                try
                {
                    _context.Users.Add(user);
                    _context.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine($"Error saving user: {ex.Message}");
                    Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                    ModelState.AddModelError("", "An error occurred while saving the user.");
                    return View(model);
                }

                // Redirect to login page after registration
                return RedirectToAction("Login");
            }

            return View(model);
        }

        // Helper: Hash the password
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        // GET: Login Page
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login User
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Username and password cannot be empty.";
                return View();
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == username);

            if (user != null)
            {
                // Verify the password using PasswordHasher
                var passwordHasher = new PasswordHasher<User>();
                var verificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

                if (verificationResult == PasswordVerificationResult.Success)
                {
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Dashboard", "Account");
                }
            }

            ViewBag.Error = "Invalid username or password.";
            return View();
        }


        // GET: Google Login Initiate
        [HttpGet]
        public IActionResult LoginWithGoogle()
        {
            // Initiates the Google OAuth login process
            var redirectUrl = Url.Action("GoogleResponse", "Account");
            return Challenge(new AuthenticationProperties { RedirectUri = redirectUrl }, GoogleDefaults.AuthenticationScheme);
        }

        // GET: Google Response
        [HttpGet]
        public async Task<IActionResult> GoogleResponse()
        {
            // Handle the response from Google after authentication
            var authenticateResult = await HttpContext.AuthenticateAsync();

            if (!authenticateResult.Succeeded)
            {
                ViewBag.Error = "Google authentication failed. Please try again.";
                return RedirectToAction("Login");
            }

            var claims = authenticateResult.Principal.Identities
                .FirstOrDefault()?.Claims.Select(claim => new
                {
                    claim.Type,
                    claim.Value
                });

            var email = claims.FirstOrDefault(x => x.Type.Contains("email"))?.Value;
            var name = claims.FirstOrDefault(x => x.Type.Contains("name"))?.Value;

            // Check if the user already exists, if not, create a new user
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                user = new User
                {
                    Email = email,
                    PasswordHash = "GoogleUser", // You can set a default password or use a placeholder
                    Role = "Candidate" // Default role can be set here
                };

                _context.Users.Add(user);
                _context.SaveChanges();
            }

            // Redirect to Home after successful Google login
            return RedirectToAction("Index", "Home");
        }

        // GET: Dashboard
        public IActionResult Dashboard()
        {
            var userId = 1; // This should be retrieved from session or authentication token
            var userRequests = _context.InterviewRequests
                .Where(r => r.Id == userId)
                .ToList();

            return View(userRequests);
        }
    }
}
