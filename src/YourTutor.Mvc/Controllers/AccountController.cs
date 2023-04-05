using MediatR;
using Microsoft.AspNetCore.Mvc;
using YourTutor.Application.Commands.SignIn;
using YourTutor.Application.Commands.SignOut;
using YourTutor.Application.Commands.SignUp;

namespace YourTutor.Mvc.Controllers
{
    [Route("[controller]")]
    public sealed class AccountController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
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

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(Login command)
        {          
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(command);

                if (response.Errors.Count > 0)
                {
                    ViewBag.ErrorMessages = response.Errors;
                    return View();
                }

                var returnUrl = GetReturnUrl();
                if(!string.IsNullOrEmpty(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessages = GetErrors();
                return View();
            }
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout(SignOut command)
        {
            await _mediator.Send(command);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
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

        private string GetReturnUrl()
        {
            var referer = _httpContextAccessor.HttpContext.Request.Headers.Referer;
            if(referer.Count > 0 && referer.ToString().ToLower().Contains("returnurl"))
            {
                var result = referer.ToString().ToLower();
                result = result.Split(new string[] { "returnurl=" }, StringSplitOptions.None)[1];
                return Uri.UnescapeDataString(result);
            }
            return null;
        }
    }
}
