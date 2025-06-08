using System.Collections.Generic;

namespace Ogu.Response.Abstractions
{
    /// <summary>
    /// Represents configuration options for validating a <see cref="HashSet{T}"/> in a validation rule.
    /// </summary>
    public class HashSetRuleOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HashSetRuleOptions"/> class.
        /// </summary>
        /// <param name="allowEmpty">Indicates whether an empty set is considered valid.</param>
        /// <param name="requireAllUnique">Indicates whether all values must be unique (enforced even beyond <see cref="HashSet{T}"/> behavior).</param>
        /// <param name="continueOnFailure">Indicates whether to continue validation on failure.</param>
        public HashSetRuleOptions(bool allowEmpty, bool requireAllUnique, bool continueOnFailure)
        {
            AllowEmpty = allowEmpty;
            RequireAllUnique = requireAllUnique;
            ContinueOnFailure = continueOnFailure;
        }

        /// <summary>
        /// Gets a value indicating whether an empty set is allowed.
        /// </summary>
        public bool AllowEmpty { get; }

        /// <summary>
        /// Gets a value indicating whether all items must be unique.
        /// </summary>
        public bool RequireAllUnique { get; }

        /// <summary>
        /// Gets a value indicating whether validation should continue even if a failure occurs.
        /// Partial stored value can still retrievable even though validation failed.
        /// </summary>
        public bool ContinueOnFailure { get; }

        /// <summary>
        /// Gets the default <see cref="HashSetRuleOptions"/> instance with <c>AllowEmpty = false</c>, <c>RequireAllUnique = false</c> and <c>ContinueOnFailure = false</c>.
        /// </summary>
        public static HashSetRuleOptions Default { get; } = new HashSetRuleOptions(false, false, false);
    }
}