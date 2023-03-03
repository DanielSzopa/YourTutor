using MediatR;

namespace YourTutor.Application.Commands;

public record Register(Guid Id, string Email, string FirstName, string LastName, string Password, string PasswordConfirmation) : IRequest<Unit>;
