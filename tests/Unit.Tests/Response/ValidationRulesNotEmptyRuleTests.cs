using Ogu.Response;

namespace Unit.Tests.Response;

public class ValidationRulesNotEmptyRuleTests
{
    [Fact]
    public void NotEmptyRule_WithValidStringInput_ValidationPasses()
    {
        const string propertyName = "Id";
        const string attemptedValue = "5";

        // Act
        var rule = ValidationRules.NotEmptyRule(propertyName, attemptedValue);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
    }

    [Fact]
    public void NotEmptyRule_WithStringEmptyInput_ValidationFails()
    {
        const string propertyName = "Id";
        const string attemptedValue = "";

        // Act
        var rule = ValidationRules.NotEmptyRule(propertyName, attemptedValue);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
    }

    [Fact]
    public void NotEmptyRule_WithStringNullInput_ValidationFails()
    {
        const string propertyName = "Id";
        const string? attemptedValue = null;

        // Act
        var rule = ValidationRules.NotEmptyRule(propertyName, attemptedValue);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
    }

    [Fact]
    public void NotEmptyRule_WithStringEmptySpaceInput_ValidationFails()
    {
        const string propertyName = "Id";
        const string attemptedValue = " ";

        // Act
        var rule = ValidationRules.NotEmptyRule(propertyName, attemptedValue);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
    }

    [Fact]
    public void NotEmptyRule_WithGuidValidInput_ValidationPasses()
    {
        const string propertyName = "Id";
        var attemptedValue = Guid.NewGuid();

        // Act
        var rule = ValidationRules.NotEmptyRule(propertyName, attemptedValue);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
    }

    [Fact]
    public void NotEmptyRule_WithGuidInvalidInput_ValidationFails()
    {
        const string propertyName = "Id";
        var attemptedValue = Guid.Empty;

        // Act
        var rule = ValidationRules.NotEmptyRule(propertyName, attemptedValue);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
    }


    [Fact]
    public void NotEmptyRule_WithNullableGuidValidInput_ValidationPasses()
    {
        const string propertyName = "Id";
        Guid? attemptedValue = Guid.NewGuid();

        // Act
        var rule = ValidationRules.NotEmptyRule(propertyName, attemptedValue);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
    }

    [Fact]
    public void NotEmptyRule_WithNullableGuidEmptyInput_ValidationFails()
    {
        const string propertyName = "Id";
        Guid? attemptedValue = Guid.Empty;

        // Act
        var rule = ValidationRules.NotEmptyRule(propertyName, attemptedValue);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
    }

    [Fact]
    public void NotEmptyRule_WithNullableGuidNullInput_ValidationFails()
    {
        const string propertyName = "Id";
        Guid? attemptedValue = null;

        // Act
        var rule = ValidationRules.NotEmptyRule(propertyName, attemptedValue);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
    }

    [Fact]
    public void NotEmptyRule_WithValidCollection_ValidationPasses()
    {
        const string propertyName = "Ids";
        var attemptedValue = new[] { "1", "2" };

        // Act
        var rule = ValidationRules.NotEmptyRule(propertyName, attemptedValue);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
    }

    [Fact]
    public void NotEmptyRule_WithInvalidEmptyCollection_ValidationFails()
    {
        const string propertyName = "Ids";
        string[] attemptedValue = [];

        // Act
        var rule = ValidationRules.NotEmptyRule(propertyName, attemptedValue);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
    }

    [Fact]
    public void NotEmptyRule_WithInvalidNullCollection_ValidationFails()
    {
        const string propertyName = "Ids";
        string[]? attemptedValue = null;

        // Act
        var rule = ValidationRules.NotEmptyRule(propertyName, attemptedValue);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(propertyName, rule.Failure.PropertyName);
        Assert.Equal(attemptedValue, rule.Failure.AttemptedValue);
    }
}