namespace Ogu.Response.Abstractions
{
    public interface IResponse<T> 
    {
        bool Success { get; }

        int Status { get; }

        string SerializedResponse { get; }

        IResponseResult<T> Result { get; }
    }
}