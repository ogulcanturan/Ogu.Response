using System.Threading;
using System.Threading.Tasks;

namespace Ogu.Response.Abstractions
{
    public interface IValidator<in TRequest, TValidated> where TValidated : IValidated<TRequest>
    {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
        ValueTask
#else
        Task
#endif
        <TValidated> ValidateAsync(TRequest request, CancellationToken cancellationToken = default);
    }
}