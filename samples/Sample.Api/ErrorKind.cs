﻿using Ogu.Response.Abstractions;
using System.ComponentModel;

namespace Sample.Api
{
    public enum ErrorKind
    {
        [Title("Enum Error")]
        [Description("Don't worry, everything's gonna be alright")]
        [HelpLink("https://google.com")]
        EXAMPLE_ERROR_OCCURRED
    }
}