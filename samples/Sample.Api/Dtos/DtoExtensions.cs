using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Ogu.Response;
using Ogu.Response.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Api.Dtos;

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