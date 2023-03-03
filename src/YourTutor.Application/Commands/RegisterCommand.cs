using MediatR;

namespace YourTutor.Application.Commands;

public record RegisterCommand(Guid Id, string Email, string FirstName, string LastName, string Password, string PasswordConfirmation) : IRequest<Unit>;
