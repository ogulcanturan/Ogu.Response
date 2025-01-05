using Microsoft.AspNetCore.Mvc;
using Ogu.AspNetCore.Response.Json;
using Ogu.Response.Abstractions;
using Ogu.Response.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace Sample.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SamplesController : ControllerBase
    {
        private static readonly string[] Samples = new[]
        {
            "Freezing", "Bracing", "Chilly"
        };

        [HttpGet("examples/1")]
        public IActionResult GetExample1()
        {
            return HttpStatusCode.OK.ToSuccessJsonResponse(Samples).ToAction();
        }

        [HttpGet("examples/2")]
        public IActionResult GetExample2()
        {
            return HttpStatusCode.OK.ToFailureJsonResponse(ErrorKind.ExampleErrorOccurred).ToAction();
        }

        [HttpGet("examples/3")]
        public IActionResult GetExample3()
        {
            return HttpStatusCode.OK.ToSuccessJsonResponse(ErrorKind.ExampleErrorOccurred).ToAction();
        }

        [HttpGet("examples/5")]
        public IActionResult GetExample5()
        {
            var samples = HttpStatusCode.OK.ToSuccessJsonResponse(Samples);
            
            samples.Extras["IsExample"] = true;

            return samples.ToAction();
        }

        [HttpGet("examples/6")]
        public IActionResult GetExample6()
        {
            return HttpStatusCode.BadRequest.ToFailureJsonResponse(new JsonValidationFailure("example6", "value is required")).ToAction();
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
                return ex.ToJsonResponse().ToAction();
            }
        }

        [HttpGet("examples/9")]
        public IActionResult GetExample9()
        {
            return HttpStatusCode.OK.ToFailureJsonResponse("Something went wrong...").ToAction();
        }

        [HttpPost("examples/10")]
        public IActionResult GetExample10([FromBody] SampleModel sample)
        {
            return ModelState.IsValid
                ? HttpStatusCode.OK.ToSuccessJsonResponse().ToAction()
                : ModelState.ToJsonAction();
        }

        [HttpPost("examples/11")]
        public IActionResult GetExample11()
        {
            return HttpStatusCode.BadRequest.ToFailureJsonResponse().ToAction();
        }

        [HttpGet("examples/12")]
        public IActionResult GetExample12()
        {
            var rawData = """
                          {
                            "Data": {
                              "Id": 1,
                              "Name": "Oğulcan",
                              "Age": 28,
                              "No": 1,
                              "Abilities": []
                            }
                          }
                          """;

            var model = JsonSerializer.Deserialize<JsonResponse<SampleModel>>(rawData);

            return HttpStatusCode.OK.ToSuccessJsonResponse(model.Data).ToAction();
        }

        [HttpPost("examples/13")]
        public IActionResult GetExamples13([FromBody] string id)
        {
            var idRule = JsonValidationRules.GreaterThanRule(nameof(id), id, 0);

            if (idRule.IsFailed())
            {
                return idRule.Failure.ToJsonResponse().ToAction();
            }

            var storedIdValue = idRule.GetStoredValue<int>();

            return HttpStatusCode.OK.ToSuccessJsonResponse(storedIdValue).ToAction();
        }

        [HttpGet("examples/14")]
        public IActionResult GetExamples14([FromQuery][Required] ExceptionTraceLevel traceLevel)
        {
            try
            {
                var innerInnerEx = new InvalidOperationException("Operation isn't valid");

                var innerEx = new ApplicationException("Application caught an expected exception", innerInnerEx);

                throw new Exception("There are some exceptions", innerEx);
            }
            catch (Exception ex)
            {
                return ex.ToJsonResponse(traceLevel).ToAction();
            }
        }
    }
}