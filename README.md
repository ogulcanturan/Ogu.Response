# <img src="logo/ogu-logo.png" alt="Header" width="24"/> Ogu.Response

[![.NET Core Desktop](https://github.com/ogulcanturan/Ogu.Response/actions/workflows/dotnet.yml/badge.svg?branch=master)](https://github.com/ogulcanturan/Ogu.Response/actions/workflows/dotnet.yml)
[![NuGet](https://img.shields.io/nuget/v/Ogu.Response.Json.svg?color=1ecf18)](https://nuget.org/packages/Ogu.Response.Json)
[![Nuget](https://img.shields.io/nuget/dt/Ogu.Response.Json.svg?logo=nuget)](https://nuget.org/packages/Ogu.Response.Json)

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

**example 1:**
```csharp
public IActionResult GetExample1()
{
    return HttpStatusCode.OK.ToSuccessResponse(new string[]{ "Freezing", "Bracing", "Chilly" }).ToAction();
}
```

output

```bash
{
  "data": [
    "Freezing",
    "Bracing",
    "Chilly"
  ],
  "success": true,
  "status": 200
}
```

**example 2:**
```csharp
public IActionResult GetExample2()
{
    return HttpStatusCode.OK.ToFailureJsonResponse(ErrorKind.EXAMPLE_ERROR_OCCURRED).ToAction();
}
```

output

```bash
{
  "success": false,
  "status": 200,
  "result": {
    "title": "Bad Request",
    "status": 400,
    "detail": "Custom failure occurred.",
    "extensions": {
      "errors": [
        {
          "title": "EXAMPLE_ERROR_OCCURRED",
          "description": "Don't worry, everything's gonna be alright",
          "code": "0",
          "type": 0
        }
      ]
    }
  }
}
```

## Sample Application
A sample application demonstrating the usage of Ogu.AspNetCore.Response can be found [here](https://github.com/ogulcanturan/Ogu.Response/tree/master/samples/Sample.Api).
