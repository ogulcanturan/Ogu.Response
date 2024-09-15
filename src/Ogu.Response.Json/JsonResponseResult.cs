using System.Collections.Generic;

namespace Ogu.Response.Json
{
    public class JsonResponseResult : JsonResponseResultBase<object>
    {
        public JsonResponseResult() : base()
        {
        }

        public JsonResponseResult(object data, string title, string detail, int? status, string type, string instance, string code, bool hasError, IDictionary<string, object> extensions)
            : base(data, title, detail, status, type, instance, code, hasError, extensions)
        {
        }
    }
}