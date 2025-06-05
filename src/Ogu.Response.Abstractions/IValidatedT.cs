using System.Collections.Generic;

namespace Ogu.Response.Abstractions
{
    /// <summary>
    /// Represents the result of a validation operation, including any validation failures.
    /// </summary>
    public interface IValidated
    {
        /// <summary>
        /// Gets the list of validation failures.
        /// </summary>
        List<IValidationFailure> Failures { get; }

        /// <summary>
        /// Gets a value indicating whether the validation has failed.
        /// </summary>
        bool HasFailed { get; }
    }
}