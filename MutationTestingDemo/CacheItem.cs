using System;

namespace MutationTestingDemo
{
    public struct CacheItem
    {
        public bool Value { get; set; }
        public DateTime ExpiryTime { get; set; }
    }
}