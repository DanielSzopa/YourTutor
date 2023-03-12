using YourTutor.Application.Models.EmailBase;

namespace YourTutor.Application.Abstractions.Email
{
    public interface IEmailSender
    {
        Task SendEmail(EmailBase email);
    }
}
