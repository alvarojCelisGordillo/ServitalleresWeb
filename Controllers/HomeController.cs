using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Servitalleres.Models;
using ServitalleresWeb.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using ServitalleresWeb;
using ServitalleresWeb.Repository.IRepository;

namespace Servitalleres.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVehicleBrandRepository _vehicleBrandRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IHttpContextAccessor _httpAcc;


        public HomeController(ILogger<HomeController> logger, IVehicleBrandRepository vehicleBrandRepository, IClientRepository clientRepository, IAccountRepository accountRepository, IHttpContextAccessor httpAcc)
        {
            _logger = logger;
            _vehicleBrandRepository = vehicleBrandRepository;
            _clientRepository = clientRepository;
            _accountRepository = accountRepository;
            _httpAcc = httpAcc;
        }

        public IActionResult Index()
        {
            if (_httpAcc.HttpContext.Session.GetString("JWToken") != null &&
                _httpAcc.HttpContext.Session.GetString("JWToken").Length > 0)
            {
                ViewBag.Name = HttpContext.User.Identity.Name;
                return View();
            }
           
            return RedirectToAction("Login");
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

        [HttpGet]
        public IActionResult Login()
        {
            User obj = new User();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User obj)
        {
            User objUser = await _accountRepository.LoginAsync(SD.AccountAPIPath + "authenticate/", obj);
            if (objUser.Token == null)
            {
                return View();
            }

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, objUser.Username));
            identity.AddClaim(new Claim(ClaimTypes.Role, objUser.Role));
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            HttpContext.Session.SetString("JWToken", objUser.Token);
            return RedirectToAction("Index", objUser);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User obj)
        {
            bool result = await _accountRepository.RegisterAsync(SD.AccountAPIPath + "register/", obj);
            if (result == false)
            {
                return View();
            }

            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString("JWToken", "");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
