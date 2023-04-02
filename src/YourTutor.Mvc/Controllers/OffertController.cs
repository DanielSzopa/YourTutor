using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace YourTutor.Mvc.Controllers
{
    [Route("[controller]")]
    public class OffertController : Controller
    {
        [Authorize]
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }
    }
}
