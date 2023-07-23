namespace Ogu.AspNetCore.Response
{
    public interface IValidationFailure
    {
        string PropertyName { get; set; }

        string Message { get; set; }

        object AttemptedValue { get; set; }

        Severity Severity { get; set; }

        string Code { get; set; }
    }
}