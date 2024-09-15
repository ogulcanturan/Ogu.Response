using System.Collections.Generic;

namespace Ogu.Response.Abstractions
{
    public interface IResponseResultBuilder
    {
        IResponseResultBuilder WithData(object data);
        IResponseResultBuilder WithTitle(string title);
        IResponseResultBuilder WithDetail(string detail);
        IResponseResultBuilder WithStatus(int? status);
        IResponseResultBuilder WithType(string type);
        IResponseResultBuilder WithInstance(string instance);
        IResponseResultBuilder WithCode(string code);
        IResponseResultBuilder WithErrors(params IResponseError[] errors);
        IResponseResultBuilder WithErrors(IEnumerable<IResponseError> errors);
        IResponseResultBuilder WithAdditionalKeyValuePair(KeyValuePair<string, object> keyValuePair);
        IResponseResultBuilder WithAdditionalKeyValuePair(string key, object value);
        IResponseResultBuilder WithExtensions(IDictionary<string, object> extensions);
        IResponseResult<T> Build<T>();
        IResponseResult<object> Build();
    }
}