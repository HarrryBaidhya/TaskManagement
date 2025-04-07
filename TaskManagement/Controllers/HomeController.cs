using AutoMapper;
using Blood.Business.LoginUser;
using Blood.domain.Models.Login;
using BloodManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TaskManagement.Interface;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILoginBusiness loginBusiness;
        private readonly IMapper mapper;
        //private readonly ITaskManagment _taskManagment;

        public HomeController(ILogger<HomeController> logger, ILoginBusiness bus, IMapper mapper/*, ITaskManagment TRepository*/)
        {
            loginBusiness = bus;
            _logger = logger;
            this.mapper = mapper;
            //_taskManagment = TRepository;
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
        [HttpPost]
        public IActionResult Login(AdminUser AU)

        {
            LoginCommon login = mapper.Map<LoginCommon>(AU);
            //login.Password = ApplicationUtilities.Encryption(AU.Password);
            var user = loginBusiness.LoginUser(login);
            LoginCommon dm = mapper.Map<LoginCommon>(user);
            if (dm.Message == "success") { return RedirectToAction("Dashboard"); }
            return View();
        }

        //  public async Task Login(UserRegModel mod)

        public async Task<IActionResult> Login(UserRegModel mod)
        {
            //var data = await _taskManagment.Get();

            // var cheeckuser = TRepository.CheckUser(mod);
            UserRegModel regModel = new UserRegModel();
            var data2 = new UserRegModel()
            {
                UserName = mod.UserName,
                Password = mod.Password,
            };

            //var user = data.FirstOrDefault(u =>
            //string.Equals(u.Title, mod.UserName, StringComparison.OrdinalIgnoreCase) &&
            //u.Status == mod.Password);
            if (User != null)
            {
                bool isPasswordValid = false;

                // If password is stored as plain text
                //if (isPasswordValid = (user.Status == mod.Password))
                //{
                //    var sessionModel = new Sessionmodel
                //    {
                //        sessionFirstname = user.Title
                //    };

                //    // Set session data
                //    HttpContext.Session.SetString("SessionFirstname", sessionModel.sessionFirstname);
                //    ///   HttpContext.Session.SetString("SessionEmail", sessionmodel.SessionEmail);
                //    return RedirectToAction("Index");
                //}
                //else
                //{
                //    return View();
                //}
            }
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        //[HttpPost]
        //public IActionResult Register(UserRegModel mod)

        //{
        //    try
        //    {
        //        var newFrnd = _taskManagment.UpdateTask(mod);
        //        return RedirectToAction("Login");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

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
