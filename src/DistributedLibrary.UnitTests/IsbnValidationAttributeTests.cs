using DistributedLibrary.Data.Validation;

namespace DistributedLibrary.UnitTests;

public class IsbnValidationAttributeTests
{
    [Theory]
    [InlineData("", false)]
    [InlineData("0-306-40615-2", true)]
    [InlineData("0306406152", true)]
    [InlineData("0-306-40615-1", false)]
    [InlineData("978-0-306-40615-7", true)]
    [InlineData("9780306406157", true)]
    [InlineData("978+0+306+40615+7", false)]
    [InlineData("978-0-306-40615-6", false)]
    [InlineData("abc-0-306-40615-6", false)]
    [InlineData("-1", false)]
    public void IsbnValidationAttribute_IsValid_OkOrFail(string isbn, bool isValid)
    {
        var att = new IsbnValidationAttribute();
        var result = att.IsValid(isbn);

        Assert.Equal(result, isValid);
    }
}