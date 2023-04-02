using YourTutor.Core.ValueObjects;

namespace YourTutor.Core.Entities;

public sealed class Offert
{
    public OffertId Id { get; private set; }

    public Offert(OffertId id, string description, string subject, Price price,
        bool isRemotely, string location, UserId tutorId)
    {
        Id = id;
        Description = description;
        Subject = subject;
        Price = price;
        IsRemotely = isRemotely;
        Location = location;
        TutorId = tutorId;
    }

    public string Description { get; private set; }
    public string Subject { get; private set; }
    public Price Price { get; private set; }
    public bool IsRemotely { get; private set; }
    public string Location { get; private set; }
    public Tutor Tutor { get; private set; }
    public UserId TutorId { get; private set; }
}


