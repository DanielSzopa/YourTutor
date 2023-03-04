using MediatR;
using Microsoft.AspNetCore.Mvc;
using YourTutor.Application.Commands;

namespace YourTutor.Mvc.Controllers
{
    [Route("[controller]")]
    public sealed class AccountController : Controller
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [Route("register")]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(Register command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _mediator.Send(command);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.IsInvalidRegistration = true;
                    return View();
                }
            }
            catch(Exception ex)
            {
                ViewBag.IsInvalidRegistration = true;
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
                      
        }
    }
}
