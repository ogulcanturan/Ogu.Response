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
            return HttpStatusCode.OK.ToSuccessJsonResponse(Samples).ToAction();
        }

        [HttpGet("examples/2")]
        public IActionResult GetExample2()
        {
            return HttpStatusCode.OK.ToFailureJsonResponse(ErrorKind.EXAMPLE_ERROR_OCCURRED).ToAction();
        }

        [HttpGet("examples/3")]
        public IActionResult GetExample3()
        {
            return HttpStatusCode.OK.ToSuccessJsonResponse(ErrorKind.EXAMPLE_ERROR_OCCURRED).ToAction();
        }

        [HttpGet("examples/5")]
        public IActionResult GetExample5()
        {
            var samples = HttpStatusCode.OK.ToSuccessJsonResponse(Samples);
            samples.Extensions["IsExample"] = true;

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
                return HttpStatusCode.InternalServerError.ToFailureJsonResponse(ex).ToAction();
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
            if (ModelState.IsValid)
            {
                return HttpStatusCode.OK.ToSuccessJsonResponse().ToAction();
            }

            return HttpStatusCode.BadRequest.ToFailureJsonResponse(ModelState).ToAction();
        }

        [HttpPost("examples/11")]
        public IActionResult GetExample11()
        {
            return HttpStatusCode.BadRequest.ToFailureJsonResponse().ToAction();
        }
    }
}