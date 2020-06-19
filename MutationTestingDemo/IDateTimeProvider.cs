using System;

namespace MutationTestingDemo
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}