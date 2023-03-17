using MediatR;
using YourTutor.Application.Dtos.Tutor;
using YourTutor.Core.Repositories;

namespace YourTutor.Application.Queries.Handlers
{
    public class GetTutorByUserIdHandler : IRequestHandler<GetTutorByUserId, TutorDto>
    {
        private readonly ITutorRepository _tutorRepository;

        public GetTutorByUserIdHandler(ITutorRepository tutorRepository)
        {
            _tutorRepository = tutorRepository;
        }

        public async Task<TutorDto> Handle(GetTutorByUserId request, CancellationToken cancellationToken)
        {
            var details = await _tutorRepository.GetTutorDetailsByUserId(request.UserId);
            if (details is null)
                return default;

            return new TutorDto($"{details.FirstName} {details.LastName}", details.Email, details.Description, details.Country, details.Language, null, null);
        }
    }
}


