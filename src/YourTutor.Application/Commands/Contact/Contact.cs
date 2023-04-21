using MediatR;
using YourTutor.Application.ViewModels;

namespace YourTutor.Application.Commands.Contact;

public sealed record Contact(ContactVm ContactVm) :IRequest<Unit>;


