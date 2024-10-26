using Ogu.Settings;
using System;
using System.Threading.Tasks;

namespace Ogu.Response.Abstractions
{
    /// <summary>
    ///     Represents a validation rule that checks a condition to determine if a failure is present.
    ///     Supports both synchronous and asynchronous condition evaluation.
    /// </summary>
    public class ValidationRule : IValidationRule, IValidationStore
    {
        private readonly Func<IValidationStore, bool> _syncCondition;
        private readonly Func<IValidationStore, Task<bool>> _asyncCondition;

        private bool? _hasFailed;
        private object _storedValue;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ValidationRule"/> class with a synchronous condition.
        /// </summary>
        /// <param name="validationFailure">The validation failure information to be used if the condition fails.</param>
        /// <param name="condition">
        ///     A synchronous function that returns <c>true</c> if the condition fails, or <c>false</c> if it succeeds.
        ///     This function determines if the rule has failed validation.
        /// </param>
        public ValidationRule(IValidationFailure validationFailure, Func<bool> condition)
        {
            ValidationFailure = validationFailure;
            _syncCondition = v => condition();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ValidationRule"/> class with a synchronous condition,
        ///     allowing storage of relevant values during condition evaluation.
        /// </summary>
        /// <param name="validationFailure">The validation failure information to use if the condition fails.</param>
        /// <param name="condition">
        ///     A synchronous function that takes an <see cref="IValidationStore"/> instance and returns <c>true</c> if 
        ///     the condition fails, or <c>false</c> if it succeeds. The <see cref="IValidationStore"/> can be used to store
        ///     intermediate values or results calculated during validation for potential use in later operations.
        ///     This function determines if the rule has failed validation.
        /// </param>
        public ValidationRule(IValidationFailure validationFailure, Func<IValidationStore, bool> condition)
        {
            ValidationFailure = validationFailure;
            _syncCondition = condition;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ValidationRule"/> class with an asynchronous condition.
        /// </summary>
        /// <param name="validationFailure">The validation failure information to be used if the condition fails.</param>
        /// <param name="condition">
        ///     An asynchronous function that returns <c>true</c> if the condition fails, or <c>false</c> if it succeeds.
        ///     This function determines if the rule has failed validation.
        /// </param>
        public ValidationRule(IValidationFailure validationFailure, Func<Task<bool>> condition)
        {
            ValidationFailure = validationFailure;
            _asyncCondition = v => condition();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ValidationRule"/> class with an asynchronous condition.
        /// </summary>
        /// <param name="validationFailure">The validation failure information to be used if the condition fails.</param>
        /// <param name="condition">
        ///     An asynchronous function that takes an <see cref="IValidationStore"/> instance and returns <c>true</c> if
        ///     the condition fails, or <c>false</c> if it succeeds.
        ///     This function determines if the rule has failed validation.
        /// </param>
        public ValidationRule(IValidationFailure validationFailure, Func<IValidationStore, Task<bool>> condition)
        {
            ValidationFailure = validationFailure;
            _asyncCondition = condition;
        }

        public IValidationFailure ValidationFailure { get; }
        
        public bool IsFailed()
        {
            if (_syncCondition == null)
            {
                return false;
            }

            var isFailed = _syncCondition.Invoke(this);

            _hasFailed = isFailed;

            return isFailed;
        }
        
        public async Task<bool> IsFailedAsync()
        {
            if (_hasFailed.HasValue)
            {
                return _hasFailed.Value;
            }

            var isFailed = _asyncCondition == null ? _syncCondition?.Invoke(this) ?? false : await _asyncCondition.Invoke(this);

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