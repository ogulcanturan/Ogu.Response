using Microsoft.Extensions.DependencyInjection;
using Moq;
using Ogu.Response;
using Ogu.Response.Abstractions;
using Assert = Xunit.Assert;

namespace Unit.Tests.Response;

public class ValidatorTests
{
    private readonly Mock<IServiceProvider> _serviceProviderMock = new();

    [Fact]
    public async Task ValidateAsync_WhenValidatorIsRegistered_ReturnsValidatedModel()
    {
        var input = new InputModel();

        var validatorType = typeof(IValidator<InputModel, ValidatedModel>);

        var validatorMock = new Mock<IValidator<InputModel, ValidatedModel>>();

        validatorMock.Setup(v => v.ValidateAsync(input, It.IsAny<CancellationToken>())).Returns(() =>
            new ValueTask<ValidatedModel>(new ValidatedModel([])));

        _serviceProviderMock.Setup(sp => sp.GetService(validatorType)).Returns(validatorMock.Object);

        var validator = new Validator(_serviceProviderMock.Object);

        // Act
        var validatedModel = await validator.ValidateAsync<InputModel, ValidatedModel>(input);

        // Assert
        Assert.NotNull(validatedModel);
        Assert.False(validatedModel.HasFailed);
            
        _serviceProviderMock.Verify(sp => sp.GetService(validatorType), Times.Once);
        validatorMock.Verify(v => v.ValidateAsync(input, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ValidateAsync_WhenValidatorNotRegistered_ThrowsInvalidOperationException()
    {
        var input = new InputModel();

        var validatorType = typeof(IValidator<InputModel, ValidatedModel>);

        var validator = new Validator(_serviceProviderMock.Object);

        // Arrange
        var record = await Record.ExceptionAsync(() => validator.ValidateAsync<InputModel, ValidatedModel>(input).AsTask());

        // Assert
        Assert.NotNull(record);
        Assert.IsType<InvalidOperationException>(record);

        _serviceProviderMock.Verify(sp => sp.GetService(validatorType), Times.Once);
    }

    [Fact]
    public async Task ValidateScopedAsync_WhenValidatorIsRegistered_ReturnsValidatedModel()
    {
        var input = new InputModel();

        var validatorType = typeof(IValidator<InputModel, ValidatedModel>);

        var validatorMock = new Mock<IValidator<InputModel, ValidatedModel>>();

        var serviceScopeFactoryType = typeof(IServiceScopeFactory);
        var serviceScopeFactoryMock = new Mock<IServiceScopeFactory>();

        serviceScopeFactoryMock.Setup(f => f.CreateScope())
            .Returns(Mock.Of<IServiceScope>(scope => scope.ServiceProvider == _serviceProviderMock.Object));

        validatorMock.Setup(v => v.ValidateAsync(input, It.IsAny<CancellationToken>())).Returns(() =>
            new ValueTask<ValidatedModel>(new ValidatedModel([])));

        _serviceProviderMock.Setup(sp => sp.GetService(serviceScopeFactoryType)).Returns(serviceScopeFactoryMock.Object);
        _serviceProviderMock.Setup(sp => sp.GetService(validatorType)).Returns(validatorMock.Object);

        var validator = new Validator(_serviceProviderMock.Object);

        // Act
        var validatedModel = await validator.ValidateScopedAsync<InputModel, ValidatedModel>(input);

        // Assert
        Assert.NotNull(validatedModel);
        Assert.False(validatedModel.HasFailed);

        serviceScopeFactoryMock.Verify(f => f.CreateScope(), Times.Once);
        _serviceProviderMock.Verify(sp => sp.GetService(validatorType), Times.Once);
        validatorMock.Verify(v => v.ValidateAsync(input, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ValidateScopedAsync_WhenValidatorNotRegistered_ThrowsInvalidOperationException()
    {
        var input = new InputModel();

        var validatorType = typeof(IValidator<InputModel, ValidatedModel>);

        var serviceScopeFactoryType = typeof(IServiceScopeFactory);
        var serviceScopeFactoryMock = new Mock<IServiceScopeFactory>();

        serviceScopeFactoryMock.Setup(f => f.CreateScope())
            .Returns(Mock.Of<IServiceScope>(scope => scope.ServiceProvider == _serviceProviderMock.Object));

        _serviceProviderMock.Setup(sp => sp.GetService(serviceScopeFactoryType)).Returns(serviceScopeFactoryMock.Object);

        var validator = new Validator(_serviceProviderMock.Object);

        // Arrange
        var record = await Record.ExceptionAsync(() => validator.ValidateScopedAsync<InputModel, ValidatedModel>(input).AsTask());

        // Assert
        Assert.NotNull(record);
        Assert.IsType<InvalidOperationException>(record);

        serviceScopeFactoryMock.Verify(f => f.CreateScope(), Times.Once);
        _serviceProviderMock.Verify(sp => sp.GetService(validatorType), Times.Once);
    }

    public class InputModel;
    public class ValidatedModel(List<IValidationFailure> failures) : Validated(failures);
}