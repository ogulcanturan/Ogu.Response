using Ogu.Response.Abstractions;
using System.Collections.Generic;

namespace Sample.Api.Models.ValidatedRequests;

public class ValidatedCreateProductRequest(List<IValidationFailure> failures = null) : Validated(failures)
{
    public string Name { get; init; }

    public decimal ParsedPrice { get; init; }

    public ProductCategory ParsedCategory { get; init; }

    public string Sku { get; init; }
}

public enum ProductCategory
{
    Electronics = 1,
    Clothing = 2,
    Food = 3
}