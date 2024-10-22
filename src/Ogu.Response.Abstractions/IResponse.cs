namespace Ogu.Response.Abstractions
{
    public interface IResponse<TSerialized> : IResponse<object, TSerialized>
    {
    }
}