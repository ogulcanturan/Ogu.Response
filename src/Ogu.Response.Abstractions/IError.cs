using System.Collections.Generic;

namespace Ogu.Response.Abstractions
{
    public interface IError
    {
        string Title { get; }

        string Description { get; }

        string Traces { get; }

        string Code { get; }

        string HelpLink { get; }

        ErrorType Type { get; }

        List<IValidationFailure> ValidationFailures { get; set; }
    }
}