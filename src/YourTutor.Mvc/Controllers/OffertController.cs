﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YourTutor.Application.Abstractions;
using YourTutor.Application.Commands;
using YourTutor.Application.Dtos;
using YourTutor.Application.Dtos.Pagination;
using YourTutor.Application.Helpers;
using YourTutor.Application.Queries;

namespace YourTutor.Mvc.Controllers
{
    [Route("[controller]")]
    public class OffertController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OffertController> _logger;
        private readonly IHttpContextService _httpContextService;

        public OffertController(IMediator mediator, ILogger<OffertController> logger, IHttpContextService httpContextService)
        {
            _mediator = mediator;
            _logger = logger;
            _httpContextService = httpContextService;
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
            //Todo:
            //Redirect to new created offert
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery]PaginationDto paginationDto, [FromQuery]OffertsFilterDto offertsDto)
        {
            var query = new GetSmallOfferts(paginationDto, offertsDto);
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
