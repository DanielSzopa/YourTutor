using MediatR;
using YourTutor.Core.ReadModels;
using YourTutor.Core.Repositories;

namespace YourTutor.Application.Queries.Handlers
{
    public class GetTutorByUserIdHandler : IRequestHandler<GetTutorByUserId, TutorDetailsReadModel>
    {
        private readonly ITutorRepository _tutorRepository;

        public GetTutorByUserIdHandler(ITutorRepository tutorRepository)
        {
            _tutorRepository = tutorRepository;
        }

        public async Task<TutorDetailsReadModel> Handle(GetTutorByUserId request, CancellationToken cancellationToken)
        {
            var details = await _tutorRepository.GetTutorDetailsByUserId(request.UserId);
            if (details is null)
                return default;

            return details;
        }
    }
}


