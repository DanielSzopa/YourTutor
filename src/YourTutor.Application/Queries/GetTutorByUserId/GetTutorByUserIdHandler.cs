using MediatR;
using YourTutor.Application.ViewModels;
using YourTutor.Core.Repositories;

namespace YourTutor.Application.Queries.GetTutorByUserId
{
    public class GetTutorByUserIdHandler : IRequestHandler<GetTutorByUserId, TutorDetailsVm>
    {
        private readonly ITutorRepository _tutorRepository;

        public GetTutorByUserIdHandler(ITutorRepository tutorRepository)
        {
            _tutorRepository = tutorRepository;
        }

        public async Task<TutorDetailsVm> Handle(GetTutorByUserId request, CancellationToken cancellationToken)
        {
            var details = await _tutorRepository.GetTutorDetailsByUserId(request.UserId) ?? default;

            var (fullName, email, description, country, language) = details;

            return new TutorDetailsVm(fullName, email, description, country, language);
        }
    }
}


