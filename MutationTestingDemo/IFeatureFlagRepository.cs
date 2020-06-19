namespace MutationTestingDemo
{
    public interface IFeatureFlagRepository
    {
        bool IsEnabled(string flagName);
        void SetEnabled(string flagName, bool enabled);
    }
}