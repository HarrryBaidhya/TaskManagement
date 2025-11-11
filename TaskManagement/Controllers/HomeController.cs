using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

      //  public async Task Login(UserRegModel mod)

          public async Task<IActionResult> Login(UserRegModel mod)
        {
            var data = await TRepository.GetUser();


           // var cheeckuser = TRepository.CheckUser(mod);
            UserRegModel regModel = new UserRegModel();
            var data2 = new UserRegModel() {
                UserName=mod.UserName,
                Password=mod.Password,
                UserRole=mod.UserRole,  
            };

           var user = data.FirstOrDefault(u =>
    string.Equals(u.UserName, mod.UserName, StringComparison.OrdinalIgnoreCase) &&
    u.Password == mod.Password);
            if (User != null)
            {
                bool isPasswordValid = false;

                // If password is stored as plain text
                if (isPasswordValid = (user.Password == mod.Password)) {


                    var sessionModel = new Sessionmodel
                    {
                        sessionFirstname = user.Firstname,
                        sessionRole=user.UserRole,
         
                    };

                    // Set session data
                    HttpContext.Session.SetString("SessionFirstname", sessionModel.sessionFirstname);
                    ///   HttpContext.Session.SetString("SessionEmail", sessionmodel.SessionEmail);
                    HttpContext.Session.SetString("SessionRole", sessionModel.sessionRole ?? "");
                    return RedirectToAction("Index", "Home");
                    //return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password");
                    return View();
                }

           
            }

            return View();
          }

        [HttpGet]
        public IActionResult Register()
        {
            var roles = new List<SelectListItem>
    {
        new SelectListItem { Value = "Admin", Text = "Admin" },
        new SelectListItem { Value = "User", Text = "User" },
    };

            ViewBag.Roles = roles;

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
