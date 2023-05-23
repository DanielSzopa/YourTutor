namespace YourTutor.Tests.Integration.Helpers.Fixtures;

public class FakerFixture
{
    public Faker Faker { get; }

    public FakerFixture()
    {
        Faker = new Faker();
    }
}
