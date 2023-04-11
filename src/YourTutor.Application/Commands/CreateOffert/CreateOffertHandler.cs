using MediatR;
using YourTutor.Application.Abstractions.UnitOfWork;
using YourTutor.Core.Entities;
using YourTutor.Core.Repositories;

namespace YourTutor.Application.Commands.CreateOffert
{
    public sealed class CreateOffertHandler : IRequestHandler<CreateOffert, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOffertRepository _offertRepository;

        public CreateOffertHandler(IUnitOfWork unitOfWork, IOffertRepository offertRepository)
        {
            _unitOfWork = unitOfWork;
            _offertRepository = offertRepository;
        }

        public async Task<Guid> Handle(CreateOffert request, CancellationToken cancellationToken)
        {
            var vm = request.CreateOffertVm;
            var offert = new Offert(Guid.NewGuid(), vm.Description, vm.Subject, vm.Price, vm.IsRemotely, vm.Location, request.UserId);

            await _offertRepository.CreateOffert(offert);
            await _unitOfWork.SaveChangesAsync();

            return offert.Id;
        }
    }
}


