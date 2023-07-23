namespace Ogu.AspNetCore.Response.Json
{
    public class ResultBuilder : ResultBuilderBase
    {
        public override IResult Build() => new Result(Title, Detail, Status, Type, Instance, Code, Extensions.IsValueCreated ? Extensions.Value : null);

        public static implicit operator Result(ResultBuilder builder) => builder.Build() as Result;
    }
}