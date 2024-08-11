using Ogu.Response.Abstractions;

namespace Ogu.Response.Json
{
    public class JsonResponseResultBuilder : ResponseResultBuilderBase
    {
        internal JsonResponseResultBuilder()
        {
        }

        public JsonResponseResultBuilder(IResponseResult result) : base(result)
        {
        }

        public override IResponseResult Build() => new JsonResponseResult(Title, Detail, Status, Type, Instance, Code, HasError, Extensions.IsValueCreated ? Extensions.Value : null);

        public static implicit operator JsonResponseResult(JsonResponseResultBuilder builder) => builder.Build() as JsonResponseResult;
    }
}