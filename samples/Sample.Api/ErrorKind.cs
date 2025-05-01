using Ogu.Response.Abstractions;

namespace Sample.Api
{
    public enum ErrorKind
    {
        [Error("Example Error", "Don't worry, everything's gonna be alright", "", "https://google.com")]
        ExampleErrorOccurred = 1
    }
}