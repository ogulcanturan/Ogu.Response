using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ogu.Response.Abstractions
{
    public static class ValidationRuleExtensions
    {
        /// <summary>
        ///     Validates a set of synchronous validation rules and returns the first validation failure, if any.
        ///     For asynchronous conditions, this method does nothing.
        /// </summary>
        /// <param name="rules">The set of validation rules to check.</param>
        /// <returns>
        ///     A task representing the asynchronous operation, with a result of the first <see cref="IResponseValidationFailure"/> 
        ///     if any condition fails; otherwise, <c>null</c> if all conditions pass.
        /// </returns>
        public static IValidationFailure ValidateFirstOrDefault(this IEnumerable<ValidationRule> rules)
        {
            return rules.FirstOrDefault(rule => rule.IsFailed())?.ValidationFailure;
        }

        /// <summary>
        ///     Validates a set of asynchronous or synchronous validation rules and returns the first validation failure, if any.
        /// </summary>
        /// <param name="rules">The set of validation rules to check.</param>
        /// <returns>
        ///     A task representing the asynchronous operation, with a result of the first <see cref="IValidationFailure"/> 
        ///     if any condition fails; otherwise, <c>null</c> if all conditions pass.
        /// </returns>
        public static async Task<IValidationFailure> ValidateFirstOrDefaultAsync(this IEnumerable<ValidationRule> rules)
        {
            foreach (var rule in rules)
            {
                if (await rule.IsFailedAsync())
                {
                    return rule.ValidationFailure;
                }
            }

            return null;
        }

        /// <summary>
        ///     Validates a set of synchronous validation rules and returns any failures.
        ///     For asynchronous conditions, this method does nothing.
        /// </summary>
        /// <param name="rules">The set of validation rules to check.</param>
        /// <returns>
        ///     A list of <see cref="IValidationFailure"/> objects representing validation failures,
        ///     if any conditions fail.
        /// </returns>
        public static List<IValidationFailure> ValidateAll(this IEnumerable<ValidationRule> rules)
        {
            return rules.Where(rule => rule.IsFailed())
                .Select(rule => rule.ValidationFailure)
                .ToList();
        }

        /// <summary>
        ///     Validates a set of asynchronous or synchronous validation rules and returns any failures.
        /// </summary>
        /// <param name="rules">The set of validation rules to check.</param>
        /// <returns>
        ///     A task representing the asynchronous validation operation, with a result of a list of 
        ///     <see cref="IValidationFailure"/> objects representing validation failures if any conditions fail.
        /// </returns>
        public static async Task<List<IValidationFailure>> ValidateAllAsync(this IEnumerable<ValidationRule> rules)
        {
            var result = await Task.WhenAll(rules.Select(async rule => new
            {
                IsFailed = await rule.IsFailedAsync(),
                rule.ValidationFailure
            }));

            return result.Where(r => r.IsFailed)
                .Select(r => r.ValidationFailure)
                .ToList();
        }
    }
}