# <img src="logo/ogu-logo.png" alt="Header" width="24"/> Ogu.Response

[![.NET Core Desktop](https://github.com/ogulcanturan/Ogu.Response/actions/workflows/dotnet.yml/badge.svg?branch=master)](https://github.com/ogulcanturan/Ogu.Response/actions/workflows/dotnet.yml)
[![NuGet](https://img.shields.io/nuget/v/Ogu.AspNetCore.Response.svg?color=1ecf18)](https://nuget.org/packages/Ogu.AspNetCore.Response)
[![Nuget](https://img.shields.io/nuget/dt/Ogu.AspNetCore.Response.svg?logo=nuget)](https://nuget.org/packages/Ogu.AspNetCore.Response)

## Introduction

Provides a generic response type (`IResponse`) compatible with `IActionResult` in `Microsoft.AspNetCore.Mvc`. This library is designed to simplify API responses by offering a structured model that can contain data, errors, validation errors, and additional extensions.

## Features

- **Generic Response:** A flexible and generic response model that can hold various types of data, including single objects, collections, or custom data structures.
- **Error Handling:** Easily handle errors and exceptions in API responses, providing consistent error formats.
- **Validation Errors:** Supports validation errors, making it easy to report validation issues in the response.
- **Extensions:** Includes support for adding custom extensions to the response for specific use cases.

## Installation

You can install the library via NuGet Package Manager:

```bash
dotnet add package Ogu.AspNetCore.Response
```
## Usage

**example 1:** Returning an Success Response
```csharp
public IActionResult GetExample1()
{
    return HttpStatusCode.OK.ToSuccessResponse(new string[]{ "Freezing", "Bracing", "Chilly" }).ToAction();
}
```

output

```bash
{
  "success": true,
  "status": 200,
  "data": [
    "Freezing",
    "Bracing",
    "Chilly"
  ]
}
```

**example 2:** Returning an Error Response Using an Enum
```csharp
public enum ErrorKind
{
    [Error("Example Error", "Don't worry, everything's gonna be alright", "", "https://google.com")]
    ExampleErrorOccurred = 1
}
```
```csharp
public IActionResult GetExample2()
{
    return HttpStatusCode.OK.ToFailureResponse(ErrorKind.ExampleErrorOccurred).ToAction();
}
```

output

```bash
{
  "success": false,
  "status": 200,
  "errors": [
    {
      "title": "Example Error",
      "description": "Don't worry, everything's gonna be alright",
      "traces": "",
      "code": "1",
      "helpLink": "https://google.com",
      "type": 0 // 0: Custom, 1: Validation, 2: Exception
    }
  ]
}
```

**example 3:** Returning an Error Response via ModelState
```csharp
public IActionResult GetExample10([FromBody] SampleModel sample)
{
    return ModelState.IsValid
        ? HttpStatusCode.OK.ToSuccessResponse().ToAction()
        : ModelState.ToAction(); 
}
```

output

```bash
{
  "success": false,
  "status": 400,
  "errors": [
    {
      "title": "Validation Error",
      "description": "One or more validation errors occurred.",
      "type": 1,
      "validationFailures": [
        {
          "propertyName": "Name",
          "message": "The field Name must be a string with a minimum length of 3 and a maximum length of 50.",
          "severity": 0
        }
      ]
    }
  ]
}
```

> [!TIP]
> If controller attached with `[ApiController]` attribute, ASP.NET Core automatically returns a 400 Bad Request when ModelState is invalid — before your controller code is even reached. To take full control you need to suppress this behavior:  
> `services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);` 

**example 4:** Including Additional Data in the Response
```csharp
public IActionResult GetExample5()
{
    var samples = HttpStatusCode.OK.ToSuccessResponse(new string[]{ "Freezing", "Bracing", "Chilly" });
    
    samples.Extras["IsExample"] = true;

    return samples.ToAction();
}
```

output

```bash
{
  "success": true,
  "status": 200,
  "data": [
    "Freezing",
    "Bracing",
    "Chilly"
  ],
  "extras": {
    "isExample": true
  }
}
```

**example 5:** Returning an Error Response via an Occurred Exception
```csharp
public IActionResult GetExample8()
{
    try
    {
        int x = 0;
        int y = 5 / x; // Will throw an exception

        return HttpStatusCode.OK.ToSuccessResponse().ToAction();
    }
    catch (Exception ex)
    {
        return ex.ToResponse().ToAction();
    }
}
```

output

```bash
{
  "success": false,
  "status": 500,
  "errors": [
    {
      "title": "Exception",
      "description": "Attempted to divide by zero.",
      "traces": "DivideByZeroException: Attempted to divide by zero.",
      "code": "-2147352558",
      "type": 2
    }
  ]
}
```

**example 6:** Returning an Error Response via an Occurred Exception
```csharp
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
        return ex.ToResponse(traceLevel).ToAction();
    }
}
```

output: when the traces level 2 ( Summary )

```bash
{
  "success": false,
  "status": 500,
  "errors": [
    {
      "title": "Exception",
      "description": "There are some exceptions",
      "traces": "Exception: There are some exceptions -> ApplicationException: Application caught an expected exception -> InvalidOperationException: Operation isn't valid",
      "code": "-2146233088",
      "type": 2
    }
  ]
}
```

**example 7:** Returning an Error Response via a Custom Validation Rule
```csharp
public IActionResult GetExamples13([FromBody] string id)
{
    var idRule = ValidationRules.GreaterThanRule(nameof(id), id, 0);

    if (idRule.IsFailed())
    {
        return idRule.Failure.ToResponse().ToAction();
    }

    var storedIdValue = idRule.GetStoredValue<int>();

    return HttpStatusCode.OK.ToSuccessResponse(storedIdValue).ToAction();
}
```

output: When the requested id value is 0

```bash
{
  "success": false,
  "status": 400,
  "errors": [
    {
      "title": "Validation Error",
      "description": "One or more validation errors occurred.",
      "type": 1,
      "validationFailures": [
        {
          "propertyName": "id",
          "message": "id must be valid number and value must be greater than 0.",
          "attemptedValue": "0",
          "severity": 0
        }
      ]
    }
  ]
}
```

output: When the requested id value is greater than 1

```bash
{
  "success": true,
  "status": 200,
  "data": 1
}
```

### Handling Exceptions

In ASP.NET Core, you can register the exception-handling middleware early in the pipeline to ensure consistent error responses using `Response`.

```csharp
app.UseExceptionHandler(cfg =>
{
    cfg.Run(async (context) =>
    {
        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

        var response = contextFeature.Error.ToResponse(ExceptionTraceLevel.Basic); // Default uses ExceptionTraceLevel.Basic
        
        // (optional) Add extras into the response model. e.g., CorrelationId etc.
        response.Extras.Add(nameof(HeaderNames.RequestId), Activity.Current?.Id ?? context.TraceIdentifier);

        await response.ToAction().ExecuteResultAsync(context);
    });
});
```

```csharp
[HttpGet("examples/15")]
public IActionResult GetExamples15()
{
    var innerInnerEx = new InvalidOperationException("Operation isn't valid");
    var innerEx = new ApplicationException("Application caught an expected exception", innerInnerEx);
    throw new Exception("There are some exceptions", innerEx);
}
```

output:

```bash
{
  "success": false,
  "status": 500,
  "errors": [
    {
      "title": "Exception",
      "description": "There are some exceptions",
      "traces": "Exception: There are some exceptions -> ApplicationException: Application caught an expected exception -> InvalidOperationException: Operation isn't valid",
      "code": "-2146233088",
      "type": 2
    }
  ],
  "extras": {
    "requestId": "00-127004cd9c8f88d3559496646d2624c8-4db22e889d3a2b52-00"
  }
}
```

### Handling Model Validation Errors

In ASP.NET Core, you can customize how model validation errors are returned by configuring ApiBehaviorOptions. This allows you to return a consistent `Response` instead of the default `BadRequestObjectResult`.

```csharp
services.Configure<ApiBehaviorOptions>(options =>
{
    // Override the default behavior to return Response for model validation errors
    options.InvalidModelStateResponseFactory = context => context.ModelState.ToAction();
});
```

## Built-in Validation Rules

There are 8 built-in validation rules:

- **ValidationRules.GreaterThanRule**: To check if a property value is greater than a specified threshold.   
  Parsed value can be retrieved through the rule's `GetStoredValue<T>()` method.

- **ValidationRules.SmallerThanRule**: To check if a property value is smaller than a specified threshold.
  Parsed value can be retrieved through the rule's `GetStoredValue<T>()` method.

- **ValidationRules.EqualToRule**: To check if a property value is equal to a specified value.
  Parsed value can be retrieved through the rule's `GetStoredValue<T>()` method.

- **ValidationRules.NotEmptyRule**: To check if a property value is empty.

- **ValidationRules.ValidBooleanRule**: To check if a property value is a valid boolean string ("true" or "false").  
  Parsed value can be retrieved through the rule's `GetStoredValue<bool>()` method.

- **ValidationRules.ValidEnumRule**: To check if a property value is a valid enum value of the specified enum type.  
  Parsed value can be retrieved through the rule's `GetStoredValue<T>()` method.

- **ValidationRules.ValidNumberRule**: To check if a property value is a valid number (integer or floating-point).  
  Parsed value can be retrieved through the rule's `GetStoredValue<T>()` method.

- **ValidationRules.ValidJsonRule**: To check if a property value is a valid json string.  
  Parsed value can be retrieved through the rule's `GetStoredValue<JsonDocument>()` method. After done with the JsonDocument do not forget the dispose it.

You can extend the rules above, just like the one below.
```csharp
public static IValidationFailure InvalidBooleanFormat(string propertyName, object attemptedValue)
{
    return new ValidationFailure(
        propertyName,
        $"The value '{attemptedValue}' for field '{propertyName}' is not a valid boolean.",
        attemptedValue
    );
}
```
```csharp
public static ValidationRule ValidBooleanRule(string propertyName, string propertyValue)
{
    return new ValidationRule(ValidationFailures.InvalidBooleanFormat(propertyName, propertyValue),
        (v) =>
        {
            if (!bool.TryParse(propertyValue, out var parsedValue))
            {
                return false; // Return false to indicate validation failure if parsing fails
            }

            v.Store(parsedValue); // Store the parsed boolean value 

            return true; // Return true indicating validation success
        });
}
```

## Deserialization Process

When deserializing a Response, you cannot use JsonSerializer.Deserialize directly, as it does not support deserializing interfaces. If the response indicates a failure, the deserialization process will throw an exception.

To address this, you should create corresponding models instead.

```csharp

public class ResponseDto : ResponseDto<object>
{
}

public class ResponseDto<T>
{
    public bool Success { get; init; }

    public HttpStatusCode Status { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public T Data { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ErrorDto[] Errors { get; init; } = [];

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, object> Extras { get; init; } = [];
}

```

```csharp
public class ErrorDto
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Title { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Description { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Traces { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Code { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string HelpLink { get; init; }

    public ErrorType Type { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ValidationFailureDto[] ValidationFailures { get; init; } = [];
}
```

```csharp
public class ValidationFailureDto
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string PropertyName { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Message { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object AttemptedValue { get; init; }

    public Severity Severity { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Code { get; init; }
}
```

```csharp
public static class DtoExtensions
{
    public static IResponse ToResponse(this ResponseDto responseDto)
    {
        return new Response(responseDto.Data, responseDto.Success, responseDto.Status, responseDto.Extras,
            responseDto.Errors?.Select(IError (e) => e.ToError()).ToList());
    }

    public static IResponse<TData> ToResponse<TData>(this ResponseDto<TData> responseDto)
    {
        return new Response<TData>(responseDto.Data, responseDto.Success, responseDto.Status, responseDto.Extras,
            responseDto.Errors?.Select(IError (e) => e.ToError()).ToList());
    }

    public static IResponse<TData> ToResponseOf<TData>(this ResponseDto responseDto)
    {
        var data = responseDto.Data switch
        {
            null => default,
            TData tData => tData,
            _ => (TData)Convert.ChangeType(responseDto.Data, typeof(TData))
        };

        return new Response<TData>(data, responseDto.Success, responseDto.Status, responseDto.Extras, responseDto.Errors?.Select(IError (e) => e.ToError()).ToList());
    }

    public static IActionResult ToActionDto(this ModelStateDictionary modelState)
    {
        return modelState.ToResponse().ToActionDto();
    }

    public static IActionResult ToActionDto<T>(this ModelStateDictionary modelState)
    {
        return modelState.ToResponse().ToActionDto();
    }

    public static IActionResult ToAction(this ResponseDto responseDto)
    {
        return InternalToAction((int)responseDto.Status, responseDto);
    }

    public static IActionResult ToAction<T>(this ResponseDto<T> responseDto)
    {
        return InternalToAction((int)responseDto.Status, responseDto);
    }

    private static IActionResult InternalToAction(int statusCode, object response)
    {
        return ResponseDefaults.NoResponseStatusCodes.Contains(statusCode)
            ? (IActionResult)new StatusCodeResult(statusCode)
            : new ObjectResult(response);
    }

    public static IActionResult ToActionDto(this IResponse response)
    {
        var responseDto = new ResponseDto
        {
            Success = response.Success,
            Status = response.Status,
            Data = response.Data,
            Errors = response.Errors.Count == 0
                ? null
                : response.Errors.Select(e => e.ToErrorDto()).ToArray(),
            Extras = response.Extras.Count == 0 ? null : response.Extras as Dictionary<string, object> ?? response.Extras.ToDictionary()
        };

        return responseDto.ToAction();
    }

    public static IActionResult ToActionDto<T>(this IResponse<T> response)
    {
        var responseDto = new ResponseDto<T>
        {
            Success = response.Success,
            Status = response.Status,
            Data = response.Data,
            Errors = response.Errors.Count == 0
                ? null
                : response.Errors.Select(e => e.ToErrorDto()).ToArray(),
            Extras = response.Extras.Count == 0 ? null : response.Extras as Dictionary<string, object> ?? response.Extras.ToDictionary()
        };

        return responseDto.ToAction();
    }

    private static Error ToError(this ErrorDto errorDto)
    {
        return new Error(errorDto.Title, errorDto.Description, errorDto.Traces, errorDto.Code, errorDto.HelpLink,
            errorDto.ValidationFailures?.Select(IValidationFailure (vf) => vf.ToValidationFailure()).ToList(), errorDto.Type);
    }

    private static ValidationFailure ToValidationFailure(this ValidationFailureDto vfDto)
    {
        return new ValidationFailure(vfDto.PropertyName, vfDto.Message, vfDto.AttemptedValue,
            vfDto.Severity, vfDto.Code);
    }

    private static ErrorDto ToErrorDto(this IError error)
    {
        return new ErrorDto
        {
            Title = error.Title,
            Description = error.Description,
            Traces = error.Traces,
            Code = error.Code,
            HelpLink = error.HelpLink,
            Type = error.Type,
            ValidationFailures = error.ValidationFailures.Count == 0
                ? null
                : error.ValidationFailures.Select(vf => vf.ToValidationFailureDto()).ToArray()
        };
    }

    private static ValidationFailureDto ToValidationFailureDto(this IValidationFailure vf)
    {
        return new ValidationFailureDto
        {
            PropertyName = vf.PropertyName,
            Message = vf.Message,
            AttemptedValue = vf.AttemptedValue,
            Severity = vf.Severity,
            Code = vf.Code
        };
    }
}
```

If you're using HttpClient to retrieve data of type Response, you can create extensions as shown below:

```csharp
public static class HttpContentResponseExtensions
{
    public static async Task<IResponse> ToResponseAsync(this HttpContent content, JsonSerializerOptions serializerOptions = null, CancellationToken cancellationToken = default)
    {
        var responseDto = await content.ReadFromJsonAsync<ResponseDto>(serializerOptions, cancellationToken);

        return responseDto.ToResponse();
    }

    public static async Task<IResponse<T>> ToResponseAsync<T>(this HttpContent content, JsonSerializerOptions serializerOptions = null, CancellationToken cancellationToken = default)
    {
        var responseDto = await content.ReadFromJsonAsync<ResponseDto<T>>(serializerOptions, cancellationToken);

        return responseDto.ToResponse();
    }
}
```

Usage

```csharp
using (var response = await _httpClient.GetAsync(relativeUri, cancellationToken
{
    return await response.Content.ToResponseAsync(cancellationToken: cancellationToken);
}

using (var response = await _httpClient.GetAsync(relativeUri, cancellationToken
{
    return await response.Content.ToResponseAsync<T>(cancellationToken: cancellationToken); // For generic type
}
```

## Sample Application
A sample application demonstrating the usage of Ogu.Response can be found [here](https://github.com/ogulcanturan/Ogu.Response/tree/master/samples/Sample.Api).
