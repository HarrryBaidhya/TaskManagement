using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TaskManagement.Extension;

namespace TaskManagement.Controllers
{
    public class NotificationController : Controller
    {

        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationController(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> SendNotification()
        {
            return View();
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendNotification(string user, string message)
        {
            user = "Harry";
            message = "Heelo";
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", user, message);
            return Ok(new { Status = "Message Sent", User = user, Message = message });
        }
    }
}
