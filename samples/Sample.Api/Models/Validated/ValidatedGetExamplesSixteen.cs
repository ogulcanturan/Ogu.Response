using Ogu.Response.Abstractions;
using Sample.Api.Models.Requests;
using System.Collections.Generic;

namespace Sample.Api.Models.Validated;

public class ValidatedGetExamplesSixteen(GetExamplesSixteenRequest request, List<IValidationFailure> failures = null) : Validated<GetExamplesSixteenRequest>(request, failures)
{
    public HashSet<int> ParsedIds { get; init; } = [];
}