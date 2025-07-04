﻿using System.Threading;
using System.Threading.Tasks;

namespace Ogu.Response.Abstractions
{
    /// <summary>
    /// Defines a validation rule, which includes the failure details and methods to evaluate
    /// whether the validation condition has failed either synchronously or asynchronously.
    /// </summary>
    public interface IValidationRule
    {
        /// <summary>
        /// Gets the <see cref="IValidationFailure"/> associated with this validation rule.
        /// This provides details of the failure.
        /// </summary>
        IValidationFailure Failure { get; }

        /// <summary>
        /// Checks if the synchronous condition for this rule fails. If a synchronous condition is defined,
        /// it will be invoked and its result cached. For asynchronous conditions, this method does nothing.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the synchronous condition fails; otherwise, <c>false</c>. If no synchronous condition 
        /// is provided, returns <c>false</c> without any evaluation.
        /// </returns>
        bool IsFailed();

        /// <summary>
        /// Checks if the condition for this rule fails. If an asynchronous condition is provided, 
        /// evaluates it asynchronously; otherwise, evaluates the synchronous condition.
        /// Caches the result to prevent repeated evaluations.
        /// </summary>
        /// <returns>
        /// A task representing the asynchronous operation, with a result of <c>true</c> if the 
        /// condition fails; otherwise, <c>false</c>.
        /// <para>
        /// - If the asynchronous condition is provided, it will be awaited and used.
        /// </para>
        /// - If only the synchronous condition is available, it will be used without blocking.
        /// </returns>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
       ValueTask
#else
       Task
#endif
       <bool> IsFailedAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves the stored value of type <typeparamref name="T"/> if available.
        /// <para>
        /// This value should only be accessed after verifying the validation has succeeded,
        /// typically by checking <c>IsFailed</c> or <c>IsFailedAsync</c>.
        /// </para>
        /// </summary>
        /// <typeparam name="T">The type of the value to retrieve.</typeparam>
        /// <returns>
        /// The stored value of type <typeparamref name="T"/> if present; otherwise, the default value of <typeparamref name="T"/>.
        /// </returns>
        T GetStoredValue<T>();
    }
}