using System.Collections.Generic;

namespace Ogu.Response.Abstractions
{
    public interface IResponseError
    {
        string Title { get; }

        string Description { get; }

        string Details { get; }

        string Code { get; }

        string HelpLink { get; }

        ErrorType Type { get; }

        List<IResponseValidationFailure> ValidationFailures { get; set; }
    }
}