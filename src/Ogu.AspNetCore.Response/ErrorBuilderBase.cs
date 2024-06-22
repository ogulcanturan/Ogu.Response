namespace Ogu.AspNetCore.Response
{
    public abstract class ErrorBuilderBase : IErrorBuilder
    {
        protected string Title;
        protected string Description;
        protected string Details;
        protected string Code;
        protected string HelpLink;
        protected IValidationFailure[] ValidationFailures;
        protected ErrorType ErrorType;

        public IErrorBuilder WithTitle(string title)
        {
            Title = title;
            return this;
        }

        public IErrorBuilder WithDescription(string description)
        {
            Description = description;
            return this;
        }

        public IErrorBuilder WithDetails(string details)
        {
            Details = details;
            return this;
        }

        public IErrorBuilder WithCode(string code)
        {
            Code = code;
            return this;
        }

        public IErrorBuilder WithHelpLink(string helpLink)
        {
            HelpLink = helpLink;
            return this;
        }

        public IErrorBuilder WithErrorType(ErrorType errorType)
        {
            ErrorType = errorType;
            return this;
        }

        public IErrorBuilder WithValidationFailures(IValidationFailure[] validationFailures)
        {
            if (validationFailures?.Length > 0)
            {
                ValidationFailures = validationFailures;
                ErrorType = ErrorType.Validation;
            }

            return this;
        }

        public abstract IError Build();
    }
}