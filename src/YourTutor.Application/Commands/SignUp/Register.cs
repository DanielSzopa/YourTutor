using MediatR;
using YourTutor.Application.ViewModels;

namespace YourTutor.Application.Commands.SignUp;

public sealed record Register(RegisterVm RegisterVm) : IRequest<RegisterResponse>;


