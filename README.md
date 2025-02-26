# <img src="logo/ogu-logo.png" alt="Header" width="24"/> Ogu.Response

[![.NET Core Desktop](https://github.com/ogulcanturan/Ogu.Response/actions/workflows/dotnet.yml/badge.svg?branch=master)](https://github.com/ogulcanturan/Ogu.Response/actions/workflows/dotnet.yml)
[![NuGet](https://img.shields.io/nuget/v/Ogu.AspNetCore.Response.Json.svg?color=1ecf18)](https://nuget.org/packages/Ogu.AspNetCore.Response.Json)
[![Nuget](https://img.shields.io/nuget/dt/Ogu.AspNetCore.Response.Json.svg?logo=nuget)](https://nuget.org/packages/Ogu.AspNetCore.Response.Json)

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
dotnet add package Ogu.AspNetCore.Response.Json
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
    return HttpStatusCode.OK.ToFailureJsonResponse(ErrorKind.ExampleErrorOccurred).ToAction();
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
        ? HttpStatusCode.OK.ToSuccessJsonResponse().ToAction()
        : ModelState.ToJsonAction(); 
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

**example 4:** Including Additional Data in the Response
```csharp
public IActionResult GetExample5()
{
    var samples = HttpStatusCode.OK.ToSuccessJsonResponse(new string[]{ "Freezing", "Bracing", "Chilly" });
    
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

        return HttpStatusCode.OK.ToSuccessJsonResponse().ToAction();
    }
    catch (Exception ex)
    {
        return ex.ToJsonResponse().ToAction();
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
        return ex.ToJsonResponse(traceLevel).ToAction();
    }
}
```

output: when the traces level 1 ( default )

```bash
{
  "success": false,
  "status": 500,
  "errors": [
    {
      "title": "Exception",
      "description": "There are some exceptions",
      "traces": "Exception: There are some exceptions - > ApplicationException: Application caught an expected exception - > InvalidOperationException: Operation isn't valid",
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
    var idRule = JsonValidationRules.GreaterThanRule(nameof(id), id, 0);

    if (idRule.IsFailed())
    {
        return idRule.Failure.ToJsonResponse().ToAction();
    }

    var storedIdValue = idRule.GetStoredValue<int>();

    return HttpStatusCode.OK.ToSuccessJsonResponse(storedIdValue).ToAction();
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

## Built-in Validation Rules

There are 8 built-in validation rules:

- **JsonValidationRules.GreaterThanRule**: To check if a property value is greater than a specified threshold.   
  Parsed value can be retrieved through the rule's `GetStoredValue<T>()` method.

- **JsonValidationRules.SmallerThanRule**: To check if a property value is smaller than a specified threshold.
  Parsed value can be retrieved through the rule's `GetStoredValue<T>()` method.

- **JsonValidationRules.EqualToRule**: To check if a property value is equal to a specified value.
  Parsed value can be retrieved through the rule's `GetStoredValue<T>()` method.

- **JsonValidationRules.NotEmptyRule**: To check if a property value is empty.

- **JsonValidationRules.ValidBooleanRule**: To check if a property value is a valid boolean string ("true" or "false").  
  Parsed value can be retrieved through the rule's `GetStoredValue<bool>()` method.

- **JsonValidationRules.ValidEnumRule**: To check if a property value is a valid enum value of the specified enum type.  
  Parsed value can be retrieved through the rule's `GetStoredValue<T>()` method.

- **JsonValidationRules.ValidNumberRule**: To check if a property value is a valid number (integer or floating-point).  
  Parsed value can be retrieved through the rule's `GetStoredValue<T>()` method.

- **JsonValidationRules.ValidJsonRule**: To check if a property value is a valid json string.  
  Parsed value can be retrieved through the rule's `GetStoredValue<JsonDocument>()` method.

You can extend the rules above, just like the one below.
```csharp
public static IValidationFailure InvalidBooleanFormat(string propertyName, object attemptedValue)
{
    return new JsonValidationFailure(
        propertyName,
        $"The value '{attemptedValue}' for '{propertyName}' is not a valid boolean.",
        attemptedValue
    );
}
```
```csharp
public static ValidationRule ValidBooleanRule(string propertyName, string propertyValue)
{
    return new ValidationRule(JsonValidationFailures.InvalidBooleanFormat(propertyName, propertyValue),
        (v) =>
        {
            if (!bool.TryParse(propertyValue, out var parsedValue))
            {
                return true; // Return true to indicate validation failure if parsing fails
            }

            v.Store(parsedValue); // Store the parsed boolean value 

            return false; // Return false indicating validation success
        });
}
```

## Deserialization Process

When deserializing a JsonResponse, you cannot use JsonSerializer.Deserialize directly, as it does not support deserializing interfaces. If the response indicates a failure, the deserialization process will throw an exception.

To address this, you should use DeserializableJsonResponse instead.

```csharp

var deserializableJsonResponse = JsonSerializer.Deserialize<DeserializableJsonResponse>(mySerializedJsonResponse);

IJsonResponse jsonResponse = deserializableJsonResponse.ToJsonResponse();

IJsonResponse<T> jsonResponseT = deserializableJsonResponse.ToJsonResponse<T>(); // For generic type

```

If you're using HttpClient to retrieve data of type JsonResponse, you can create extensions as shown below:

```csharp
public static class HttpContentJsonResponseExtensions
{
    public static async Task<IJsonResponse> ToJsonResponseAsync(this HttpContent content, JsonSerializerOptions serializerOptions = null, CancellationToken cancellationToken = default)
    {
        var deserializableJsonResponse = await content.ReadFromJsonAsync<DeserializableJsonResponse>(serializerOptions, cancellationToken);

        return deserializableJsonResponse.ToJsonResponse();
    }

    public static async Task<IJsonResponse<T>> ToJsonResponseAsync<T>(this HttpContent content, JsonSerializerOptions serializerOptions = null, CancellationToken cancellationToken = default)
    {
        var deserializableJsonResponse = await content.ReadFromJsonAsync<DeserializableJsonResponse>(serializerOptions, cancellationToken);

        return deserializableJsonResponse.ToJsonResponse<T>(serializerOptions);
    }
}
```

Usage

```csharp
using (var response = await _httpClient.GetAsync(relativeUri, cancellationToken
{
    return await response.Content.ToJsonResponseAsync(cancellationToken: cancellationToken);
}

using (var response = await _httpClient.GetAsync(relativeUri, cancellationToken
{
    return await response.Content.ToJsonResponseAsync<T>(cancellationToken: cancellationToken); // For generic type
}
```


## Sample Application
A sample application demonstrating the usage of Ogu.Response can be found [here](https://github.com/ogulcanturan/Ogu.Response/tree/master/samples/Sample.Api).
