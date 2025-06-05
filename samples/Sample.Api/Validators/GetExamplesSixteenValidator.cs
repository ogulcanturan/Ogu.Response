using Ogu.Response;
using Ogu.Response.Abstractions;
using Sample.Api.Models.Requests;
using Sample.Api.Models.Validated;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Api.Validators;

public class GetExamplesSixteenValidator : IValidator<GetExamplesSixteenRequest, ValidatedGetExamplesSixteen>
{
    public ValueTask<ValidatedGetExamplesSixteen> ValidateAsync(GetExamplesSixteenRequest request, CancellationToken cancellationToken = default)
    {
        var hashSetRule = ValidationRules.ValidHashSetRule<int>(nameof(request.Ids), request.Ids);

        if (hashSetRule.IsFailed())
        {
            return new ValueTask<ValidatedGetExamplesSixteen>(new ValidatedGetExamplesSixteen([hashSetRule.Failure]));
        }

        var storedIds = hashSetRule.GetStoredValue<HashSet<int>>();

        return new ValueTask<ValidatedGetExamplesSixteen>(new ValidatedGetExamplesSixteen
        {
            ParsedIds = storedIds
        });
    }
}