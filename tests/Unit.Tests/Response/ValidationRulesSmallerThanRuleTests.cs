using Ogu.Response;

namespace Unit.Tests.Response;

public class ValidationRulesSmallerThanRuleTests
{
    [Fact]
    public void SmallerThanRule_WithValidStringShortInput_ValidationPasses()
    {
        const string propertyName = "Id";
        const string attemptedValue= "5";
        const short expectedValue = 5;
        const short smallerThan = 10;

        // Act
        var rule = ValidationRules.SmallerThanRule(propertyName, attemptedValue, smallerThan);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(expectedValue, rule.GetStoredValue<short>());
    }

    [Fact]
    public void SmallerThanRule_WithInvalidStringShortInput_ValidationFails()
    {
        const string propertyName = "Id";
        const string attemptedValue = "a";
        const short smallerThan = 10;

        // Act
        var rule = ValidationRules.SmallerThanRule(propertyName, attemptedValue, smallerThan);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal((short)0, rule.GetStoredValue<short>());
    }

    [Fact]
    public void SmallerThanRule_WithValidStringIntInput_ValidationPasses()
    {
        const string propertyName = "Id";
        const string attemptedValue = "5";
        const int expectedValue = 5;
        const int smallerThan = 10;

        // Act
        var rule = ValidationRules.SmallerThanRule(propertyName, attemptedValue, smallerThan);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(expectedValue, rule.GetStoredValue<int>());
    }

    [Fact]
    public void SmallerThanRule_WithInvalidStringIntInput_ValidationFails()
    {
        const string propertyName = "Id";
        const string attemptedValue = "a";
        const int smallerThan = 10;

        // Act
        var rule = ValidationRules.SmallerThanRule(propertyName, attemptedValue, smallerThan);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(0, rule.GetStoredValue<int>());
    }

    [Fact]
    public void SmallerThanRule_WithValidStringLongInput_ValidationPasses()
    {
        const string propertyName = "Id";
        const string attemptedValue = "5";
        const long expectedValue = 5L;
        const long smallerThan = 10L;

        // Act
        var rule = ValidationRules.SmallerThanRule(propertyName, attemptedValue, smallerThan);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(expectedValue, rule.GetStoredValue<long>());
    }

    [Fact]
    public void SmallerThanRule_WithInvalidStringLongInput_ValidationFails()
    {
        const string propertyName = "Id";
        const string attemptedValue = "a";
        const long smallerThan = 10L;

        // Act
        var rule = ValidationRules.SmallerThanRule(propertyName, attemptedValue, smallerThan);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(0L, rule.GetStoredValue<long>());
    }

    [Fact]
    public void SmallerThanRule_WithValidStringFloatInput_ValidationPasses()
    {
        const string propertyName = "Id";
        const string attemptedValue = "5";
        const float expectedValue = 5f;
        const float smallerThan = 10f;

        // Act
        var rule = ValidationRules.SmallerThanRule(propertyName, attemptedValue, smallerThan);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(expectedValue, rule.GetStoredValue<float>());
    }

    [Fact]
    public void SmallerThanRule_WithInvalidStringFloatInput_ValidationFails()
    {
        const string propertyName = "Id";
        const string attemptedValue = "a";
        const float smallerThan = 10f;

        // Act
        var rule = ValidationRules.SmallerThanRule(propertyName, attemptedValue, smallerThan);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(0f, rule.GetStoredValue<float>());
    }

    [Fact]
    public void SmallerThanRule_WithValidStringDoubleInput_ValidationPasses()
    {
        const string propertyName = "Id";
        const string attemptedValue = "5";
        const double expectedValue = 5d;
        const double smallerThan = 10d;

        // Act
        var rule = ValidationRules.SmallerThanRule(propertyName, attemptedValue, smallerThan);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(expectedValue, rule.GetStoredValue<double>());
    }

    [Fact]
    public void SmallerThanRule_WithInvalidStringDoubleInput_ValidationFails()
    {
        const string propertyName = "Id";
        const string attemptedValue = "a";
        const double smallerThan = 10d;

        // Act
        var rule = ValidationRules.SmallerThanRule(propertyName, attemptedValue, smallerThan);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(0d, rule.GetStoredValue<double>());
    }

    [Fact]
    public void SmallerThanRule_WithValidStringDecimalInput_ValidationPasses()
    {
        const string propertyName = "Id";
        const string attemptedValue = "5";
        const decimal expectedValue = 5m;
        const decimal smallerThan = 10m;

        // Act
        var rule = ValidationRules.SmallerThanRule(propertyName, attemptedValue, smallerThan);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(expectedValue, rule.GetStoredValue<decimal>());
    }

    [Fact]
    public void SmallerThanRule_WithInvalidStringDecimalInput_ValidationFails()
    {
        const string propertyName = "Id";
        const string attemptedValue = "a";
        const decimal smallerThan = 10m;

        // Act
        var rule = ValidationRules.SmallerThanRule(propertyName, attemptedValue, smallerThan);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(0m, rule.GetStoredValue<int>());
    }

    [Fact]
    public void SmallerThanRule_WithValidShortInput_ValidationPasses()
    {
        const string propertyName = "Id";
        const short attemptedValue = 5;
        const short smallerThan = 10;

        // Act
        var rule = ValidationRules.SmallerThanRule(propertyName, attemptedValue, smallerThan);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(0m, rule.GetStoredValue<decimal>());
    }

    [Fact]
    public void SmallerThanRule_WithInvalidShortInput_ValidationFails()
    {
        const string propertyName = "Id";
        const short attemptedValue = 15;
        const short smallerThan = 10;

        // Act
        var rule = ValidationRules.SmallerThanRule(propertyName, attemptedValue, smallerThan);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal((short)0, rule.GetStoredValue<short>());
    }

    [Fact]
    public void SmallerThanRule_WithValidIntInput_ValidationPasses()
    {
        const string propertyName = "Id";
        const int attemptedValue = 5;
        const int smallerThan = 10;

        // Act
        var rule = ValidationRules.SmallerThanRule(propertyName, attemptedValue, smallerThan);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(0m, rule.GetStoredValue<decimal>());
    }

    [Fact]
    public void SmallerThanRule_WithInvalidIntInput_ValidationFails()
    {
        const string propertyName = "Id";
        const int attemptedValue = 15;
        const int smallerThan = 10;

        // Act
        var rule = ValidationRules.SmallerThanRule(propertyName, attemptedValue, smallerThan);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(0, rule.GetStoredValue<int>());
    }

    [Fact]
    public void SmallerThanRule_WithValidLongInput_ValidationPasses()
    {
        const string propertyName = "Id";
        const long attemptedValue = 5L;
        const long smallerThan = 10L;

        // Act
        var rule = ValidationRules.SmallerThanRule(propertyName, attemptedValue, smallerThan);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(0m, rule.GetStoredValue<decimal>());
    }

    [Fact]
    public void SmallerThanRule_WithInvalidLongInput_ValidationFails()
    {
        const string propertyName = "Id";
        const long attemptedValue = 15L;
        const long smallerThan = 10L;

        // Act
        var rule = ValidationRules.SmallerThanRule(propertyName, attemptedValue, smallerThan);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(0L, rule.GetStoredValue<long>());
    }

    [Fact]
    public void SmallerThanRule_WithValidFloatInput_ValidationPasses()
    {
        const string propertyName = "Id";
        const float attemptedValue = 5f;
        const float smallerThan = 10f;

        // Act
        var rule = ValidationRules.SmallerThanRule(propertyName, attemptedValue, smallerThan);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(0m, rule.GetStoredValue<decimal>());
    }

    [Fact]
    public void SmallerThanRule_WithInvalidFloatInput_ValidationFails()
    {
        const string propertyName = "Id";
        const float attemptedValue = 15f;
        const float smallerThan = 10f;

        // Act
        var rule = ValidationRules.SmallerThanRule(propertyName, attemptedValue, smallerThan);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(0f, rule.GetStoredValue<float>());
    }

    [Fact]
    public void SmallerThanRule_WithValidDoubleInput_ValidationPasses()
    {
        const string propertyName = "Id";
        const double attemptedValue = 5d;
        const double smallerThan = 10d;

        // Act
        var rule = ValidationRules.SmallerThanRule(propertyName, attemptedValue, smallerThan);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(0m, rule.GetStoredValue<decimal>());
    }

    [Fact]
    public void SmallerThanRule_WithInvalidDoubleInput_ValidationFails()
    {
        const string propertyName = "Id";
        const double attemptedValue = 15d;
        const double smallerThan = 10d;

        // Act
        var rule = ValidationRules.SmallerThanRule(propertyName, attemptedValue, smallerThan);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(0d, rule.GetStoredValue<double>());
    }

    [Fact]
    public void SmallerThanRule_WithValidDecimalInput_ValidationPasses()
    {
        const string propertyName = "Id";
        const decimal attemptedValue = 5m;
        const decimal smallerThan = 10m;

        // Act
        var rule = ValidationRules.SmallerThanRule(propertyName, attemptedValue, smallerThan);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(0m, rule.GetStoredValue<decimal>());
    }

    [Fact]
    public void SmallerThanRule_WithInvalidDecimalInput_ValidationFails()
    {
        const string propertyName = "Id";
        const decimal attemptedValue = 15m;
        const decimal smallerThan = 10m;

        // Act
        var rule = ValidationRules.SmallerThanRule(propertyName, attemptedValue, smallerThan);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(0m, rule.GetStoredValue<int>());
    }
}