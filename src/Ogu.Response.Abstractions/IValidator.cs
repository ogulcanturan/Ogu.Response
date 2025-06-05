using System.Threading;
using System.Threading.Tasks;

namespace Ogu.Response.Abstractions
{
    /// <summary>
    /// Defines a contract for validating input and returning a validated model.
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// Validates the input and returns a validated model.
        /// </summary>
        /// <typeparam name="TInput">The type of the input object to validate.</typeparam>
        /// <typeparam name="TValidated">The type of the validated model to return.</typeparam>
        /// <param name="input">The input object to be validated.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous validation operation.</param>
        /// <returns></returns>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
        ValueTask
#else
        Task
#endif
           <TValidated> ValidateAsync<TInput, TValidated>(TInput input, CancellationToken cancellationToken = default) where TValidated : IValidated;

        /// <summary>
        /// Validates the input within a scoped context and returns a validated model.
        /// </summary>
        /// <typeparam name="TInput">The type of the input object to validate.</typeparam>
        /// <typeparam name="TValidated">The type of the validated model to return.</typeparam>
        /// <param name="input">The input object to be validated.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous validation operation.</param>
        /// <returns></returns>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
        ValueTask
#else
        Task
#endif
            <TValidated> ValidateScopedAsync<TInput, TValidated>(TInput input, CancellationToken cancellationToken = default) where TValidated : IValidated;
    }

    /// <summary>
    /// Defines a validator that validates an input of type <typeparamref name="TInput"/> 
    /// and produces a validated model of type <typeparamref name="TValidated"/>.
    /// </summary>
    /// <typeparam name="TInput">The type of the input object to be validated.</typeparam>
    /// <typeparam name="TValidated"> The type representing the result of the validation. Must implement <see cref="IValidated" />.
    /// </typeparam>
    public interface IValidator<in TInput, TValidated> where TValidated : IValidated
    {
        /// <summary>
        /// Validates the input and returns a validated model.
        /// </summary>
        /// <typeparam name="TInput">The type of the input object to validate.</typeparam>
        /// <typeparam name="TValidated">The type of the validated model to return.</typeparam>
        /// <param name="input">The input object to be validated.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous validation operation.</param>
        /// <returns></returns>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
        ValueTask
#else
        Task
#endif
            <TValidated> ValidateAsync(TInput input, CancellationToken cancellationToken = default);
    }
}