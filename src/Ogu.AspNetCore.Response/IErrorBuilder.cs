namespace Ogu.AspNetCore.Response
{
    public interface IErrorBuilder
    {
        IErrorBuilder WithTitle(string title);
        IErrorBuilder WithDescription(string description);
        IErrorBuilder WithDetails(string details);
        IErrorBuilder WithCode(string code);
        IErrorBuilder WithHelpLink(string helpLink);
        IErrorBuilder WithErrorType(ErrorType errorType);
        IErrorBuilder WithValidationFailures(IValidationFailure[] validationFailures);
        IError Build();
    }
}