using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YourTutor.Application.Abstractions;
using YourTutor.Application.Commands.CreateOffer;
using YourTutor.Application.Commands.DeleteOffer;
using YourTutor.Application.Dtos;
using YourTutor.Application.Dtos.Pagination;
using YourTutor.Application.Helpers;
using YourTutor.Application.Queries.GetOfferDetails;
using YourTutor.Application.Queries.GetSmallOffers;
using YourTutor.Application.ViewModels;
using YourTutor.Infrastructure.Constans;

namespace YourTutor.Mvc.Controllers
{
    [Route("[controller]")]
    public class OfferController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OfferController> _logger;
        private readonly IHttpContextService _httpContextService;
        private readonly IAuthorizationService _authorizationService;

        public OfferController(IMediator mediator, ILogger<OfferController> logger, IHttpContextService httpContextService,
            IAuthorizationService authorizationService)
        {
            _mediator = mediator;
            _logger = logger;
            _httpContextService = httpContextService;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] PaginationDto paginationDto, [FromQuery] OffersFilterDto offersDto)
        {
            var query = new GetSmallOffers(paginationDto, offersDto);
            var response = await _mediator.Send(query);
            return View(response);
        }


        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var details = await _mediator.Send(new GetOfferDetails(id));        

            if (details is null)
                return RedirectToAction(nameof(Index));

            var result = _httpContextService.GetUserIdFromClaims()  == details.TutorId 
                ? true 
                : false;

            ViewBag.IsHisOffer = result;

            return View(details);
        }

        [Authorize]
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateOfferVm vm)
        {
            var userId = _httpContextService.GetUserIdFromClaims();
            if (userId == Guid.Empty)
            {
                _logger.LogError(AppLogEvent.IndicateUser, "Problem with indicating user");
                return RedirectToAction(nameof(HomeController.Error), "Home");
            }

            var offerId = await _mediator.Send(new CreateOffer(vm, userId));

            return RedirectToAction(nameof(Details), new { id = offerId });
        }


        [Authorize]
        [Route("delete/{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpContextService.GetUser(),
                new DeleteOffer(id), CustomAuthorizationPolicy.DeleteOffer);

            if (!authorizationResult.Succeeded)
                return new ForbidResult();

            await _mediator.Send(new DeleteOffer(id));

            return RedirectToAction(nameof(Index));
        }
    }
}
