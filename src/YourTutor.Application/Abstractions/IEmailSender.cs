using YourTutor.Application.Models.EmailBase;

namespace YourTutor.Application.Abstractions
{
    public interface IEmailSender
    {
        Task SendEmail(EmailBase email);
    }
}
