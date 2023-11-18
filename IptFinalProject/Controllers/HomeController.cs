using IptFinalProject.Areas.Identity.Data;
using IptFinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;

namespace IptFinalProject.Controllers
{
    [Authorize]
    //[Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IptFinalProjectUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
       

        public HomeController(ILogger<HomeController> logger, UserManager<IptFinalProjectUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            this._userManager = userManager;
            _webHostEnvironment = webHostEnvironment;

            //ViewData["UserID"] = _userManager.GetUserId(this.User);
        }
        
        public IActionResult Index()
        {
            //var User = _userManager.GetUserId(this.User);
            //Session.SetStrin["UserID"] = User;
            ViewData["UserID"] = _userManager.GetUserId(this.User);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}