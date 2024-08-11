namespace Ogu.Response.Abstractions
{
    public interface IResponseErrorBuilder
    {
        IResponseErrorBuilder WithTitle(string title);
        IResponseErrorBuilder WithDescription(string description);
        IResponseErrorBuilder WithDetails(string details);
        IResponseErrorBuilder WithCode(string code);
        IResponseErrorBuilder WithHelpLink(string helpLink);
        IResponseErrorBuilder WithErrorType(ErrorType errorType);
        IResponseErrorBuilder WithValidationFailures(IResponseValidationFailure[] validationFailures);
        IResponseError Build();
    }
}