using Microsoft.AspNetCore.Mvc;

namespace TicketAssignerAPI.Controllers
{
    public class PredictionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
