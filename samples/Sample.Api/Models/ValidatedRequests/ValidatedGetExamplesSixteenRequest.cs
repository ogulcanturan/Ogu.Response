using Ogu.Response.Abstractions;
using System.Collections.Generic;

namespace Sample.Api.Models.ValidatedRequests;

public class ValidatedGetExamplesSixteenRequest(List<IValidationFailure> failures = null) : Validated(failures)
{
    public HashSet<int> ParsedIds { get; init; } = [];
}