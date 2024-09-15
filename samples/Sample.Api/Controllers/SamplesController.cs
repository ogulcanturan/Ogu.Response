using Microsoft.AspNetCore.Mvc;
using Ogu.AspNetCore.Response.Json;
using Ogu.Response.Json;
using System;
using System.Net;

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
            var resp = HttpStatusCode.OK.ToSuccessJsonResponse(Samples);

            return HttpStatusCode.OK.ToSuccessJsonResponse(Samples).ToAction();
        }

        [HttpGet("examples/2")]
        public IActionResult GetExample2()
        {
            return HttpStatusCode.OK.ToFailJsonResponse(ErrorKind.EXAMPLE_ERROR_OCCURRED).ToAction();
        }

        [HttpGet("examples/3")]
        public IActionResult GetExample3()
        {
            return HttpStatusCode.OK.ToSuccessJsonResponse(result:
                JsonResponseResult.Builder.JsonCustomFailure(ErrorKind.EXAMPLE_ERROR_OCCURRED)).ToAction();
        }

        [HttpGet("examples/4")]
        public IActionResult GetExample4()
        {
            return HttpStatusCode.OK.ToSuccessJsonResponse(JsonResponseResult.Builder.WithData(Samples).WithTitle("Example 4").WithCode("Example:4").Build()).ToAction();
        }

        [HttpGet("examples/5")]
        public IActionResult GetExample5()
        {
            return HttpStatusCode.OK.ToSuccessJsonResponse(JsonResponseResult.Builder.WithData(Samples).WithAdditionalKeyValuePair("IsExample", true).Build()).ToAction();
        }

        [HttpGet("examples/6")]
        public IActionResult GetExample6()
        {
            return HttpStatusCode.BadRequest.ToFailJsonResponse(new JsonValidationFailure("example6","value is required")).ToAction();
        }

        [HttpGet("examples/7")]
        public IActionResult GetExample7()
        {
           return HttpStatusCode.OK.ToOtherJsonResponse(true, JsonResponseResult.Builder.WithStatus(400).Build()).ToAction();
        }

        [HttpGet("examples/8")]
        public IActionResult GetExample8()
        {
            try
            {
                int x = 0;
                int y = 5 / x; // Will throw an exception

                return HttpStatusCode.OK.ToSuccessJsonResponse().ToAction();
            }
            catch (Exception ex)
            {
                return HttpStatusCode.InternalServerError.ToFailJsonResponse(ex).ToAction();
            }
        }

        [HttpGet("examples/9")]
        public IActionResult GetExample9()
        {
            return HttpStatusCode.OK.ToFailJsonResponse("Something went wrong...").ToAction();
        }

        [HttpPost("examples/10")]
        public IActionResult GetExample10([FromBody] SampleModel sample)
        {
            if (ModelState.IsValid)
            {
                return HttpStatusCode.OK.ToSuccessJsonResponse().ToAction();
            }

            return HttpStatusCode.BadRequest.ToFailJsonResponse(ModelState).ToAction();
        }
    }
}