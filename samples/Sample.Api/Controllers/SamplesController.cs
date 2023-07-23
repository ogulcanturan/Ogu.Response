using System.Net;
using Microsoft.AspNetCore.Mvc;
using Ogu.AspNetCore.Response;
using Ogu.AspNetCore.Response.Json;

namespace Sample.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SamplesController : ControllerBase
    {
        private static readonly string[] Samples = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("examples/1")]
        public IActionResult GetExample1()
        {
            return HttpStatusCode.OK.ToSuccessResponse(Samples);
        }

        [HttpGet("examples/2")]
        public IActionResult GetExample2()
        {
            return HttpStatusCode.OK.ToFailResponse(ErrorKind.EXAMPLE_ERROR_OCCURRED);
        }

        [HttpGet("examples/3")]
        public IActionResult GetExample3()
        {
            return HttpStatusCode.OK.ToSuccessResponse(null, 
                Result.BasicFailure(ErrorKind.EXAMPLE_ERROR_OCCURRED));
        }

        [HttpGet("examples/4")]
        public IActionResult GetExample4()
        {
            return HttpStatusCode.OK.ToSuccessResponse(Samples, 
                Result.Builder.WithTitle("Example 4").WithCode("Example:4").Build());
        }

        [HttpGet("examples/5")]
        public IActionResult GetExample5()
        {
            return HttpStatusCode.OK.ToSuccessResponse(Samples, 
                Result.Builder.WithAdditionalKeyValuePair(new KeyValuePair<string, object>("ExtraProperties", true)).Build());
        }

        [HttpGet("examples/6")]
        public IActionResult GetExample6()
        {
            return HttpStatusCode.BadRequest.ToFailResponse(new IValidationFailure[]{ new ValidationFailure("example6","value is required")});
        }

        [HttpGet("examples/7")]
        public IActionResult GetExample7()
        {
            return HttpStatusCode.OK.ToOtherResponse(null, true, Result.Builder.WithStatus(400).Build());
        }
    }
}
