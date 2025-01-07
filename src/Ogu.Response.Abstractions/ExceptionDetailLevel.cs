namespace Ogu.Response.Abstractions
{
    /// <summary>
    ///     Specifies the level of exception trace details to include in the traces.
    /// </summary>
    public enum ExceptionTraceLevel
    {
        /// <summary>
        ///     No exception traces are included.
        /// </summary>
        None = 0,

        /// <summary>
        ///     Includes basic information about the exception and its inner exceptions, such as the type and message.
        /// </summary>
        Basic = 1,

        /// <summary>
        ///     Includes the full exception trace details, such as the type, message, and stack trace.
        /// </summary>
        Full = 2
    }
}