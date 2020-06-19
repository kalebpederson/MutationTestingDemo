using AutoFixture;
using NUnit.Framework;

namespace MutationTestingDemo
{
    [TestFixture]
    public abstract class FeatureFlagRepositoryTestBase
    {
        private readonly Fixture _fixture = new Fixture();

        public abstract IFeatureFlagRepository CreateRepository();
        
        [Test]
        public void Can_retrieve_a_value_that_exists()
        {
            var flagName = CreateRandomFlagName();
            var flagValue = true;
            var repo = CreateRepository();
            repo.SetEnabled(flagName, flagValue);
            var isEnabled = repo.IsEnabled(flagName);
            Assert.That(isEnabled, Is.True);
        }

        [Test]
        public void Returns_false_when_flag_does_not_exist()
        {
            var flagName = CreateRandomFlagName();
            var repo = CreateRepository();
            var isEnabled = repo.IsEnabled(flagName);
            Assert.That(isEnabled, Is.False);
        }
        
        protected string CreateRandomFlagName()
        {
            return _fixture.Create<string>();
        }
    }
}