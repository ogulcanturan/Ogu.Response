using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Ogu.Response;
using Ogu.Response.Abstractions;
using Sample.Api.Models.Dtos;
using Sample.Api.Models.Requests;
using Sample.Api.Models.Validated;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sample.Api.Controllers;

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
        var notEmptyRule = ValidationRules.NotEmptyRule(nameof(Samples), Samples);

        if(notEmptyRule.IsFailed())
        {
            return notEmptyRule.Failure.ToResponse().ToAction();
        }

        return HttpStatusCode.OK.ToSuccessResponse(Samples).ToActionDto();
    }

    [HttpGet("examples/2")]
    public IActionResult GetExample2()
    {
        return HttpStatusCode.OK.ToFailureResponse(ErrorKind.ExampleErrorOccurred).ToActionDto();
    }

    [HttpGet("examples/3")]
    public IActionResult GetExample3()
    {
        return HttpStatusCode.OK.ToSuccessResponse(ErrorKind.ExampleErrorOccurred).ToActionDto();
    }

    [HttpGet("examples/5")]
    public IActionResult GetExample5()
    {
        var samples = HttpStatusCode.OK.ToSuccessResponse(Samples);

        samples.Extras["IsExample"] = true;

        return samples.ToActionDto();
    }

    [HttpGet("examples/6")]
    public IActionResult GetExample6()
    {
        return HttpStatusCode.BadRequest
            .ToFailureResponse(new ValidationFailure("example6", "value is required")).ToActionDto();
    }

    [HttpGet("examples/8")]
    public IActionResult GetExample8()
    {
        try
        {
            int x = 0;
            int y = 5 / x; // Will throw an exception

            return HttpStatusCode.OK.ToSuccessResponse().ToActionDto();
        }
        catch (Exception ex)
        {
            return ex.ToResponse().ToActionDto();
        }
    }

    [HttpGet("examples/9")]
    public IActionResult GetExample9()
    {
        return HttpStatusCode.OK.ToFailureResponse("Something went wrong...").ToActionDto();
    }

    [HttpPost("examples/10")]
    public IActionResult GetExample10([FromBody] SampleModel sample)
    {
        return ModelState.IsValid
            ? HttpStatusCode.OK.ToSuccessResponse().ToActionDto()
            : ModelState.ToActionDto();
    }

    [HttpPost("examples/11")]
    public IActionResult GetExample11()
    {
        return HttpStatusCode.BadRequest.ToFailureResponse().ToActionDto();
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

        var model = JsonSerializer.Deserialize<Response<SampleModel>>(rawData);

        return HttpStatusCode.OK.ToSuccessResponse(model.Data).ToActionDto();
    }

    [HttpPost("examples/13")]
    public IActionResult GetExamples13([FromBody] string id)
    {
        var idRule = ValidationRules.GreaterThanRule(nameof(id), id, 0);
        
        if (idRule.IsFailed())
        {
            return idRule.Failure.ToResponse().ToActionDto();
        }

        var storedIdValue = idRule.GetStoredValue<int>();

        return HttpStatusCode.OK.ToSuccessResponse(storedIdValue).ToActionDto();
    }

    [HttpGet("examples/14")]
    public IActionResult GetExamples14([FromQuery][Required] ExceptionTraceLevel traceLevel)
    {
        try
        {
            var innerInnerEx = new InvalidOperationException("Operation isn't valid");

            var innerEx = new ApplicationException("Application caught an expected exception", innerInnerEx);

            throw new ExternalException("There are some exceptions", innerEx);
        }
        catch (Exception ex)
        {
            return ex.ToResponse(traceLevel).ToActionDto();
        }
    }

    [HttpGet("examples/15")]
    public IActionResult GetExamples15()
    {
        var innerInnerEx = new InvalidOperationException("Operation isn't valid");
        var innerEx = new ApplicationException("Application caught an expected exception", innerInnerEx);
        throw new Exception("There are some exceptions", innerEx);
    }

    [HttpGet("examples/16")]
    public async Task<IActionResult> GetExamples16(GetExamplesSixteenRequest request)
    {
        var validator = HttpContext.RequestServices.GetRequiredService<IValidator<GetExamplesSixteenRequest, ValidatedGetExamplesSixteen>>();

        var validated = await validator.ValidateAsync(request);

        return validated.HasFailed ? validated.Failures.ToResponse().ToActionDto() : HttpStatusCode.OK.ToSuccessResponse(validated.ParsedIds).ToActionDto();
    }
}