using YourTutor.Core.ValueObjects;

namespace YourTutor.Core.Entities;

public sealed class Offert
{
    public OffertId Id { get; private set; }

    public Offert(OffertId id, string description, string subject, Price priceFrom,
        Price priceTo, bool isRemotely, string location, Tutor tutor, UserId tutorId)
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
    public Price PriceFrom { get; private set; }
    public Price PriceTo { get; private set; }
    public bool IsRemotely { get; private set; }
    public string Location { get; private set; }
    public Tutor Tutor { get; private set; }
    public UserId TutorId { get; private set; }
}


