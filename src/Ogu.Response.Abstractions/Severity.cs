namespace Ogu.Response.Abstractions
{
    /// <summary>
    /// Represents the severity levels of validation failures.
    /// </summary>
    public enum Severity
    {
        /// <summary>
        /// Indicates a critical validation failure.
        /// </summary>
        Error,

        /// <summary>
        /// Indicates a potential issue that should be reviewed.
        /// </summary>
        Warning,

        /// <summary>
        /// Provides informational feedback that may not indicate a failure.
        /// </summary>
        Info
    }
}