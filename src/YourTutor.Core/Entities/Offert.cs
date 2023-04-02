using YourTutor.Core.ValueObjects;

namespace YourTutor.Core.Entities
{
    public sealed class Offert
    {
        public Guid Id { get; private set; }

        public Offert(Guid id, string description, string subject, int priceFrom,
            int priceTo, bool isRemotely, string location, Tutor tutor, UserId tutorId)
        {
            Id = id;
            Description = description;
            Subject = subject;
            PriceFrom = priceFrom;
            PriceTo = priceTo;
            IsRemotely = isRemotely;
            Location = location;
            Tutor = tutor;
            TutorId = tutorId;
        }

        public string Description { get; private set; }
        public string Subject { get; private set; }
        public int PriceFrom { get; private set; }
        public int PriceTo { get; private set; }
        public bool IsRemotely { get; private set; }
        public string Location { get; private set; }
        public Tutor Tutor { get; private set; }
        public UserId TutorId { get; private set; }
    }
}


