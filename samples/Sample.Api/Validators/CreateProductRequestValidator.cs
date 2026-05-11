using Ogu.Response;
using Ogu.Response.Abstractions;
using Sample.Api.Models.Requests;
using Sample.Api.Models.ValidatedRequests;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Api.Validators;

/// <summary>
/// Demonstrates advanced validation patterns:
/// - Sync rules first (fail fast, avoid unnecessary I/O)
/// - Async rules second (database/external checks only if sync passed)
/// - Using yield return for conditional sync rules
/// - Using GetStoredValue to carry parsed values into the validated model
/// </summary>
public class CreateProductRequestValidator : IValidator<CreateProductRequest, ValidatedCreateProductRequest>
{
    // Simulates a database of existing SKUs
    private static readonly HashSet<string> ExistingSkus = new HashSet<string> { "SKU-001", "SKU-002", "SKU-003" };

    public async ValueTask<ValidatedCreateProductRequest> ValidateAsync(CreateProductRequest request, CancellationToken cancellationToken = default)
    {
        // Phase 1: Sync validation — cheap checks first, fail fast
        var enumRule = ValidationRules.ValidEnumRule<ProductCategory>(nameof(request.Category), request.Category);
        var priceRule = ValidationRules.GreaterThanRule(nameof(request.Price), request.Price, 0m);

        var syncFailure = GetSyncValidationRules(request, enumRule, priceRule).ValidateFirstOrDefault();

        if (syncFailure != null)
            return new ValidatedCreateProductRequest([syncFailure]);

        // Phase 2: Async validation — only runs if sync passed (avoids unnecessary I/O)
        var skuUniqueRule = new ValidationRule(
            () => ValidationFailures.AlreadyExists(nameof(request.Sku), request.Sku),
            async (store, ct) =>
            {
                // Simulate async database lookup
                await Task.Delay(10, ct);
                return !ExistingSkus.Contains(request.Sku);
            });

        if (await skuUniqueRule.IsFailedAsync(cancellationToken))
            return new ValidatedCreateProductRequest([skuUniqueRule.Failure]);

        // All validations passed — build the validated model with parsed values
        return new ValidatedCreateProductRequest
        {
            Name = request.Name,
            ParsedPrice = priceRule.GetStoredValue<decimal>(),
            ParsedCategory = enumRule.GetStoredValue<ProductCategory>(),
            Sku = request.Sku
        };
    }

    /// <summary>
    /// Sync validation rules using yield return for conditional logic.
    /// Rules are evaluated lazily — if the first rule fails, subsequent rules are never constructed.
    /// </summary>
    private static IEnumerable<ValidationRule> GetSyncValidationRules(
        CreateProductRequest request,
        ValidationRule enumRule,
        ValidationRule priceRule)
    {
        yield return ValidationRules.NotEmptyRule(nameof(request.Name), request.Name);
        yield return ValidationRules.NotEmptyRule(nameof(request.Sku), request.Sku);
        yield return priceRule;
        yield return enumRule;

        // Conditional rules based on the parsed category
        var category = enumRule.GetStoredValue<ProductCategory>();

        if (category == ProductCategory.Food)
        {
            // Food items require a name with at least 3 characters 
            yield return new ValidationRule(
                () => new ValidationFailure(nameof(request.Name), "Food items require a name with at least 3 characters.", request.Name),
                () => request.Name.Length >= 3);
        }
    }
}