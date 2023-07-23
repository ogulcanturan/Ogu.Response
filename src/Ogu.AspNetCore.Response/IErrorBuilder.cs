namespace Ogu.AspNetCore.Response
{
    public interface IErrorBuilder
    {
        IErrorBuilder WithTitle(string title);

        IErrorBuilder WithDescription(string description);

        IErrorBuilder WithCode(string code);

        IErrorBuilder WithValidationFailures(IValidationFailure[] validationFailures);

        IError Build();
    }
}