using AutoMapper;
using Blood.Business.LoginUser;
using Blood.domain.Models.Login;
using BloodManagement.Models;
using DocumentFormat.OpenXml.Math;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TaskManagement.Models;
using template.application.Library;

namespace TaskManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILoginBusiness loginBusiness;
        private readonly IMapper mapper;
        public HomeController(ILogger<HomeController> logger,ILoginBusiness bus, IMapper mapper)
        {
            loginBusiness = bus;
            _logger = logger;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
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
        public IActionResult Login()

        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(AdminUser AU)

        {

   
            LoginCommon login = mapper.Map<LoginCommon>(AU);
            login.Password = ApplicationUtilities.Encryption(AU.Password);
            var user = loginBusiness.LoginUser(login);
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult SiginUser()
        {
            return View();
        }


        public IActionResult RegisterUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RegisterUser(BSRegisterUser BSR)
        {
            return View();
        }



        public IActionResult SignOutUser()
        {
            return View();
        }

    }
}
