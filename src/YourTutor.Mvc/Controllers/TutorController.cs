using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YourTutor.Application.Abstractions;
using YourTutor.Application.Commands.EditTutor;
using YourTutor.Application.Helpers;
using YourTutor.Application.Queries.GetTutorByUserId;
using YourTutor.Application.Queries.GetTutorEditDetails;
using YourTutor.Application.ViewModels;

namespace YourTutor.Mvc.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public sealed class TutorController : Controller
    {
        private readonly ISender _sender;
        private readonly IHttpContextService _httpContextService;
        private readonly ILogger<TutorController> _logger;

        public TutorController(ISender sender, IHttpContextService httpContextService
            , ILogger<TutorController> logger)
        {
            _sender = sender;
            _httpContextService = httpContextService;
            _logger = logger;
        }

        [HttpGet]      
        public async Task<IActionResult> MyAccount()
        {
            var userId = _httpContextService.GetUserIdFromClaims();
            if (userId == Guid.Empty)
            {
                _logger.LogError(AppLogEvent.IndicateUser, "Problem with indicating user");
                return RedirectToAction(nameof(HomeController.Error), "Home");
            }

            var details = await _sender.Send(new GetTutorByUserId(userId));

            ViewBag.IsHisAccount = true;

            return View("Tutor", details);
        }
       
        [HttpGet]
        [Route("{id:Guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id)
        {
            var details = await _sender.Send(new GetTutorByUserId(id));

            var result = _httpContextService.GetUserIdFromClaims() == id
                ? true
                : false;

            ViewBag.IsHisAccount = result;

            return View("Tutor", details);
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var details = await _sender.Send(new GetTutorEditDetails(id));
            return View(details);
        }

        [HttpPost]
        [Route("Edit")]
        public async Task<IActionResult> Edit(EditTutorVm vm)
        {
            var userId = _httpContextService.GetUserIdFromClaims();
            if (userId == Guid.Empty)
            {
                _logger.LogError(AppLogEvent.IndicateUser, "Problem with indicating user");
                return RedirectToAction(nameof(HomeController.Error), "Home");
            }

            await _sender.Send(new EditTutor(vm,userId));
            return RedirectToAction(nameof(MyAccount));
        }
    }
}
