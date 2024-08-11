using Ogu.AspNetCore.Response;
using System.ComponentModel;
using Ogu.Response.Abstractions;

namespace Sample.Api
{
    public enum ErrorKind
    {
        [Description("Don't worry, everything's gonna be alright")]
        [HelpLink("https://google.com")]
        EXAMPLE_ERROR_OCCURRED
    }
}