using Ogu.Response;
using Ogu.Response.Abstractions;
using System.Text.Json;

namespace Unit.Tests.Response;

public class ValidationRulesTests
{
    [Fact]
    public void ValidJsonRule_WhenValidJsonString_WithStoreJsonDocument_ValidationPasses()
    {
        const string propertyName = "Id";
        const string attemptedValue = "{\"key\":\"id\"}";

        // Act
        var rule = ValidationRules.ValidJsonRule(propertyName, attemptedValue);
        var jsonDocument = rule?.GetStoredValue<JsonDocument>();

        // Assert
        Assert.NotNull(rule);
        Assert.Null(jsonDocument);
        Assert.False(rule.IsFailed());

        jsonDocument = rule.GetStoredValue<JsonDocument>();

        Assert.NotNull(jsonDocument);
     
        jsonDocument.Dispose();
    }

    [Fact]
    public void ValidJsonRule_WhenInvalidJsonString_WithStoreJsonDocument_ValidationFails()
    {
        const string propertyName = "Id";
        const string attemptedValue = "{";

        // Act
        var rule = ValidationRules.ValidJsonRule(propertyName, attemptedValue);
        var jsonDocument = rule?.GetStoredValue<JsonDocument>();

        // Assert
        Assert.NotNull(rule);
        Assert.Null(jsonDocument);
        Assert.True(rule.IsFailed());

        jsonDocument = rule.GetStoredValue<JsonDocument>();

        Assert.Null(jsonDocument);
    }

    [Fact]
    public void ValidEnumRule_WhenValidEnumInput_ValidationPasses()
    {
        const Sample sample = Sample.First;

        // Act
        var rule = ValidationRules.ValidEnumRule<Sample>(nameof(Sample), sample);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(sample, rule.GetStoredValue<Sample>());
    }

    [Fact]
    public void ValidEnumRule_WhenInvalidEnumInput_ValidationFails()
    {
        const DifferentSample sample = DifferentSample.Fourth;

        // Act
        var rule = ValidationRules.ValidEnumRule<Sample>(nameof(DifferentSample), sample);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
    }

    [Fact]
    public void ValidEnumRule_WhenValidStringInput_ValidationPasses()
    {
        const Sample sample = Sample.Second;

        // Act
        var rule = ValidationRules.ValidEnumRule<Sample>(nameof(Sample), "Second");

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(sample, rule.GetStoredValue<Sample>());
    }

    [Fact]
    public void ValidEnumRule_WhenValidLowercaseStringInput_ValidationPasses()
    {
        const Sample sample = Sample.Second;

        // Act
        var rule = ValidationRules.ValidEnumRule<Sample>(nameof(Sample), "second");

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(sample, rule.GetStoredValue<Sample>());
    }

    [Fact]
    public void ValidEnumRule_WhenInvalidStringInput_ValidationFails()
    {
        // Act
        var rule = ValidationRules.ValidEnumRule<Sample>(nameof(Sample), "Fourth");

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
    }

    [Fact]
    public void ValidEnumRule_WhenValidIntInput_ValidationPasses()
    {
        const Sample sample = Sample.Third;

        // Act
        var rule = ValidationRules.ValidEnumRule<Sample>(nameof(Sample), 2);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(sample, rule.GetStoredValue<Sample>());
    }

    [Fact]
    public void ValidEnumRule_WhenInvalidIntInput_ValidationFails()
    {
        const Sample sample = Sample.Third;

        // Act
        var rule = ValidationRules.ValidEnumRule<Sample>(nameof(Sample), 3);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
    }

    [Fact]
    public void ValidBooleanRule_WhenValidStringInput_ValidationPasses()
    {
        const string propertyName = "IsActive";
        const string attemptedValue = "True";

        // Act
        var rule = ValidationRules.ValidBooleanRule(propertyName, attemptedValue);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.True(rule.GetStoredValue<bool>());
    }

    [Fact]
    public void ValidBooleanRule_WhenValidLowercaseStringInput_ValidationPasses()
    {
        const string propertyName = "IsActive";
        const string attemptedValue = "true";

        // Act
        var rule = ValidationRules.ValidBooleanRule(propertyName, attemptedValue);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.True(rule.GetStoredValue<bool>());
    }

    [Fact]
    public void ValidBooleanRule_WhenInvalidStringInput_ValidationFails()
    {
        const string propertyName = "IsActive";
        const string attemptedValue = "Trues";

        // Act
        var rule = ValidationRules.ValidBooleanRule(propertyName, attemptedValue);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.False(rule.GetStoredValue<bool>());
    }

    [Fact]
    public void ValidNumberRule_WhenValidStringInput_ValidationPasses()
    {
        const string propertyName = "Age";
        const string attemptedValue = "25";

        // Act
        var rule = ValidationRules.ValidNumberRule(propertyName, attemptedValue);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(25L, rule.GetStoredValue<long>());
        Assert.Equal(0, rule.GetStoredValue<int>());
    }

    [Fact]
    public void ValidNumberRule_WhenInvalidStringInput_ValidationFails()
    {
        const string propertyName = "Age";
        const string attemptedValue = "25A";

        // Act
        var rule = ValidationRules.ValidNumberRule(propertyName, attemptedValue);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());
        Assert.Equal(0L, rule.GetStoredValue<long>());
    }

    [Fact]
    public void ValidNumberRule_WhenValidDoubleStringInput_ValidationPasses()
    {
        const string propertyName = "Age";
        const string attemptedValue = "25.25";

        // Act
        var rule = ValidationRules.ValidNumberRule(propertyName, attemptedValue);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());
        Assert.Equal(25.25m, rule.GetStoredValue<decimal>());
        Assert.Equal(0L, rule.GetStoredValue<long>());
    }

    [Fact]
    public void ValidHashSetRule_WhenValidInput_ValidationPasses()
    {
        const string propertyName = "Ids";
        const string attemptedValue = "1,2,3";
        var expectedCollection = new HashSet<int> { 1, 2, 3 };

        // Act
        var rule = ValidationRules.ValidHashSetRule<int>(propertyName, attemptedValue);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());

        var storedValue = rule.GetStoredValue<HashSet<int>>();

        Assert.NotNull(storedValue);
        Assert.Equal(3, storedValue.Count);
        Assert.All(storedValue, i => Assert.Contains(i, expectedCollection));
    }

    [Fact]
    public void ValidHashSetRule_WhenInvalidInput_ValidationFails()
    {
        const string propertyName = "Ids";
        const string attemptedValue = "1,2,3,a";

        // Act
        var rule = ValidationRules.ValidHashSetRule<int>(propertyName, attemptedValue);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());

        var storedValue = rule.GetStoredValue<HashSet<int>>();

        Assert.Null(storedValue);
    }

    [Fact]
    public void ValidHashSetRule_WhenValidInput_WithOrdinalIgnoreCaseComparer_ValidationPasses()
    {
        const string propertyName = "Ids";
        const string attemptedValue = "a,B,B";
        var expectedCollection = new HashSet<string> { "a", "B" };

        // Act
        var rule = ValidationRules.ValidHashSetRule<string>(propertyName, attemptedValue, StringComparer.OrdinalIgnoreCase);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());

        var storedValue = rule.GetStoredValue<HashSet<string>>();

        Assert.NotNull(storedValue);
        Assert.Equal(2, storedValue.Count);
        Assert.All(storedValue, i => Assert.Contains(i, expectedCollection));
    }

    [Fact]
    public void ValidHashSetRule_WhenValidInput_WithColumnSeparator_ValidationPasses()
    {
        const string propertyName = "Ids";
        const string attemptedValue = "1:2:3";
        var expectedCollection = new HashSet<int> { 1, 2, 3 };
        char[] separators = [':'];

        // Act
        var rule = ValidationRules.ValidHashSetRule<int>(propertyName, attemptedValue, separators);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());

        var storedValue = rule.GetStoredValue<HashSet<int>>();

        Assert.NotNull(storedValue);
        Assert.Equal(3, storedValue.Count);
        Assert.All(storedValue, i => Assert.Contains(i, expectedCollection));
    }

    [Fact]
    public void ValidHashSetRule_WhenValidInput_WithOrdinalIgnoreCaseComparerAndColumnSeparator_ValidationPasses()
    {
        const string propertyName = "Ids";
        const string attemptedValue = "a:B:B";
        var expectedCollection = new HashSet<string> { "a", "B" };
        char[] separators = [':'];

        // Act
        var rule = ValidationRules.ValidHashSetRule<string>(propertyName, attemptedValue, StringComparer.OrdinalIgnoreCase, separators);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());

        var storedValue = rule.GetStoredValue<HashSet<string>>();

        Assert.NotNull(storedValue);
        Assert.Equal(2, storedValue.Count);
        Assert.All(storedValue, i => Assert.Contains(i, expectedCollection));
    }

    public static IEnumerable<object[]> EmptyInputs =>
        new List<object[]>
        {
            new object[] { null },
            new object[] { "" },
            new object[] { " " },
            new object[] { "   " },
        };

    [Theory]
    [MemberData(nameof(EmptyInputs))]
    public void ValidHashSetRule_WhenEmptyInputs_WithHashSetRuleOptionsAllowEmptyTrue_ValidationPasses(string? attemptedValue)
    {
        const string propertyName = "Ids";
        var hashSetRuleOptions = new HashSetRuleOptions(allowEmpty: true, requireAllUnique: false, continueOnFailure: false);

        // Act
        var rule = ValidationRules.ValidHashSetRule<string>(propertyName, attemptedValue, hashSetRuleOptions);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());

        var storedValue = rule.GetStoredValue<HashSet<string>>();

        Assert.NotNull(storedValue);
        Assert.Empty(storedValue);
    }

    [Theory]
    [MemberData(nameof(EmptyInputs))]
    public void ValidHashSetRule_WhenEmptyInputs_WithHashSetRuleOptionsAllowEmptyFalse_ValidationFails(string? attemptedValue)
    {
        const string propertyName = "Ids";
        var hashSetRuleOptions = new HashSetRuleOptions(allowEmpty: false, requireAllUnique: false, continueOnFailure: false);

        // Act
        var rule = ValidationRules.ValidHashSetRule<string>(propertyName, attemptedValue, hashSetRuleOptions);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());

        var storedValue = rule.GetStoredValue<HashSet<string>>();

        Assert.Null(storedValue);
    }

    [Fact]
    public void ValidHashSetRule_WhenValidData_WithHashSetRuleOptionsRequireAllUniqueFalse_ValidationPasses()
    {
        const string propertyName = "Ids";
        const string attemptedValue = "1,2,2";
        var expectedCollection = new HashSet<string> { "1", "2" };
        var hashSetRuleOptions = new HashSetRuleOptions(allowEmpty: false, requireAllUnique: false, continueOnFailure: false);

        // Act
        var rule = ValidationRules.ValidHashSetRule<string>(propertyName, attemptedValue, hashSetRuleOptions);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());

        var storedValue = rule.GetStoredValue<HashSet<string>>();

        Assert.NotNull(storedValue);
        Assert.Equal(2, storedValue.Count);
        Assert.All(storedValue, i => Assert.Contains(i, expectedCollection));
    }

    [Fact]
    public void ValidHashSetRule_WhenValidData_WithHashSetRuleOptionsRequireAllUniqueTrue_ValidationPasses()
    {
        const string propertyName = "Ids";
        const string attemptedValue = "1,2";
        var expectedCollection = new HashSet<string> { "1", "2" };
        var hashSetRuleOptions = new HashSetRuleOptions(allowEmpty: false, requireAllUnique: true, continueOnFailure: false);

        // Act
        var rule = ValidationRules.ValidHashSetRule<string>(propertyName, attemptedValue, hashSetRuleOptions);

        // Assert
        Assert.NotNull(rule);
        Assert.False(rule.IsFailed());

        var storedValue = rule.GetStoredValue<HashSet<string>>();

        Assert.NotNull(storedValue);
        Assert.Equal(2, storedValue.Count);
        Assert.All(storedValue, i => Assert.Contains(i, expectedCollection));
    }

    [Fact]
    public void ValidHashSetRule_WhenValidData_WithHashSetRuleOptionsRequireAllUniqueTrue_ValidationFails()
    {
        const string propertyName = "Ids";
        const string attemptedValue = "1,2,2";
        var hashSetRuleOptions = new HashSetRuleOptions(allowEmpty: false, requireAllUnique: true, continueOnFailure: false);

        // Act
        var rule = ValidationRules.ValidHashSetRule<string>(propertyName, attemptedValue, hashSetRuleOptions);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());

        var storedValue = rule.GetStoredValue<HashSet<string>>();

        Assert.Null(storedValue);
    }

    [Fact]
    public void ValidHashSetRule_WhenValidData_WithHashSetRuleOptionsContinueOnFailureTrue_ValidationFails()
    {
        const string propertyName = "Ids";
        const string attemptedValue = "1,2,2a";
        var expectedCollection = new HashSet<int> { 1, 2 };
        var hashSetRuleOptions = new HashSetRuleOptions(allowEmpty: false, requireAllUnique: false, continueOnFailure: true);

        // Act
        var rule = ValidationRules.ValidHashSetRule<int>(propertyName, attemptedValue, hashSetRuleOptions);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());

        var storedValue = rule.GetStoredValue<HashSet<int>>();

        Assert.NotNull(storedValue);
        Assert.Equal(2, storedValue.Count);
        Assert.All(storedValue, i => Assert.Contains(i, expectedCollection));
    }

    [Fact]
    public void ValidHashSetRule_WhenValidData_WithHashSetRuleOptionsContinueOnFailureFalse_ValidationFails()
    {
        const string propertyName = "Ids";
        const string attemptedValue = "1,2,2a";
        var hashSetRuleOptions = new HashSetRuleOptions(allowEmpty: false, requireAllUnique: false, continueOnFailure: false);

        // Act
        var rule = ValidationRules.ValidHashSetRule<int>(propertyName, attemptedValue, hashSetRuleOptions);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule.IsFailed());

        var storedValue = rule.GetStoredValue<HashSet<int>>();

        Assert.Null(storedValue);
    }

    public enum Sample
    {
        First,
        Second,
        Third
    }

    public enum DifferentSample
    {
        Fourth
    }
}