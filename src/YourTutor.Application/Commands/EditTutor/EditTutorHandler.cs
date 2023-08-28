using MediatR;
using YourTutor.Application.Abstractions.UnitOfWork;
using YourTutor.Core.Exceptions;
using YourTutor.Core.Repositories;

namespace YourTutor.Application.Commands.EditTutor;

public sealed class EditTutorHandler : IRequestHandler<EditTutor, Unit>
{
    private readonly ITutorRepository _tutorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EditTutorHandler(ITutorRepository tutorRepository, IUnitOfWork unitOfWork)
    {
        _tutorRepository = tutorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(EditTutor request, CancellationToken cancellationToken)
    {
        var tutor = await _tutorRepository.GetTutorById(request.UserId, cancellationToken) ?? throw new NotFoundTutorException(request.UserId);

        tutor.UpdateDescription(request.EditTutorVm.Description);
        tutor.UpdateCountry(request.EditTutorVm.Country);
        tutor.UpdateLanguage(request.EditTutorVm.Language);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}


