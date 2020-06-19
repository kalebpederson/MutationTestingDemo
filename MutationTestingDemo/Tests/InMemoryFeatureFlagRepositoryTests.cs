using NUnit.Framework;

namespace MutationTestingDemo
{
    [TestFixture]
    public class InMemoryFeatureFlagRepositoryTests : FeatureFlagRepositoryTestBase
    {
        public override IFeatureFlagRepository CreateRepository()
        {
            return new InMemoryFeatureFlagRepository();
        }
    }
}