using System.Collections.Generic;

namespace Ogu.AspNetCore.Response
{
    public interface IResultBuilder
    {
        IResultBuilder WithTitle(string title);
        IResultBuilder WithDetail(string detail);
        IResultBuilder WithStatus(int? status);
        IResultBuilder WithType(string type);
        IResultBuilder WithInstance(string instance);
        IResultBuilder WithCode(string code);
        IResultBuilder WithErrors(params IError[] errors);
        IResultBuilder WithErrors(IList<IError> errors);
        IResultBuilder WithAdditionalKeyValuePair(KeyValuePair<string, object> keyValuePair);
        IResultBuilder WithAdditionalKeyValuePair(string key, object value);
        IResultBuilder WithExtensions(IDictionary<string, object> extensions);
        IResult Build();
    }
}