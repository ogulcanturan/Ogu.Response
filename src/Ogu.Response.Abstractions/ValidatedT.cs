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
        /// <summary>
        /// Initializes a new instance of the <see cref="Validated"/> class with the specified validation failures.
        /// </summary>
        /// <param name="failures">A list of validation failures. If <c>null</c>, an empty list is used.</param>
        protected Validated(List<IValidationFailure> failures)
        {
            Failures = failures ?? new List<IValidationFailure>();
        }
 
        public List<IValidationFailure> Failures { get; }
     
        public bool HasFailed => Failures.Count > 0;
    }
}