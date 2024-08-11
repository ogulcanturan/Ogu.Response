namespace Ogu.Response.Abstractions
{
    public interface IResponseError
    {
        string Title { get; }

        string Description { get; }

        string Details { get; }

        string Code { get; }

        string HelpLink { get; }

        IResponseValidationFailure[] ValidationFailures { get; }

        ErrorType Type { get; }
    }
}