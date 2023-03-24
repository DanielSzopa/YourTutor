using MediatR;
using YourTutor.Core.Exceptions;
using YourTutor.Core.Repositories;

namespace YourTutor.Application.Commands.Handlers;

public sealed class EditTutorHandler : IRequestHandler<EditTutor, Unit>
{
    private readonly ITutorRepository _tutorRepository;

    public EditTutorHandler(ITutorRepository tutorRepository)
    {
        _tutorRepository = tutorRepository;
    }

    public async Task<Unit> Handle(EditTutor request, CancellationToken cancellationToken)
    {
        var tutor = await _tutorRepository.GetTutorById(request.UserId);
        if (tutor is null)
            throw new NotFoundTutorException(request.UserId);

        tutor.UpdateDescription(request.Dto.Description);
        tutor.UpdateCountry(request.Dto.Country);
        tutor.UpdateLanguage(request.Dto.Language);

        //Add need to add unitOfWork for saveChangesAsync

        return Unit.Value;
    }
}


