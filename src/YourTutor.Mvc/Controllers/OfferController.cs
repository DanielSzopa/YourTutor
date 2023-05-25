using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YourTutor.Application.Abstractions;
using YourTutor.Application.Commands.Contact;
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
        private readonly ISender _sender;
        private readonly ILogger<OfferController> _logger;
        private readonly IHttpContextService _httpContextService;
        private readonly IAuthorizationService _authorizationService;

        public OfferController(ISender sender, ILogger<OfferController> logger, IHttpContextService httpContextService,
            IAuthorizationService authorizationService)
        {
            _sender = sender;
            _logger = logger;
            _httpContextService = httpContextService;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] PaginationDto paginationDto, [FromQuery] OffersFilterDto offersDto)
        {
            var query = new GetSmallOffers(paginationDto, offersDto);
            var response = await _sender.Send(query);
            return View(response);
        }


        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var details = await _sender.Send(new GetOfferDetails(id));        

            if (details is null)
                return RedirectToAction(nameof(Index));

            var result = _httpContextService.GetUserIdFromClaims()  == details.TutorId 
                ? true 
                : false;

            ViewBag.IsHisOffer = result;

            return View(details);
        }

        [Authorize]
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateOfferVm vm)
        {
            var userId = _httpContextService.GetUserIdFromClaims();
            if (userId == Guid.Empty)
            {
                _logger.LogError(AppLogEvent.IndicateUser, "Problem with indicating user");
                return RedirectToAction(nameof(HomeController.Error), "Home");
            }

            var offerId = await _sender.Send(new CreateOffer(vm, userId));

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

            await _sender.Send(new DeleteOffer(id));

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("contact/{id:Guid}")]
        public IActionResult Contact(Guid id, string to)
        {
            return View(new ContactVm()
            {
                Id = id,
                Description = "Tell something about you...",
                Email = "Your Email...",
                To = to,
                Name = "Your Name..."
            });
        }

        [HttpPost("contact/{id:Guid}")]
        public async Task<IActionResult> Contact(ContactVm contactVm)
        {
            await _sender.Send(new Contact(contactVm));
            return RedirectToAction(nameof(Details), new { id = contactVm.Id });
        }
    }
}
