using Microsoft.AspNetCore.Mvc;

namespace NvtLession1MVC.Controllers
{
    public class NvtDemoController : Controller
    {
        public IActionResult NvtIndex()
        {
            return View();
        }
    }
}

