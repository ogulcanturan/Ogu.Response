using System.Collections.Generic;
using System.Linq;

namespace Ogu.Response.Abstractions
{
    public abstract class ResponseResultBuilderBase : IResponseResultBuilder
    {
        protected string Detail;
        protected string Title;
        protected int? Status;
        protected string Instance;
        protected string Type;
        protected string Code;
        protected bool HasError;
        protected IDictionary<string, object> Extensions = new Dictionary<string, object>();

        protected ResponseResultBuilderBase() { }

        protected ResponseResultBuilderBase(IResponseResult<object> result)
        {
            this.WithTitle(result.Title)
                .WithDetail(result.Detail)
                .WithStatus(result.Status)
                .WithType(result.Type)
                .WithInstance(result.Instance)
                .WithCode(result.Code)
                .WithExtensions(result.Extensions);
        }

        public IResponseResultBuilder WithTitle(string title)
        {
            Title = title;
            return this;
        }

        public IResponseResultBuilder WithDetail(string detail)
        {
            Detail = detail;
            return this;
        }

        public IResponseResultBuilder WithStatus(int? status)
        {
            Status = status;
            return this;
        }

        public IResponseResultBuilder WithType(string type)
        {
            Type = type;
            return this;
        }

        public IResponseResultBuilder WithInstance(string instance)
        {
            Instance = instance;
            return this;
        }

        public IResponseResultBuilder WithCode(string code)
        {
            Code = code;
            return this;
        }

        public IResponseResultBuilder WithError(IResponseError error)
        {
            return WithErrors(error);
        }

        public IResponseResultBuilder WithErrors(params IResponseError[] errors)
        {
            HasError = true;

            if (!Extensions.TryGetValue("Errors", out var existingValue))
            {
                return this.WithAdditionalKeyValuePair("Errors", errors.ToList());
            }

            var castedValue = (IList<IResponseError>)existingValue;

            foreach (var error in errors)
            {
                castedValue.Add(error);
            }

            return this;
        }

        public IResponseResultBuilder WithErrors(IEnumerable<IResponseError> errors)
        {
            HasError = true;

            if (!Extensions.TryGetValue("Errors", out var existingValue))
            {
                return this.WithAdditionalKeyValuePair("Errors", errors.ToList());
            }

            var castedValue = (IList<IResponseError>)existingValue;

            foreach (var error in errors)
            {
                castedValue.Add(error);
            }

            return this;
        }

        public IResponseResultBuilder WithAdditionalKeyValuePair(string key, object value) => WithAdditionalKeyValuePair(new KeyValuePair<string, object>(key, value));

        public IResponseResultBuilder WithAdditionalKeyValuePair(KeyValuePair<string, object> keyValuePair)
        {
            Extensions.Add(keyValuePair);
            return this;
        }

        public IResponseResultBuilder WithExtensions(IDictionary<string, object> extensions)
        {
            if (!(extensions?.Count > 0))
            {
                return this;
            }

            Extensions = extensions;

            return this;
        }

        public abstract IResponseResult<T> Build<T>();

        public IResponseResult<object> Build() => Build<object>();
    }
}