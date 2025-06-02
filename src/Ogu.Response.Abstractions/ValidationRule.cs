using System;
using System.Threading.Tasks;

namespace Ogu.Response.Abstractions
{
    /// <summary>
    /// Represents a validation rule that checks a condition to determine if a failure is present.
    /// Supports both synchronous and asynchronous condition evaluation.
    /// </summary>
    public class ValidationRule : IValidationRule, IValidationStore
    {
        private readonly Func<IValidationStore, bool> _syncCondition;
        private readonly Func<IValidationStore,
#if NETSTANDARD2_1_OR_GREATER
            ValueTask
#else
            Task
#endif
            <bool>> _asyncCondition;

        private bool? _hasFailed;
        private object _storedValue;

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
            Failure = failure;
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
            Failure = failure;
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
#if NETSTANDARD2_1_OR_GREATER
            ValueTask
#else
            Task
#endif
            <bool>> condition)
        {
            Failure = failure;
            _asyncCondition = _ => condition();
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
#if NETSTANDARD2_1_OR_GREATER
            ValueTask
#else
            Task
#endif
            <bool>> condition)
        {
            Failure = failure;
            _asyncCondition = condition;
        }

        public IValidationFailure Failure { get; }

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

        public async Task<bool> IsFailedAsync()
        {
            if (_hasFailed.HasValue)
            {
                return _hasFailed.Value;
            }

            var isFailed = _asyncCondition == null ? !_syncCondition?.Invoke(this) ?? false : !await _asyncCondition.Invoke(this);

            _hasFailed = isFailed;

            return isFailed;
        }

        public T GetStoredValue<T>()
        {
            return _storedValue == null ? default : (T)_storedValue;
        }

        public void Store(object value)
        {
            _storedValue = value;
        }
    }
}