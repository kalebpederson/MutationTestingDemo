using System.Collections.Generic;

namespace MutationTestingDemo
{
    public class CachingFeatureFlagRepository : IFeatureFlagRepository
    {
        private readonly IFeatureFlagRepository _dataStore;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly Dictionary<string, CacheItem> _cache;

        public CachingFeatureFlagRepository(IFeatureFlagRepository dataStore, IDateTimeProvider dateTimeProvider)
        {
            _dataStore = dataStore;
            _dateTimeProvider = dateTimeProvider;
            _cache = new Dictionary<string, CacheItem>();
        }
        
        public bool IsEnabled(string flagName)
        {
            if (_cache.TryGetValue(flagName, out var cacheItem))
            {
                if (!IsExpired(cacheItem))
                {
                    return cacheItem.Value;
                }
            }
            var value = _dataStore.IsEnabled(flagName);
            _cache[flagName] = new CacheItem
            {
                Value = value,
                ExpiryTime = _dateTimeProvider.Now
            };
            return value;
        }

        public void SetEnabled(string flagName, bool enabled)
        {
            _dataStore.SetEnabled(flagName, enabled);
            _cache[flagName] = new CacheItem
            {
                Value = enabled,
                ExpiryTime = _dateTimeProvider.Now
            };
        }

        private bool IsExpired(CacheItem cacheItem)
        {
            return _dateTimeProvider.Now > cacheItem.ExpiryTime;
        }
    }
}