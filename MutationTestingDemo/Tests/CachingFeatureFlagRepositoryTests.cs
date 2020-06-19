using System;
using Moq;
using NUnit.Framework;

namespace MutationTestingDemo
{
    [TestFixture]
    public class CachingFeatureFlagRepositoryTests : FeatureFlagRepositoryTestBase
    {
        public override IFeatureFlagRepository CreateRepository()
        {
            return new CachingFeatureFlagRepository(
               new InMemoryFeatureFlagRepository(),
                new DateTimeProvider()
                );
        }

        [Test]
        public void IsEnabled_retrieves_value_from_cache_when_value_is_not_expired()
        {
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var flagName = CreateRandomFlagName();
            var memoryRepository = new InMemoryFeatureFlagRepository();
            var repo = 
                new CachingFeatureFlagRepository(
                    memoryRepository, 
                    mockDateTimeProvider.Object
                    );
            mockDateTimeProvider
                .Setup(x => x.Now)
                .Returns(DateTime.Now.AddMinutes(5));
            repo.SetEnabled(flagName, true);
            mockDateTimeProvider
                .Setup(x => x.Now)
                .Returns(DateTime.Now.AddMinutes(-5));
            memoryRepository.SetEnabled(flagName, false);
            var isEnabled = repo.IsEnabled(flagName);
            Assert.That(isEnabled, Is.True);
        }
    }
}