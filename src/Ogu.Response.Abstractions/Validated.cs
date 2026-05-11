using System.Collections.Generic;

namespace Ogu.Response.Abstractions
{
    /// <summary>
    /// Represents the base class for validation results containing failure information.
    /// </summary>
    /// <remarks>
    /// Intended to be inherited by domain-specific validated models.
    /// </remarks>
    public abstract class Validated : IValidated
    {
        private List<IValidationFailure> _failures;

        /// <summary>
        /// Initializes a new instance of the <see cref="Validated"/> class with the specified validation failures.
        /// </summary>
        /// <param name="failures">A list of validation failures. If <c>null</c>, the list will be lazily initialized when accessed.</param>
        protected Validated(List<IValidationFailure> failures)
        {
            _failures = failures;
        }

        public List<IValidationFailure> Failures
        {
            get { return _failures ?? (_failures = new List<IValidationFailure>()); }
        }

        public bool HasFailed => _failures != null && _failures.Count > 0;
    }
}