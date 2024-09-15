using Ogu.Response.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Ogu.Response.Json
{
    public abstract class JsonResponseResultBase<T> : IResponseResult<T>
    {
        protected JsonResponseResultBase()
        {
            Extensions = new Dictionary<string, object>();
        }

        protected JsonResponseResultBase(string title, string detail, int? status, string type, string instance, string code, bool hasError, IDictionary<string, object> extensions)
        {
            Title = title;
            Detail = detail;
            Status = status;
            Type = type;
            Instance = instance;
            Code = code;
            HasError = hasError;
            Extensions = extensions ?? new Dictionary<string, object>();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Type { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Title { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Status { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Code { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Detail { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Instance { get; }

        public bool HasError { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T Data { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IDictionary<string, object> Extensions { get; }

        public static JsonResponseResultBuilder Builder => new JsonResponseResultBuilder();

        public void AddErrorsToExtensions(IEnumerable<IResponseError> errors)
        {
            if (errors == null)
            {
                throw new ArgumentNullException(nameof(errors));
            }

            HasError = true;

            if (!Extensions.TryGetValue("Errors", out var existingValue))
            {
                var errorsAsList = errors as IList<IResponseError> ?? errors.ToList();

                Extensions.Add("Errors", errorsAsList);
                return;
            }

            var castedValue = (IList<IResponseError>)existingValue;

            foreach (var error in errors)
            {
                castedValue.Add(error);
            }
        }
    }
}