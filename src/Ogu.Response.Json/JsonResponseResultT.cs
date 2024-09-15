using System.Collections.Generic;

namespace Ogu.Response.Json
{
    public class JsonResponseResult<T> : JsonResponseResultBase<T>
    {
        public JsonResponseResult() : base()
        {
        }

        public JsonResponseResult(T data, string title, string detail, int? status, string type, string instance, string code, bool hasError, IDictionary<string, object> extensions)
            : base(data, title, detail, status, type, instance, code, hasError, extensions)
        {
        }
    }
}