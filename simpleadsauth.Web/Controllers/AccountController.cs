using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SimpleAdsAuth.Data;
using System.Security.Claims;

namespace simpleadsauth.Web.Controllers
{
    public class AccountController : Controller
    {
        private string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=SimpleAdsAuth;Integrated Security=true;";
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Signup(User user, string password)
        {
            var manager = new Manager(_connectionString);
            manager.AddUser(user, password);
            return RedirectToAction("Login");
        }
        public  IActionResult Login()
        {
            if (TempData["message"] != null)
            {
                ViewBag.Message = TempData["message"];
            }
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var manager = new Manager(_connectionString);
            var user = manager.Login(email, password);
            if(user == null)
            {
                TempData["message"] = "Invalid Login";
                return Redirect("/account/login");
            }
           
            var claims = new List<Claim>
            {
                new Claim("user", email) 
            };

            HttpContext.SignInAsync(new ClaimsPrincipal(
                new ClaimsIdentity(claims, "Cookies", "user", "role")))
                .Wait();

            return Redirect("/home/newad");

            
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync().Wait();
            return Redirect("/home/index");
        }
    }
}
