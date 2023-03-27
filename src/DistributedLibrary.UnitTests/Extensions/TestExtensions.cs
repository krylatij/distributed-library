using System.Diagnostics.CodeAnalysis;
using MockQueryable.Moq;

namespace DistributedLibrary.UnitTests.Extensions;

internal static class TestExtensions
{
    public static IQueryable<T> ToQueryable<T>(this T item)
    {
        return new []{item}.AsQueryable();
    }

    public static IQueryable<T> ToQueryableAsync<T>(this T item) where T : class
    {
        return new[] { item }.AsQueryable().BuildMock();
    }
}