namespace Ogu.AspNetCore.Response.Abstractions
{
    /// <summary>
    /// Represents a response from an action result with a generic data type of <c>object</c>.
    /// This interface allows for a more general response structure without specifying a concrete data type.
    /// </summary>
    public interface IActionResponse : IActionResponse<object> { }
}