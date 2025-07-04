﻿using Ogu.Response.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ogu.Response
{
    public static class ErrorExtensions
    {
        private static readonly Lazy<Dictionary<Type, Dictionary<string, Error>>> LazyEnumTypeToEnumNameToJsonResponseError = new Lazy<Dictionary<Type,Dictionary<string,Error>>>(() => new Dictionary<Type, Dictionary<string, Error>>());

        public static IError ToError<TEnum>(this TEnum @enum) where TEnum : struct, Enum
        {
            var enumType = typeof(TEnum);
            var enumName = @enum.ToString();

            var enumTypeToEnumNameToJsonResponseError = LazyEnumTypeToEnumNameToJsonResponseError.Value;

            if (!enumTypeToEnumNameToJsonResponseError.TryGetValue(enumType, out var enumNameToJsonResponseError))
            {
                enumNameToJsonResponseError = new Dictionary<string, Error>();
                enumTypeToEnumNameToJsonResponseError[enumType] = enumNameToJsonResponseError;
            }

            if (enumNameToJsonResponseError.TryGetValue(enumName, out var jsonResponseError))
            {
                return jsonResponseError;
            }

            var errorAttribute = Extensions.GetErrorAttributeFromEnum(enumType, enumName);
            
            jsonResponseError = errorAttribute == null 
                    ? new Error(Extensions.GetTitleFromEnum(enumType, enumName) ?? ResponseDefaults.ErrorTitles.Error, Extensions.GetDescriptionFromEnum(enumType, enumName) ?? enumName, Extensions.GetTracesFromEnum(enumType, enumName), Extensions.GetValue(@enum, enumType, enumName).ToString(), Extensions.GetHelpLinkFromEnum(enumType, enumName), new List<IValidationFailure>(), ErrorType.Custom)
                    : new Error(errorAttribute.Title, errorAttribute.Description, errorAttribute.Traces, Extensions.GetValue(@enum, enumType, enumName).ToString(), errorAttribute.HelpLink, new List<IValidationFailure>(), ErrorType.Custom);

            enumNameToJsonResponseError[enumName] = jsonResponseError;

            return jsonResponseError;
        }

        public static Exception ToException(this IError error)
        {
            var exception = new Exception(error.Description)
            {
                HelpLink = error.HelpLink
            };

#if NETCOREAPP3_1_OR_GREATER
            if (int.TryParse(error.Code, out var code))
            {
                exception.HResult = code;
            }
#endif

            if (!string.IsNullOrWhiteSpace(error.Title))
            {
                exception.Data[nameof(error.Title)] = error.Title;
            }

            if (!string.IsNullOrWhiteSpace(error.Traces))
            {
                exception.Data[nameof(error.Traces)] = error.Traces;
            }
            
            if (!string.IsNullOrWhiteSpace(error.Code))
            {
                exception.Data[nameof(error.Code)] = error.Code;
            }

            exception.Data[nameof(error.Type)] = error.Type;

            if (error.ValidationFailures?.Count > 0)
            {
                exception.Data[nameof(error.ValidationFailures)] = error.ValidationFailures;
            }

            return exception;
        }

        /// <summary>
        /// Converts a list of <see cref="IError"/> instances to a single <see cref="Exception"/>.
        /// </summary>
        /// <param name="errors">
        /// The list of <see cref="IError"/> objects to convert. Each error represents an issue 
        /// that can be transformed into an <see cref="Exception"/>.</param>
        /// <returns>
        /// An <see cref="AggregateException"/> containing exceptions created from each <see cref="IError"/> 
        /// if the list contains any errors; otherwise, <c>null</c>.
        /// </returns>
        /// <remarks>
        /// If there are no errors in the provided list, this method returns <c>null</c>.
        /// </remarks>
        public static Exception ToException(this IReadOnlyList<IError> errors)
        {
            switch (errors.Count)
            {
                case 0: return null;
                case 1: return errors[0].ToException();
                default: return new AggregateException(errors.Select(error => error.ToException()));
            }
        }
    }
}