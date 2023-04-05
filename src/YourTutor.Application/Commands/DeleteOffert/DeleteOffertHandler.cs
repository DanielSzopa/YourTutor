using MediatR;
using Microsoft.Extensions.Logging;
using YourTutor.Application.Abstractions.UnitOfWork;
using YourTutor.Core.Repositories;

namespace YourTutor.Application.Commands.DeleteOffert;

public sealed class DeleteOffertHandler : IRequestHandler<DeleteOffert, Unit>
{
    private readonly IOffertRepository _offertRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteOffertHandler> _logger;

    public DeleteOffertHandler(IOffertRepository offertRepository, IUnitOfWork unitOfWork,
        ILogger<DeleteOffertHandler> logger)
    {
        _offertRepository = offertRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteOffert request, CancellationToken cancellationToken)
    {
        await _offertRepository.RemoveOffertById(request.Id);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Offert with id: {0} has been removed", request.Id.ToString());
        return Unit.Value;
    }
}


