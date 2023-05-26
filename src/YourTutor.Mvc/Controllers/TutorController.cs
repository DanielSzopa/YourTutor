using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YourTutor.Application.Abstractions;
using YourTutor.Application.Commands.EditTutor;
using YourTutor.Application.Helpers;
using YourTutor.Application.Queries.GetTutorByUserId;
using YourTutor.Application.Queries.GetTutorEditDetails;
using YourTutor.Application.ViewModels;
using YourTutor.Infrastructure.Authorization;
using YourTutor.Infrastructure.Constans;

namespace YourTutor.Mvc.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public sealed class TutorController : Controller
    {
        private readonly ISender _sender;
        private readonly IHttpContextService _httpContextService;
        private readonly ILogger<TutorController> _logger;
        private readonly IAuthorizationService _authorizationService;
        private readonly string _errorEndpoint = nameof(HomeController.Error).ToLower();
        private readonly string _homeController = nameof(HomeController).Replace("Controller", "").ToLower();
        private readonly string _offerController = nameof(OfferController).Replace("Controller", "").ToLower();

        public TutorController(ISender sender, IHttpContextService httpContextService
            , ILogger<TutorController> logger, IAuthorizationService authorizationService)
        {
            _sender = sender;
            _httpContextService = httpContextService;
            _logger = logger;
            _authorizationService = authorizationService;
        }

        [HttpGet]      
        public async Task<IActionResult> MyAccount()
        {
            var userId = _httpContextService.GetUserIdFromClaims();
            if (userId == Guid.Empty)
            {
                _logger.LogError(AppLogEvent.IndicateUser, "Problem with indicating user");
                return RedirectToAction(_errorEndpoint, _homeController);
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

            if(details is null)
                return RedirectToAction("", _offerController);

            var result = _httpContextService.GetUserIdFromClaims() == id
                ? true
                : false;

            ViewBag.IsHisAccount = result;

            return View("Tutor", details);
        }

        [HttpGet]
        [Route("edit")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var authorizationResult = await _authorizationService
                .AuthorizeAsync(_httpContextService.GetUser(), new CanEditTutorRequest(id), CustomAuthorizationPolicy.EditTutor);

            if (!authorizationResult.Succeeded)
                return new ForbidResult();

            var details = await _sender.Send(new GetTutorEditDetails(id));
            return View(details);
        }

        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> Edit(EditTutorVm vm)
        {
            var userId = _httpContextService.GetUserIdFromClaims();
            if (userId == Guid.Empty)
            {
                _logger.LogError(AppLogEvent.IndicateUser, "Problem with indicating user");
                return RedirectToAction(_errorEndpoint, _homeController);
            }

            await _sender.Send(new EditTutor(vm,userId));
            return RedirectToAction(nameof(MyAccount));
        }
    }
}
