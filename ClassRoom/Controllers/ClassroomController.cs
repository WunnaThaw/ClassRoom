using Microsoft.AspNetCore.Mvc;

namespace ClassRoom.Controllers
{
    public class ClassroomController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
