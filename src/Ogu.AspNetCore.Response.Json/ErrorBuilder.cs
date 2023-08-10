namespace Ogu.AspNetCore.Response.Json
{
    public class ErrorBuilder : ErrorBuilderBase
    {
        public override IError Build() => new Error(Title, Description, Details, Code, ValidationFailures, ErrorType);

        public static implicit operator Error(ErrorBuilder builder) => builder.Build() as Error;
    }
}