using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ogu.Response.Abstractions
{
    /// <summary>
    /// Represents a validation rule that checks a condition to determine if a failure is present.
    /// Supports both synchronous and asynchronous condition evaluation.
    /// </summary>
    public class ValidationRule : IValidationRule, IValidationStore
    {
        private readonly Func<IValidationFailure> _createFailure;
        private readonly Func<IValidationStore, bool> _syncCondition;
        private readonly Func<IValidationStore, CancellationToken,
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
        ValueTask
#else
       Task
#endif
       <bool>> _asyncCondition;

        private bool? _hasFailed;
        private object _storedValue;
        private IValidationFailure _failure;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRule"/> class with a synchronous condition.
        /// </summary>
        /// <param name="failure">The validation failure information to be used if the condition fails.</param>
        /// <param name="condition">
        /// A synchronous function that returns <c>false</c> if the condition fails, or <c>true</c> if it succeeds.
        /// This function determines if the rule has failed validation.
        /// </param>
        /// <remarks>Validation <c>passes</c> when the <c>condition</c> returns <c>true</c>; otherwise, <c>false</c>.</remarks>
        public ValidationRule(IValidationFailure failure, Func<bool> condition)
        {
            _failure = failure;
            _syncCondition = v => condition();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRule"/> class with a synchronous condition,
        /// allowing storage of relevant values during condition evaluation.
        /// </summary>
        /// <param name="failure">The validation failure information to use if the condition fails.</param>
        /// <param name="condition">
        /// A synchronous function that takes an <see cref="IValidationStore"/> instance and returns <c>false</c> if 
        /// the condition fails, or <c>true</c> if it succeeds. The <see cref="IValidationStore"/> can be used to store
        /// intermediate values or results calculated during validation for potential use in later operations.
        /// This function determines if the rule has failed validation.
        /// </param>
        /// <remarks>Validation <c>passes</c> when the <c>condition</c> returns <c>true</c>; otherwise, <c>false</c>.</remarks>
        public ValidationRule(IValidationFailure failure, Func<IValidationStore, bool> condition)
        {
            _failure = failure;
            _syncCondition = condition;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRule"/> class with an asynchronous condition.
        /// </summary>
        /// <param name="failure">The validation failure information to be used if the condition fails.</param>
        /// <param name="condition">
        /// An asynchronous function that returns <c>false</c> if the condition fails, or <c>true</c> if it succeeds.
        /// This function determines if the rule has failed validation.
        /// </param>
        /// <remarks>Validation <c>passes</c> when the <c>condition</c> returns <c>true</c>; otherwise, <c>false</c>.</remarks>
        public ValidationRule(IValidationFailure failure, Func<
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
            ValueTask
#else
            Task
#endif
            <bool>> condition)
        {
            _failure = failure;
            _asyncCondition = (_, __) => condition();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRule"/> class with an asynchronous condition.
        /// </summary>
        /// <param name="failure">The validation failure information to be used if the condition fails.</param>
        /// <param name="condition">
        /// An asynchronous function that takes an <see cref="CancellationToken" /> object and returns <c>false</c> if the condition fails, or <c>true</c> if it succeeds.
        /// This function determines if the rule has failed validation.
        /// </param>
        /// <remarks>Validation <c>passes</c> when the <c>condition</c> returns <c>true</c>; otherwise, <c>false</c>.</remarks>
        public ValidationRule(IValidationFailure failure, Func<CancellationToken,
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
            ValueTask
#else
            Task
#endif
            <bool>> condition)
        {
            _failure = failure;
            _asyncCondition = (_, ct) => condition(ct);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRule"/> class with an asynchronous condition.
        /// </summary>
        /// <param name="failure">The validation failure information to be used if the condition fails.</param>
        /// <param name="condition">
        /// An asynchronous function that takes an <see cref="IValidationStore"/> instance and returns <c>false</c> if
        /// the condition fails, or <c>true</c> if it succeeds.
        /// This function determines if the rule has failed validation.
        /// </param>
        /// <remarks>Validation <c>passes</c> when the <c>condition</c> returns <c>true</c>; otherwise, <c>false</c>.</remarks>
        public ValidationRule(IValidationFailure failure, Func<IValidationStore,
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
            ValueTask
#else
            Task
#endif
            <bool>> condition)
        {
            _failure = failure;
            _asyncCondition = (vs, _) => condition(vs);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRule"/> class with an asynchronous condition.
        /// </summary>
        /// <param name="failure">The validation failure information to be used if the condition fails.</param>
        /// <param name="condition">
        /// An asynchronous function that takes an <see cref="IValidationStore"/> instance and <see cref="CancellationToken" /> then returns <c>false</c> if
        /// the condition fails, or <c>true</c> if it succeeds.
        /// This function determines if the rule has failed validation.
        /// </param>
        /// <remarks>Validation <c>passes</c> when the <c>condition</c> returns <c>true</c>; otherwise, <c>false</c>.</remarks>
        public ValidationRule(IValidationFailure failure, Func<IValidationStore, CancellationToken,
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
            ValueTask
#else
            Task
#endif
            <bool>> condition)
        {
            _failure = failure;
            _asyncCondition = condition;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRule"/> class with a synchronous condition.
        /// </summary>
        /// <param name="createFailure">A factory function used to create the <see cref="Failure"/> instance when it is accessed.</param>
        /// <param name="condition">
        /// A synchronous function that returns <c>false</c> if the condition fails, or <c>true</c> if it succeeds.
        /// This function determines if the rule has failed validation.
        /// </param>
        /// <remarks>Validation <c>passes</c> when the <c>condition</c> returns <c>true</c>; otherwise, <c>false</c>.</remarks>
        public ValidationRule(Func<IValidationFailure> createFailure, Func<bool> condition)
        {
            _createFailure = createFailure;
            _syncCondition = v => condition();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRule"/> class with a synchronous condition,
        /// allowing storage of relevant values during condition evaluation.
        /// </summary>
        /// <param name="createFailure">A factory function used to create the <see cref="Failure"/> instance when it is accessed.</param>
        /// <param name="condition">
        /// A synchronous function that takes an <see cref="IValidationStore"/> instance and returns <c>false</c> if 
        /// the condition fails, or <c>true</c> if it succeeds. The <see cref="IValidationStore"/> can be used to store
        /// intermediate values or results calculated during validation for potential use in later operations.
        /// This function determines if the rule has failed validation.
        /// </param>
        /// <remarks>Validation <c>passes</c> when the <c>condition</c> returns <c>true</c>; otherwise, <c>false</c>.</remarks>
        public ValidationRule(Func<IValidationFailure> createFailure, Func<IValidationStore, bool> condition)
        {
            _createFailure = createFailure;
            _syncCondition = condition;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRule"/> class with an asynchronous condition.
        /// </summary>
        /// <param name="createFailure">A factory function used to create the <see cref="Failure"/> instance when it is accessed.</param>
        /// <param name="condition">
        /// An asynchronous function that returns <c>false</c> if the condition fails, or <c>true</c> if it succeeds.
        /// This function determines if the rule has failed validation.
        /// </param>
        /// <remarks>Validation <c>passes</c> when the <c>condition</c> returns <c>true</c>; otherwise, <c>false</c>.</remarks>
        public ValidationRule(Func<IValidationFailure> createFailure, Func<
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
            ValueTask
#else
            Task
#endif
            <bool>> condition)
        {
            _createFailure = createFailure;
            _asyncCondition = (_, __) => condition();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRule"/> class with an asynchronous condition.
        /// </summary>
        /// <param name="createFailure">A factory function used to create the <see cref="Failure"/> instance when it is accessed.</param>
        /// <param name="condition">
        /// An asynchronous function that takes an <see cref="CancellationToken" /> object and returns <c>false</c> if the condition fails, or <c>true</c> if it succeeds.
        /// This function determines if the rule has failed validation.
        /// </param>
        /// <remarks>Validation <c>passes</c> when the <c>condition</c> returns <c>true</c>; otherwise, <c>false</c>.</remarks>
        public ValidationRule(Func<IValidationFailure> createFailure, Func<CancellationToken,
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
            ValueTask
#else
            Task
#endif
            <bool>> condition)
        {
            _createFailure = createFailure;
            _asyncCondition = (_, ct) => condition(ct);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRule"/> class with an asynchronous condition.
        /// </summary>
        /// <param name="createFailure">A factory function used to create the <see cref="Failure"/> instance when it is accessed.</param>
        /// <param name="condition">
        /// An asynchronous function that takes an <see cref="IValidationStore"/> instance and returns <c>false</c> if
        /// the condition fails, or <c>true</c> if it succeeds.
        /// This function determines if the rule has failed validation.
        /// </param>
        /// <remarks>Validation <c>passes</c> when the <c>condition</c> returns <c>true</c>; otherwise, <c>false</c>.</remarks>
        public ValidationRule(Func<IValidationFailure> createFailure, Func<IValidationStore,
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
            ValueTask
#else
            Task
#endif
            <bool>> condition)
        {
            _createFailure = createFailure;
            _asyncCondition = (vs, _) => condition(vs);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRule"/> class with an asynchronous condition.
        /// </summary>
        /// <param name="createFailure">A factory function used to create the <see cref="Failure"/> instance when it is accessed.</param>
        /// <param name="condition">
        /// An asynchronous function that takes an <see cref="IValidationStore"/> instance and <see cref="CancellationToken" /> then returns <c>false</c> if
        /// the condition fails, or <c>true</c> if it succeeds.
        /// This function determines if the rule has failed validation.
        /// </param>
        /// <remarks>Validation <c>passes</c> when the <c>condition</c> returns <c>true</c>; otherwise, <c>false</c>.</remarks>
        public ValidationRule(Func<IValidationFailure> createFailure, Func<IValidationStore, CancellationToken,
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
            ValueTask
#else
            Task
#endif
            <bool>> condition)
        {
            _createFailure = createFailure;
            _asyncCondition = condition;
        }

        public IValidationFailure Failure => _failure ?? (_failure = _createFailure?.Invoke());

        public bool IsFailed()
        {
            if (_syncCondition == null)
            {
                return false;
            }

            var isFailed = !_syncCondition.Invoke(this);

            _hasFailed = isFailed;

            return isFailed;
        }

        public async
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
            ValueTask
#else
            Task
#endif
            <bool> IsFailedAsync(CancellationToken cancellationToken = default)
        {
            if (_hasFailed.HasValue)
            {
                return _hasFailed.Value;
            }

            var isFailed = _asyncCondition == null ? !_syncCondition?.Invoke(this) ?? false : !await _asyncCondition.Invoke(this, cancellationToken);

            _hasFailed = isFailed;

            return isFailed;
        }

        public T GetStoredValue<T>()
        {
            return _storedValue is T value ? value : default;
        }

        public void Store(object value)
        {
            _storedValue = value;
        }
    }
}