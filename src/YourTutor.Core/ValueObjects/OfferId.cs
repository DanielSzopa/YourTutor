using YourTutor.Core.Exceptions;

namespace YourTutor.Core.ValueObjects;

public sealed record OfferId
{
    public Guid Value { get; }
    public OfferId(Guid value)
    {
        if (value == Guid.Empty)
            throw new InvalidOfferIdException(value);

        Value = value;
    }

    public static implicit operator OfferId (Guid value) => new(value);

    public static implicit operator Guid (OfferId offerId) => offerId.Value;
}


