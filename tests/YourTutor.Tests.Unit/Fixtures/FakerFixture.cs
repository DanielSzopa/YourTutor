namespace YourTutor.Tests.Unit.Fixtures;

public class FakerFixture
{
    public Faker Faker { get; set; }
    public FakerFixture()
    {
        Faker = new();
    }
}


