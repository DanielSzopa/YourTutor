using MediatR;
using YourTutor.Application.ViewModels;
using YourTutor.Core.Repositories;

namespace YourTutor.Application.Queries.GetTutorByUserId
{
    public class GetTutorByUserIdHandler : IRequestHandler<GetTutorByUserId, GetTutorByUserIdResponse>
    {
        private readonly ITutorRepository _tutorRepository;

        public GetTutorByUserIdHandler(ITutorRepository tutorRepository)
        {
            _tutorRepository = tutorRepository;
        }

        public async Task<GetTutorByUserIdResponse> Handle(GetTutorByUserId request, CancellationToken cancellationToken)
        {
            var details = await _tutorRepository.GetTutorDetailsByUserId(request.UserId) ?? default;

            var (fullName, email, description, country, language) = details;

            return new(new TutorDetailsVm(fullName, email, description, country, language));
        }
    }
}


