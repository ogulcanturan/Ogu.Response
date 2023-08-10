namespace Ogu.AspNetCore.Response
{
    public interface IError
    {
        string Title { get; }
        string Description { get; }
        string Details { get; }
        string Code { get; }
        IValidationFailure[] ValidationFailures { get; }
        ErrorType Type { get; }
    }
}