using MediatR;
using YourTutor.Application.Abstractions.UnitOfWork;
using YourTutor.Core.Entities;
using YourTutor.Core.Repositories;
using YourTutor.Core.ValueObjects;

namespace YourTutor.Application.Commands.Handlers
{
    public sealed class CreateOffertHandler : IRequestHandler<CreateOffert, OffertId>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOffertRepository _offertRepository;

        public CreateOffertHandler(IUnitOfWork unitOfWork, IOffertRepository offertRepository)
        {
            _unitOfWork = unitOfWork;
            _offertRepository = offertRepository;
        }

        public async Task<OffertId> Handle(CreateOffert request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;
            var offert = new Offert(Guid.NewGuid(), dto.Description, dto.Subject, dto.PriceFrom, dto.PriceTo, dto.IsRemotely, dto.Location, request.UserId);

            await _offertRepository.CreateOffert(offert);
            await _unitOfWork.SaveChangesAsync();

            return offert.Id;
        }
    }
}


