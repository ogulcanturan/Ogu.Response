using Ogu.Response.Abstractions;

namespace Unit.Tests.Response.Abstractions;

public class ResponseDefaultsTests
{
    [Fact]
    public void ResponseDefaults_ShouldHaveDefaultValues()
    {
        HashSet<int> codes = [204, 205, 304];

        // Act & Assert
        Assert.NotNull(ResponseDefaults.NoResponseStatusCodes);
        Assert.Equal(3, ResponseDefaults.NoResponseStatusCodes.Count);
        Assert.All(ResponseDefaults.NoResponseStatusCodes, i => Assert.Contains(i, codes));
    }
}