namespace Ogu.Response.Abstractions
{
    public interface IResponse<out T> 
    {
        T Data { get; }

        bool Success { get; }

        int Status { get; }

        string SerializedResponse { get; }

        IResponseResult Result { get; }
    }
}