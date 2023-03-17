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
            var tutor = await _tutorRepository.GetTutorDetailsByUserId(request.UserId);
            if (tutor is null)
                return default;

            return new TutorDto($"{tutor.User.FirstName} {tutor.User.LastName}", tutor.User.Email, tutor.Description, tutor.Country, tutor.Language, null, null);
        }
    }
}


