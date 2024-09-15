using Ogu.Response.Abstractions;

namespace Ogu.Response.Json
{
    public class JsonResponseResultBuilder : ResponseResultBuilderBase
    {
        internal JsonResponseResultBuilder()
        {
        }

        public JsonResponseResultBuilder(IResponseResult<object> result) : base(result)
        {
        }

        public override IResponseResult<T> Build<T>() => new JsonResponseResult<T>((T)Data, Title, Detail, Status, Type, Instance, Code, HasError, Extensions);

        public static implicit operator JsonResponseResult<object>(JsonResponseResultBuilder builder) => builder.Build<object>() as JsonResponseResult<object>;
    }
}