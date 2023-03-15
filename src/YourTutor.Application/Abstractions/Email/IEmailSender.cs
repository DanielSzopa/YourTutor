using YourTutor.Application.Models.EmailBase;

namespace YourTutor.Application.Abstractions.Email
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailBase email);
    }
}
