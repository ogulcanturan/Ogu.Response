namespace Ogu.Response.Abstractions
{
    public interface IResponse
    {
        bool Success { get; }

        int Status { get; }

        string SerializedResponse { get; }

        IResponseResult Result { get; }
    }
}