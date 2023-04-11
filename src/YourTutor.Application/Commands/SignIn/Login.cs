using MediatR;
using YourTutor.Application.ViewModels;

namespace YourTutor.Application.Commands.SignIn;

public sealed record Login(LoginVm LoginVm) : IRequest<LoginResponse>;


