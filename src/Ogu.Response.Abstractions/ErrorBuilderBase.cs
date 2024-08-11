namespace Ogu.Response.Abstractions
{
    public abstract class ErrorBuilderBase : IResponseErrorBuilder
    {
        protected string Title;
        protected string Description;
        protected string Details;
        protected string Code;
        protected string HelpLink;
        protected IResponseValidationFailure[] ValidationFailures;
        protected ErrorType ErrorType;

        public IResponseErrorBuilder WithTitle(string title)
        {
            Title = title;
            return this;
        }

        public IResponseErrorBuilder WithDescription(string description)
        {
            Description = description;
            return this;
        }

        public IResponseErrorBuilder WithDetails(string details)
        {
            Details = details;
            return this;
        }

        public IResponseErrorBuilder WithCode(string code)
        {
            Code = code;
            return this;
        }

        public IResponseErrorBuilder WithHelpLink(string helpLink)
        {
            HelpLink = helpLink;
            return this;
        }

        public IResponseErrorBuilder WithErrorType(ErrorType errorType)
        {
            ErrorType = errorType;
            return this;
        }

        public IResponseErrorBuilder WithValidationFailures(IResponseValidationFailure[] validationFailures)
        {
            if (validationFailures?.Length > 0)
            {
                ValidationFailures = validationFailures;
                ErrorType = ErrorType.Validation;
            }

            return this;
        }

        public abstract IResponseError Build();
    }
}