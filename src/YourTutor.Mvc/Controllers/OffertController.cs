using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YourTutor.Application.Abstractions;
using YourTutor.Application.Commands.CreateOffert;
using YourTutor.Application.Commands.DeleteOffert;
using YourTutor.Application.Dtos;
using YourTutor.Application.Dtos.Pagination;
using YourTutor.Application.Helpers;
using YourTutor.Application.Queries.GetOffertDetails;
using YourTutor.Application.Queries.GetSmallOfferts;
using YourTutor.Infrastructure.Constans;

namespace YourTutor.Mvc.Controllers
{
    [Route("[controller]")]
    public class OffertController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OffertController> _logger;
        private readonly IHttpContextService _httpContextService;
        private readonly IAuthorizationService _authorizationService;

        public OffertController(IMediator mediator, ILogger<OffertController> logger, IHttpContextService httpContextService,
            IAuthorizationService authorizationService)
        {
            _mediator = mediator;
            _logger = logger;
            _httpContextService = httpContextService;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] PaginationDto paginationDto, [FromQuery] OffertsFilterDto offertsDto)
        {
            var query = new GetSmallOfferts(paginationDto, offertsDto);
            var response = await _mediator.Send(query);
            return View(response);
        }


        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var response = await _mediator.Send(new GetOffertDetails(id));

            if (response is null)
                return RedirectToAction(nameof(Index));

            return View(response);
        }

        [Authorize]
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateOffertDto dto)
        {
            var userId = _httpContextService.GetUserIdFromClaims();
            if (userId == Guid.Empty)
            {
                _logger.LogError(AppLogEvent.IndicateUser, "Problem with indicating user");
                return RedirectToAction(nameof(HomeController.Error), "Home");
            }

            var id = await _mediator.Send(new CreateOffert(dto, userId));

            return RedirectToAction(nameof(Details), new { id = id.Value });
        }


        [Authorize]
        [Route("delete/{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpContextService.GetUser(),
                new DeleteOffert(id), CustomAuthorizationPolicy.DeleteOffert);

            if (!authorizationResult.Succeeded)
                return new ForbidResult();

            await _mediator.Send(new DeleteOffert(id));

            return RedirectToAction(nameof(Index));
        }
    }
}
