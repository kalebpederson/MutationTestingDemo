using System.Collections.Generic;

namespace MutationTestingDemo
{
    public class InMemoryFeatureFlagRepository : IFeatureFlagRepository
    {
        private readonly Dictionary<string, bool> _store = new Dictionary<string, bool>();
        
        public bool IsEnabled(string flagName)
        {
            if (_store.TryGetValue(flagName, out var value))
            {
                return value;
            }
            return false;
        }

        public void SetEnabled(string flagName, bool enabled)
        {
            _store[flagName] = enabled;
        }
    }
}