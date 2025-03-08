namespace Ogu.Response.Abstractions
{
    /// <summary>
    /// Represents a validation failure associated with a property.
    /// </summary>
    public interface IValidationFailure 
    {
        /// <summary>
        /// The name of the property that failed validation.
        /// </summary>
        string PropertyName { get; set; }

        /// <summary>
        /// The error message describing the validation failure.
        /// </summary>
        string Message { get; set; }

        /// <summary>
        /// The value that was attempted for the property, which caused the validation failure.
        /// </summary>
        object AttemptedValue { get; set; }

        /// <summary>
        /// The severity level associated with the failure. 
        /// Default level is <c>Error</c>.
        /// <list>
        ///     <item>Error (0): Indicates a critical validation failure.</item>
        ///     <item>Warning (1): Indicates a potential issue that should be reviewed.</item>
        ///     <item>Info (2): Provides informational feedback that may not indicate a failure.</item>
        /// </list>
        /// </summary>
        Severity Severity { get; set; }

        /// <summary>
        /// A code that can be used to identify the validation failure.
        /// </summary>
        string Code { get; set; }
    }
}