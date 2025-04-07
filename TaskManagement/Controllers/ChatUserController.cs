using Microsoft.AspNetCore.Mvc;

namespace BloodManagement.Controllers
{
    public class ChatUserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ChatIndex()
        {

            return View();
        }
    }
}