using YourTutor.Application.Models.EmailBase;

namespace YourTutor.Application.Abstractions
{
    public interface IEmailSender
    {
        Task<bool> SendEmail(EmailBase email);
    }
}
