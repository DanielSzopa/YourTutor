using YourTutor.Tests.Integration.Helpers.Fixtures;

namespace YourTutor.Tests.Integration.Setup;

[CollectionDefinition(nameof(YourTutorCollection))]
public class YourTutorCollection : ICollectionFixture<YourTutorApp>, ICollectionFixture<FakerFixture>
{
}


