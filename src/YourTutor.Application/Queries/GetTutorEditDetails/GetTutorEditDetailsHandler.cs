using MediatR;
using YourTutor.Application.ViewModels;
using YourTutor.Core.Repositories;

namespace YourTutor.Application.Queries.GetTutorEditDetails;

public sealed class GetTutorEditDetailsHandler : IRequestHandler<GetTutorEditDetails, EditTutorVm>
{
    private readonly ITutorRepository _tutorRepository;

    public GetTutorEditDetailsHandler(ITutorRepository tutorRepository)
    {
        _tutorRepository = tutorRepository;
    }

    public async Task<EditTutorVm> Handle(GetTutorEditDetails request, CancellationToken cancellationToken)
    {
        var details = await _tutorRepository.GetTutorDetailsForEdit(request.TutorId, cancellationToken);

        return new()
        {
            Country = details.Country,
            Description = details.Description,
            Language = details.Language
        };
    }
}


