namespace Ogu.Response.Abstractions
{
    /// <summary>
    /// Specifies the level of exception trace details to include in the traces.
    /// </summary>
    public enum ExceptionTraceLevel
    {
        /// <summary>
        /// No exception description or traces are included.
        /// <example>
        /// <code>
        /// {
        ///    "title": "Exception",
        ///    "description": "Oops! Something went wrong."
        ///    ...
        /// }
        /// </code>
        /// </example>
        /// </summary>
        None = 0,

        /// <summary>
        /// Includes exception message. e.g.
        /// <example>
        /// <code>
        /// {
        ///    "title": "Exception",
        ///    "description": "There are some exceptions."
        ///    ...
        /// }
        /// </code>
        /// </example>
        /// </summary>
        Basic = 1,

        /// <summary>
        /// Includes basic information about the exception and its inner exceptions, such as the type and message. e.g.
        /// <example>
        /// <code>
        /// {
        ///    "title": "Exception",
        ///    "description": "There are some exceptions."
        ///    "traces": "ExternalException: There are some exceptions -> ApplicationException: Application caught an expected exception -> ...
        ///    ...
        /// }
        /// </code>
        /// </example>
        /// </summary>
        Summary = 2,

        /// <summary>
        /// Includes the full exception trace details, such as the type, message, and stack trace.
        /// <example>
        /// <code>
        /// {
        ///    "title": "Exception",
        ///    "description": "There are some exceptions."
        ///    "traces": "System.Runtime.InteropServices.ExternalException (0x80004005): There are some exceptions\r\n --> System.ApplicationException: Application caught an expected exception\r\n ---> ...
        ///    ...
        /// }
        /// </code>
        /// </example>
        /// </summary>
        Full = 3
    }
}