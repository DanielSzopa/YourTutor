﻿using MediatR;
using YourTutor.Application.Abstractions.UnitOfWork;
using YourTutor.Core.Exceptions;
using YourTutor.Core.Repositories;

namespace YourTutor.Application.Commands.Handlers;

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
        var tutor = await _tutorRepository.GetTutorById(request.UserId);
        if (tutor is null)
            throw new NotFoundTutorException(request.UserId);

        tutor.UpdateDescription(request.Dto.Description);
        tutor.UpdateCountry(request.Dto.Country);
        tutor.UpdateLanguage(request.Dto.Language);

        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}


