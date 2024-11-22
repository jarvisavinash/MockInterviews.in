using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

namespace MockInterviews.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Credentials Validation Logic Here
            if (username == "admin" && password == "password")
            {
                // Redirect to Home on Success
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid username or password !!!";

            return View();
        }

        [HttpGet]
        public IActionResult LoginWithGoogle()
        {
            // Initiates the Google OAuth login process
            var redirectUrl = Url.Action("GoogleResponse", "Login");
            return Challenge(new AuthenticationProperties { RedirectUri = redirectUrl }, GoogleDefaults.AuthenticationScheme);
        }

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

            // Use the email and name to register/login the user
            // Example: Check if user exists in the database, if not, create a new user

            // For now, redirect to Home
            return RedirectToAction("Index", "Home");
        }
    }
}
