using Microsoft.Extensions.DependencyInjection;
using Ogu.Response.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ogu.Response
{
    internal sealed class Validator : IValidator
    {
        private readonly IServiceProvider _provider;

        public Validator(IServiceProvider provider)
        {
            _provider = provider;
        }

        public
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
            ValueTask
#else
            Task
#endif
            <TValidated> ValidateAsync<TInput, TValidated>(TInput input, CancellationToken cancellationToken = default) where TValidated : IValidated
        {
            var service = _provider.GetRequiredService<IValidator<TInput, TValidated>>();

            return service.ValidateAsync(input, cancellationToken);
        }

        public
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
            ValueTask
#else
            Task
#endif            
            <TValidated> ValidateScopedAsync<TInput, TValidated>(TInput input, CancellationToken cancellationToken = default) where TValidated : IValidated
        {
            using (var scope = _provider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IValidator<TInput, TValidated>>();
;
                return service.ValidateAsync(input, cancellationToken);
            }
        }
    }
}