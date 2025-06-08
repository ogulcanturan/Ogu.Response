using Ogu.Response;

namespace Unit.Tests.Response;

public class ValidationRulesGreaterThanRuleTests
{
    [Fact]
    public void GreaterThanRule_WithValidStringShortInput_ValidationPasses()
    {
        const string propertyName = "Id";
        const string attemptedValue = "25";
        const short expectedValue = 25;
        const short greaterThan = 10;

        // Act
        var rule = ValidationRules.GreaterThanRule(propertyName, attemptedValue, greaterThan);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(expectedValue, rule.GetStoredValue<short>());
    }

    [Fact]
    public void GreaterThanRule_WithInvalidStringShortInput_ValidationFails()
    {
        const string propertyName = "Id";
        const string attemptedValue = "a";
        const short greaterThan = 10;

        // Act
        var rule = ValidationRules.GreaterThanRule(propertyName, attemptedValue, greaterThan);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal((short)0, rule.GetStoredValue<short>());
    }

    [Fact]
    public void GreaterThanRule_WithValidStringIntInput_ValidationPasses()
    {
        const string propertyName = "Id";
        const string attemptedValue = "25";
        const int expectedValue = 25;
        const int greaterThan = 10;

        // Act
        var rule = ValidationRules.GreaterThanRule(propertyName, attemptedValue, greaterThan);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(expectedValue, rule.GetStoredValue<int>());
    }

    [Fact]
    public void GreaterThanRule_WithInvalidStringIntInput_ValidationFails()
    {
        const string propertyName = "Id";
        const string attemptedValue = "a";
        const int greaterThan = 10;

        // Act
        var rule = ValidationRules.GreaterThanRule(propertyName, attemptedValue, greaterThan);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(0, rule.GetStoredValue<int>());
    }

    [Fact]
    public void GreaterThanRule_WithValidStringLongInput_ValidationPasses()
    {
        const string propertyName = "Id";
        const string attemptedValue = "25";
        const long expectedValue = 25L;
        const long greaterThan = 10L;

        // Act
        var rule = ValidationRules.GreaterThanRule(propertyName, attemptedValue, greaterThan);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(expectedValue, rule.GetStoredValue<long>());
    }

    [Fact]
    public void GreaterThanRule_WithInvalidStringLongInput_ValidationFails()
    {
        const string propertyName = "Id";
        const string attemptedValue = "a";
        const long greaterThan = 10L;

        // Act
        var rule = ValidationRules.GreaterThanRule(propertyName, attemptedValue, greaterThan);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(0L, rule.GetStoredValue<long>());
    }

    [Fact]
    public void GreaterThanRule_WithValidStringFloatInput_ValidationPasses()
    {
        const string propertyName = "Id";
        const string attemptedValue = "25";
        const float expectedValue = 25f;
        const float greaterThan = 10f;

        // Act
        var rule = ValidationRules.GreaterThanRule(propertyName, attemptedValue, greaterThan);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(expectedValue, rule.GetStoredValue<float>());
    }

    [Fact]
    public void GreaterThanRule_WithInvalidStringFloatInput_ValidationFails()
    {
        const string propertyName = "Id";
        const string attemptedValue = "a";
        const float greaterThan = 10f;

        // Act
        var rule = ValidationRules.GreaterThanRule(propertyName, attemptedValue, greaterThan);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(0f, rule.GetStoredValue<float>());
    }

    [Fact]
    public void GreaterThanRule_WithValidStringDoubleInput_ValidationPasses()
    {
        const string propertyName = "Id";
        const string attemptedValue = "25";
        const double expectedValue = 25d;
        const double greaterThan = 10d;

        // Act
        var rule = ValidationRules.GreaterThanRule(propertyName, attemptedValue, greaterThan);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(expectedValue, rule.GetStoredValue<double>());
    }

    [Fact]
    public void GreaterThanRule_WithInvalidStringDoubleInput_ValidationFails()
    {
        const string propertyName = "Id";
        const string attemptedValue = "a";
        const double greaterThan = 10d;

        // Act
        var rule = ValidationRules.GreaterThanRule(propertyName, attemptedValue, greaterThan);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(0d, rule.GetStoredValue<double>());
    }

    [Fact]
    public void GreaterThanRule_WithValidStringDecimalInput_ValidationPasses()
    {
        const string propertyName = "Id";
        const string attemptedValue = "25";
        const decimal expectedValue = 25m;
        const decimal greaterThan = 10m;

        // Act
        var rule = ValidationRules.GreaterThanRule(propertyName, attemptedValue, greaterThan);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(expectedValue, rule.GetStoredValue<decimal>());
    }

    [Fact]
    public void GreaterThanRule_WithInvalidStringDecimalInput_ValidationFails()
    {
        const string propertyName = "Id";
        const string attemptedValue = "a";
        const decimal greaterThan = 10m;

        // Act
        var rule = ValidationRules.GreaterThanRule(propertyName, attemptedValue, greaterThan);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(0m, rule.GetStoredValue<int>());
    }

    [Fact]
    public void GreaterThanRule_WithValidShortInput_ValidationPasses()
    {
        const string propertyName = "Id";
        const short attemptedValue = 25;
        const short greaterThan = 10;

        // Act
        var rule = ValidationRules.GreaterThanRule(propertyName, attemptedValue, greaterThan);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(0m, rule.GetStoredValue<decimal>());
    }

    [Fact]
    public void GreaterThanRule_WithInvalidShortInput_ValidationFails()
    {
        const string propertyName = "Id";
        const short attemptedValue = 5;
        const short greaterThan = 10;

        // Act
        var rule = ValidationRules.GreaterThanRule(propertyName, attemptedValue, greaterThan);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal((short)0, rule.GetStoredValue<short>());
    }

    [Fact]
    public void GreaterThanRule_WithValidIntInput_ValidationPasses()
    {
        const string propertyName = "Id";
        const int attemptedValue = 25;
        const int greaterThan = 10;

        // Act
        var rule = ValidationRules.GreaterThanRule(propertyName, attemptedValue, greaterThan);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(0m, rule.GetStoredValue<decimal>());
    }

    [Fact]
    public void GreaterThanRule_WithInvalidIntInput_ValidationFails()
    {
        const string propertyName = "Id";
        const int attemptedValue = 5;
        const int greaterThan = 10;

        // Act
        var rule = ValidationRules.GreaterThanRule(propertyName, attemptedValue, greaterThan);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(0, rule.GetStoredValue<int>());
    }

    [Fact]
    public void GreaterThanRule_WithValidLongInput_ValidationPasses()
    {
        const string propertyName = "Id";
        const long attemptedValue = 25L;
        const long greaterThan = 10L;

        // Act
        var rule = ValidationRules.GreaterThanRule(propertyName, attemptedValue, greaterThan);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(0m, rule.GetStoredValue<decimal>());
    }

    [Fact]
    public void GreaterThanRule_WithInvalidLongInput_ValidationFails()
    {
        const string propertyName = "Id";
        const long attemptedValue = 5L;
        const long greaterThan = 10L;

        // Act
        var rule = ValidationRules.GreaterThanRule(propertyName, attemptedValue, greaterThan);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(0L, rule.GetStoredValue<long>());
    }

    [Fact]
    public void GreaterThanRule_WithValidFloatInput_ValidationPasses()
    {
        const string propertyName = "Id";
        const float attemptedValue = 25f;
        const float greaterThan = 10f;

        // Act
        var rule = ValidationRules.GreaterThanRule(propertyName, attemptedValue, greaterThan);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(0m, rule.GetStoredValue<decimal>());
    }

    [Fact]
    public void GreaterThanRule_WithInvalidFloatInput_ValidationFails()
    {
        const string propertyName = "Id";
        const float attemptedValue = 5f;
        const float greaterThan = 10f;

        // Act
        var rule = ValidationRules.GreaterThanRule(propertyName, attemptedValue, greaterThan);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(0f, rule.GetStoredValue<float>());
    }

    [Fact]
    public void GreaterThanRule_WithValidDoubleInput_ValidationPasses()
    {
        const string propertyName = "Id";
        const double attemptedValue = 25d;
        const double greaterThan = 10d;

        // Act
        var rule = ValidationRules.GreaterThanRule(propertyName, attemptedValue, greaterThan);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(0m, rule.GetStoredValue<decimal>());
    }

    [Fact]
    public void GreaterThanRule_WithInvalidDoubleInput_ValidationFails()
    {
        const string propertyName = "Id";
        const double attemptedValue = 5d;
        const double greaterThan = 10d;

        // Act
        var rule = ValidationRules.GreaterThanRule(propertyName, attemptedValue, greaterThan);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(0d, rule.GetStoredValue<double>());
    }

    [Fact]
    public void GreaterThanRule_WithValidDecimalInput_ValidationPasses()
    {
        const string propertyName = "Id";
        const decimal attemptedValue = 25m;
        const decimal greaterThan = 10m;

        // Act
        var rule = ValidationRules.GreaterThanRule(propertyName, attemptedValue, greaterThan);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(0m, rule.GetStoredValue<decimal>());
    }

    [Fact]
    public void GreaterThanRule_WithInvalidDecimalInput_ValidationFails()
    {
        const string propertyName = "Id";
        const decimal attemptedValue = 5m;
        const decimal greaterThan = 10m;

        // Act
        var rule = ValidationRules.GreaterThanRule(propertyName, attemptedValue, greaterThan);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(0m, rule.GetStoredValue<int>());
    }
}