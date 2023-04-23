using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using simpleadsauth.Web.Models;
using SimpleAdsAuth.Data;
using System.Collections.Generic;
using System.Diagnostics;

namespace simpleadsauth.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=SimpleAdsAuth;Integrated Security=true;";
        public IActionResult Index()
        {
            string currentUserEmail = User.Identity.Name;
            var manager = new Manager(_connectionString);
            var vm = new HomeViewModel
            {
                Ads = manager.GetAds(),
               
            };
            if(currentUserEmail!=null)
            {
                vm.CurrentUser = manager.GetByEmail(currentUserEmail);
            }
            return View(vm);
        }
        [Authorize]
        public IActionResult NewAd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Newad(Ad ad)
        {
            
            var currentUserEmail = User.Identity.Name;
            var manager = new Manager(_connectionString);
            var user = manager.GetByEmail(currentUserEmail);
            manager.NewAd(ad,user.Id);
            return Redirect("/home/Index");

        }
        [HttpPost]
        public IActionResult DeleteAd(int id)
        {
            var manager = new Manager(_connectionString);
            manager.Delete(id);
            return Redirect("/Home/Index");
        }
        [Authorize]
        public IActionResult MyAccount(int userid)

        {
            
            var manager = new Manager(_connectionString);
            string currentUserEmail = User.Identity.Name;
            var vm = new HomeViewModel();
            var user = manager.GetByEmail(currentUserEmail);
            vm.Ads = manager.GetMyAds(user.Id);
            return View(vm);
            
        }


    }
}