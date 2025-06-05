using Ogu.Response.Abstractions;
using System.Collections.Generic;

namespace Sample.Api.Models.Validated;

public class ValidatedGetExamplesSixteen(List<IValidationFailure> failures = null)
    : Ogu.Response.Abstractions.Validated(failures)
{
    public HashSet<int> ParsedIds { get; init; } = [];
}