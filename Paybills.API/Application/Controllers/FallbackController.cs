using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace Paybills.API.Controllers
{
    public class FallbackController : Controller
    {
        public ActionResult Index()
        {
            return PhysicalFile(
                Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "Presentation"), "wwwroot", "index.html"),
                "text/HTML");
        }
    }
}