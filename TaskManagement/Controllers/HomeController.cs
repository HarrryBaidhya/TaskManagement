using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TaskManagement.Interface;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITaskManagment TRepository;
        public HomeController(ILogger<HomeController> logger,ITaskManagment managment)
        {
            _logger = logger;
            this.TRepository = managment;
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
        [HttpGet]
        public IActionResult Login()

        {
            return View();
        }

        public IActionResult Login(UserRegModel mod)

        {

            var cheeckuser = TRepository.CheckUser(mod);
            UserRegModel regModel = new UserRegModel();
            Sessionmodel sessionmodel = new Sessionmodel();
            HttpContext.Session.SetString(sessionmodel.sessionFirstname, "pranaya@dotnettutotials.net");
            HttpContext.Session.SetString(sessionmodel.SessionCountry, "pranaya@dotnettutotials.net");
            HttpContext.Session.SetString(sessionmodel.SessionCountry, "pranaya@dotnettutotials.net");

            return View();
        }

        [HttpGet]
        public IActionResult Register()

        {
            return View();
        }


        [HttpPost]
        public IActionResult Register(UserRegModel mod)

        {
            try
            {
                var newFrnd = TRepository.CreateRegister(mod);
                return RedirectToAction("Login");
            }
            catch
            {
                
                return View();
            }

          
        }

        public IActionResult Dashboard()
        {
            return View();
        }

    }
}
