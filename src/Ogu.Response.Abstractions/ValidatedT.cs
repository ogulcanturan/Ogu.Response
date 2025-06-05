using System.Collections.Generic;

namespace Ogu.Response.Abstractions
{
    public abstract class Validated<TRequest> : IValidated<TRequest>
    {
        protected Validated(TRequest request, List<IValidationFailure> failures)
        {
            Request = request;
            Failures = failures ?? new List<IValidationFailure>();
            HasFailed = Failures.Count > 0;
        }

        public TRequest Request { get; }

        public List<IValidationFailure> Failures { get; }

        public bool HasFailed { get; }
    }
}