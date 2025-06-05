using System.Collections.Generic;

namespace Ogu.Response.Abstractions
{
    public interface IValidated<out TRequest>
    {
        TRequest Request { get; }

        List<IValidationFailure> Failures { get; }

        bool HasFailed { get; }
    }
}