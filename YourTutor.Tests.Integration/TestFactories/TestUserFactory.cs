using YourTutor.Core.Entities;

namespace YourTutor.Tests.Integration.TestFactories;

public static class TestUserFactory
{
    public static User User
        => new User(Guid.NewGuid(), "phill@gmail.com", "Phill", "Cash", "notHashedPass!1");
}
