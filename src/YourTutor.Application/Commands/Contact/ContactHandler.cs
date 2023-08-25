using MediatR;
using Microsoft.Extensions.Options;
using YourTutor.Application.Abstractions.Email;
using YourTutor.Application.Models.Email;
using YourTutor.Application.Settings.Email;

namespace YourTutor.Application.Commands.Contact;

public sealed class ContactHandler : IRequestHandler<Contact, Unit>
{
    private readonly IEmailSender _emailSender;
    private readonly EmailSettings _emailSettings;

    public ContactHandler(IEmailSender emailSender, IOptions<EmailSettings> emailSettings)
    {
        _emailSender = emailSender;
        _emailSettings = emailSettings.Value;
    }

    public async Task<Unit> Handle(Contact request, CancellationToken cancellationToken)
    {
        var vm = request.ContactVm;
        var body = $"From: {vm.Email} \nName: {vm.Name} \nDescription: {vm.Description}";
        var email = new ContactEmail(body, vm.To, _emailSettings.From);

        await _emailSender.SendEmailAsync(email, cancellationToken);

        return Unit.Value;
    }
}


