using Microsoft.Extensions.DependencyInjection;
using Ogu.Response;
using Ogu.Response.Abstractions;

namespace Unit.Tests.Response
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddValidator_RegistersValidatorInServiceCollection()
        {
            var services = new ServiceCollection();

            ServiceCollectionExtensions.AddValidator(services);
            
            var serviceProvider = services.BuildServiceProvider();

            // Act
            var validator = serviceProvider.GetService<IValidator>();

            // Assert
            Assert.NotNull(validator);
            Assert.IsType<Validator>(validator);
        }

        [Fact]
        public void AddValidator_WhenServiceNull_ThrowsArgumentNullException()
        {
            ServiceCollection? services = null;

            // Act
            var exception = Record.Exception(() => ServiceCollectionExtensions.AddValidator(services));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}
