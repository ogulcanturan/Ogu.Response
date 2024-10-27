namespace Ogu.Response.Abstractions
{
    /// <summary>
    /// Provides a mechanism for storing validation-related data that can be retrieved later during or after validation processing.
    /// </summary>
    public interface IValidationStore
    {
        /// <summary>
        /// Stores a specified value for later retrieval, enabling the retention of relevant data for validation purposes.
        /// </summary>
        /// <param name="value">The object to store, typically representing data from a validation check.</param>
        void Store(object value);
    }
}