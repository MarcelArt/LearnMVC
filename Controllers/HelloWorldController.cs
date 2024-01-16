using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Text.Encodings.Web;
using System.Xml.Linq;

namespace LearnMVC.Controllers
{
    public class HelloWorldController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;
            return View();
        }
    }
}
