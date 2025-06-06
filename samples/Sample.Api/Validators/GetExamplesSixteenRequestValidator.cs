using Ogu.Response;
using Ogu.Response.Abstractions;
using Sample.Api.Models.Requests;
using Sample.Api.Models.ValidatedRequests;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Api.Validators;

public class GetExamplesSixteenRequestValidator : IValidator<GetExamplesSixteenRequest, ValidatedGetExamplesSixteenRequest>
{
    public ValueTask<ValidatedGetExamplesSixteenRequest> ValidateAsync(GetExamplesSixteenRequest request, CancellationToken cancellationToken = default)
    {
        var hashSetRule = ValidationRules.ValidHashSetRule<int>(nameof(request.Ids), request.Ids);

        if (hashSetRule.IsFailed())
        {
            return new ValueTask<ValidatedGetExamplesSixteenRequest>(new ValidatedGetExamplesSixteenRequest([hashSetRule.Failure]));
        }

        var storedIds = hashSetRule.GetStoredValue<HashSet<int>>();

        return new ValueTask<ValidatedGetExamplesSixteenRequest>(new ValidatedGetExamplesSixteenRequest
        {
            ParsedIds = storedIds
        });
    }
}