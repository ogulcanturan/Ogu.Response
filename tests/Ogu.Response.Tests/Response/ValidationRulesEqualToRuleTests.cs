using Ogu.Response;

namespace Unit.Tests.Response;

public class ValidationRulesEqualToRuleTests
{
    [Fact]
    public void EqualToRule_WhenEqualToStringShortInput_ValidationPasses()
    {
        const string propertyName = "Id";
        const string attemptedValue = "25";
        const short expectedValue = 25;

        // Act
        var rule = ValidationRules.EqualToRule(propertyName, attemptedValue, expectedValue);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(expectedValue, rule.GetStoredValue<short>());
    }

    [Fact]
    public void EqualToRule_WhenNotEqualToStringShortInput_ValidationFails()
    {
        const string propertyName = "Id";
        const string attemptedValue = "a";
        const short equalTo = 10;

        // Act
        var rule = ValidationRules.EqualToRule(propertyName, attemptedValue, equalTo);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal((short)0, rule.GetStoredValue<short>());
    }

    [Fact]
    public void EqualToRule_WhenEqualToStringIntInput_ValidationPasses()
    {
        const string propertyName = "Id";
        const string attemptedValue = "25";
        const int  expectedValue = 25;

        // Act
        var rule = ValidationRules.EqualToRule(propertyName, attemptedValue, expectedValue);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(expectedValue, rule.GetStoredValue<int>());
    }

    [Fact]
    public void EqualToRule_WhenNotEqualToStringIntInput_ValidationFails()
    {
        const string propertyName = "Id";
        const string attemptedValue = "a";
        const int equalTo = 10;

        // Act
        var rule = ValidationRules.EqualToRule(propertyName, attemptedValue, equalTo);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(0, rule.GetStoredValue<int>());
    }

    [Fact]
    public void EqualToRule_WhenEqualToStringLongInput_ValidationPasses()
    {
        const string propertyName = "Id";
        const string attemptedValue = "25";
        const long expectedValue = 25;

        // Act
        var rule = ValidationRules.EqualToRule(propertyName, attemptedValue, expectedValue);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(expectedValue, rule.GetStoredValue<long>());
    }

    [Fact]
    public void EqualToRule_WhenNotEqualToStringLongInput_ValidationFails()
    {
        const string propertyName = "Id";
        const string attemptedValue = "a";
        const long equalTo = 10L;

        // Act
        var rule = ValidationRules.EqualToRule(propertyName, attemptedValue, equalTo);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(0L, rule.GetStoredValue<long>());
    }

    [Fact]
    public void EqualToRule_WhenEqualToStringFloatInput_ValidationPasses()
    {
        const string propertyName = "Id";
        const string attemptedValue = "25";
        const float expectedValue = 25f;

        // Act
        var rule = ValidationRules.EqualToRule(propertyName, attemptedValue, expectedValue);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(expectedValue, rule.GetStoredValue<float>());
    }

    [Fact]
    public void EqualToRule_WhenNotEqualToStringFloatInput_ValidationFails()
    {
        const string propertyName = "Id";
        const string attemptedValue = "a";
        const float equalTo = 10f;

        // Act
        var rule = ValidationRules.EqualToRule(propertyName, attemptedValue, equalTo);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(0f, rule.GetStoredValue<float>());
    }

    [Fact]
    public void EqualToRule_WhenEqualToStringDoubleInput_ValidationPasses()
    {
        const string propertyName = "Id";
        const string attemptedValue = "25";
        const double expectedValue = 25d;

        // Act
        var rule = ValidationRules.EqualToRule(propertyName, attemptedValue, expectedValue);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(expectedValue, rule.GetStoredValue<double>());
    }

    [Fact]
    public void EqualToRule_WhenNotEqualToStringDoubleInput_ValidationFails()
    {
        const string propertyName = "Id";
        const string attemptedValue = "a";
        const double equalTo = 10d;

        // Act
        var rule = ValidationRules.EqualToRule(propertyName, attemptedValue, equalTo);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(0d, rule.GetStoredValue<double>());
    }

    [Fact]
    public void EqualToRule_WhenEqualToStringDecimalInput_ValidationPasses()
    {
        const string propertyName = "Id";
        const string attemptedValue = "25";
        const decimal expectedValue = 25m;

        // Act
        var rule = ValidationRules.EqualToRule(propertyName, attemptedValue, expectedValue);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(expectedValue, rule.GetStoredValue<decimal>());
    }

    [Fact]
    public void EqualToRule_WhenNotEqualToStringDecimalInput_ValidationFails()
    {
        const string propertyName = "Id";
        const string attemptedValue = "a";
        const decimal equalTo = 10m;

        // Act
        var rule = ValidationRules.EqualToRule(propertyName, attemptedValue, equalTo);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
        Assert.Equal(0m, rule.GetStoredValue<decimal>());
    }




    [Fact]
    public void EqualToRule_WhenEqualToShortInput_ValidationPasses()
    {
        const string propertyName = "Id";
        const short attemptedValue = 25;
        const short expectedValue = 25;

        // Act
        var rule = ValidationRules.EqualToRule(propertyName, attemptedValue, expectedValue);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
    }

    [Fact]
    public void EqualToRule_WhenNotEqualToShortInput_ValidationFails()
    {
        const string propertyName = "Id";
        const short attemptedValue = 1;
        const short equalTo = 10;

        // Act
        var rule = ValidationRules.EqualToRule(propertyName, attemptedValue, equalTo);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
    }

    [Fact]
    public void EqualToRule_WhenEqualToIntInput_ValidationPasses()
    {
        const string propertyName = "Id";
        const int attemptedValue = 25;
        const int expectedValue = 25;

        // Act
        var rule = ValidationRules.EqualToRule(propertyName, attemptedValue, expectedValue);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
    }

    [Fact]
    public void EqualToRule_WhenNotEqualToIntInput_ValidationFails()
    {
        const string propertyName = "Id";
        const int attemptedValue = 1;
        const int equalTo = 10;

        // Act
        var rule = ValidationRules.EqualToRule(propertyName, attemptedValue, equalTo);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
    }

    [Fact]
    public void EqualToRule_WhenEqualToLongInput_ValidationPasses()
    {
        const string propertyName = "Id";
        const long attemptedValue = 25;
        const long expectedValue = 25;

        // Act
        var rule = ValidationRules.EqualToRule(propertyName, attemptedValue, expectedValue);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
    }

    [Fact]
    public void EqualToRule_WhenNotEqualToLongInput_ValidationFails()
    {
        const string propertyName = "Id";
        const long attemptedValue = 5L;
        const long equalTo = 10L;

        // Act
        var rule = ValidationRules.EqualToRule(propertyName, attemptedValue, equalTo);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
    }

    [Fact]
    public void EqualToRule_WhenEqualToFloatInput_ValidationPasses()
    {
        const string propertyName = "Id";
        const float attemptedValue = 25f;
        const float expectedValue = 25f;

        // Act
        var rule = ValidationRules.EqualToRule(propertyName, attemptedValue, expectedValue);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
    }

    [Fact]
    public void EqualToRule_WhenNotEqualToFloatInput_ValidationFails()
    {
        const string propertyName = "Id";
        const float attemptedValue = 5f;
        const float equalTo = 10f;

        // Act
        var rule = ValidationRules.EqualToRule(propertyName, attemptedValue, equalTo);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
    }

    [Fact]
    public void EqualToRule_WhenEqualToDoubleInput_ValidationPasses()
    {
        const string propertyName = "Id";
        const double attemptedValue = 25d;
        const double expectedValue = 25d;

        // Act
        var rule = ValidationRules.EqualToRule(propertyName, attemptedValue, expectedValue);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
    }

    [Fact]
    public void EqualToRule_WhenNotEqualToDoubleInput_ValidationFails()
    {
        const string propertyName = "Id";
        const double attemptedValue = 5d;
        const double equalTo = 10d;

        // Act
        var rule = ValidationRules.EqualToRule(propertyName, attemptedValue, equalTo);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
    }

    [Fact]
    public void EqualToRule_WhenEqualToDecimalInput_ValidationPasses()
    {
        const string propertyName = "Id";
        const decimal attemptedValue = 25m;
        const decimal expectedValue = 25m;

        // Act
        var rule = ValidationRules.EqualToRule(propertyName, attemptedValue, expectedValue);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
    }

    [Fact]
    public void EqualToRule_WhenNotEqualToDecimalInput_ValidationFails()
    {
        const string propertyName = "Id";
        const decimal attemptedValue = 5m;
        const decimal equalTo = 10m;

        // Act
        var rule = ValidationRules.EqualToRule(propertyName, attemptedValue, equalTo);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
    }
}