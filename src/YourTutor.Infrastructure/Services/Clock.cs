using YourTutor.Core.Abstractions;

namespace YourTutor.Infrastructure.Services
{
    public class Clock : IClock
    {
        public DateTime Now => DateTime.UtcNow;
    }
}


