using YourTutor.Core.Exceptions;

namespace YourTutor.Core.ValueObjects;

public sealed record OffertId
{
    public Guid Value { get; }
    public OffertId(Guid value)
    {
        if (value == Guid.Empty)
            throw new InvalidOffertIdException(value);

        Value = value;
    }

    public static implicit operator OffertId (Guid value) => new(value);

    public static implicit operator Guid (OffertId offertId) => offertId.Value;
}


