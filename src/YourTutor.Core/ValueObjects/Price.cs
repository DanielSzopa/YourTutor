using YourTutor.Core.Exceptions;

namespace YourTutor.Core.ValueObjects;

public sealed record Price
{
    public int Value { get; }

    public Price(int value)
    {
        if (value <= 0)
            throw new InvalidPriceException(value);

        Value = value;
    }

    public static implicit operator int (Price price) => price.Value;

    public static implicit operator Price(int price) => new(price);
}


