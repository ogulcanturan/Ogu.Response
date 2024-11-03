namespace Ogu.Response.Abstractions
{
    /// <summary>
    ///     Represents the type of the error.
    /// </summary>
    public enum ErrorType
    {
        /// <summary>
        ///     Indicates a custom-defined errors.
        /// </summary>
        Custom = 0,

        /// <summary>
        ///     Indicates a validation error that occurs during input validation.
        /// </summary>
        Validation = 1,

        /// <summary>
        ///     Indicates a standard exception that occurs during processing.
        /// </summary>
        Exception = 2,
    }
}