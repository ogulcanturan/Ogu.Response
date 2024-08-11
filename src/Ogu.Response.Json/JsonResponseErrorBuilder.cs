using Ogu.Response.Abstractions;

namespace Ogu.Response.Json
{
    public class JsonResponseErrorBuilder : ErrorBuilderBase
    {
        public override IResponseError Build() => new JsonResponseError(Title, Description, Details, Code, HelpLink, ValidationFailures, ErrorType);

        public static implicit operator JsonResponseError(JsonResponseErrorBuilder builder) => builder.Build() as JsonResponseError;
    }
}