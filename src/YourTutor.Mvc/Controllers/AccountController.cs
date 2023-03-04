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
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(command);

                if (response.Errors.Count > 0)
                {
                    ViewBag.ErrorMessages = response.Errors;
                    return View();
                }

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessages = GetErrors();
                return View();
            }
        }

        private List<string> GetErrors()
        {
            var errors = new List<string>();
            var test = ModelState?.Values
                .Where(x => x.Errors.Count > 0)
                .Select(x => x.Errors)
                .ToList();

            foreach (var error in test)
            {
                foreach (var e in error)
                {
                    errors.Add(e.ErrorMessage);
                }
            }

            return errors;
        }
    }
}
