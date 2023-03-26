using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YourTutor.Application.Abstractions;
using YourTutor.Application.Commands;
using YourTutor.Application.Dtos;
using YourTutor.Application.Queries;

namespace YourTutor.Mvc.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public sealed class TutorController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextService _httpContextService;
        private readonly ILogger<TutorController> _logger;

        public TutorController(IMediator mediator, IHttpContextService httpContextService
            , ILogger<TutorController> logger)
        {
            _mediator = mediator;
            _httpContextService = httpContextService;
            _logger = logger;
        }

        [HttpGet]      
        public async Task<IActionResult> MyAccount()
        {
            var userId = _httpContextService.GetUserIdFromClaims();
            if (userId == Guid.Empty)
            {
                _logger.LogError("Problem with indicating user");
                return RedirectToAction(nameof(HomeController.Error), "Home");
            }

            var tutorDto = await _mediator.Send(new GetTutorByUserId(userId));

            return View("Tutor", tutorDto);
        }

        [HttpGet("Edit")]
        public IActionResult Edit()
        {           
            return View();
        }

        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(EditTutorDto dto)
        {
            var userId = _httpContextService.GetUserIdFromClaims();
            if (userId == Guid.Empty)
            {
                _logger.LogError("Problem with indicating user");
                return RedirectToAction(nameof(HomeController.Error), "Home");
            }

            await _mediator.Send(new EditTutor(dto,userId));
            return RedirectToAction(nameof(MyAccount));
        }
    }
}
