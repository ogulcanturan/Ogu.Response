using System;
using System.Collections.Generic;

namespace Ogu.AspNetCore.Response
{
    public abstract class ResultBuilderBase : IResultBuilder
    {
        protected string Detail;
        protected string Title;
        protected int? Status;
        protected string Instance;
        protected string Type;
        protected string Code;
        protected Lazy<IDictionary<string, object>> Extensions = new Lazy<IDictionary<string, object>>(() => new Dictionary<string, object>());

        public IResultBuilder WithTitle(string title)
        {
            Title = title;
            return this;
        }

        public IResultBuilder WithDetail(string detail)
        {
            Detail = detail;
            return this;
        }

        public IResultBuilder WithStatus(int? status)
        {
            Status = status;
            return this;
        }

        public IResultBuilder WithType(string type)
        {
            Type = type;
            return this;
        }

        public IResultBuilder WithInstance(string instance)
        {
            Instance = instance;
            return this;
        }

        public IResultBuilder WithCode(string code)
        {
            Code = code;
            return this;
        }

        public IResultBuilder WithErrors(params IError[] errors) => this.WithAdditionalKeyValuePair(new KeyValuePair<string, object>("Errors", errors));

        public IResultBuilder WithAdditionalKeyValuePair(KeyValuePair<string, object> keyValuePair)
        {
            Extensions.Value.Add(keyValuePair);
            return this;
        }

        public IResultBuilder WithAdditionalKeyValuePair(string key, object value)
        {
            Extensions.Value.Add(new KeyValuePair<string, object>(key, value));
            return this;
        }

        public IResultBuilder WithExtensions(IDictionary<string, object> extensions)
        {
            if (extensions?.Count > 0)
            {
                Extensions = new Lazy<IDictionary<string, object>>(() => extensions);
                _ = Extensions.Value;
            }
            return this;
        }

        public abstract IResult Build();
    }
}