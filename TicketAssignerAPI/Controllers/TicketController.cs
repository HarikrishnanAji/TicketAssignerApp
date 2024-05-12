using Microsoft.AspNetCore.Mvc;

namespace TicketAssignerAPI.Controllers
{
    public class TicketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
