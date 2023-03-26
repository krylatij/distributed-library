using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DistributedLibrary.Shared.Enums;

namespace DistributedLibrary.Shared.Dto;

[ExcludeFromCodeCoverage]
public class ResponseDto<T> : ResponseDto
{
    public ResponseDto(T result, ResponseState responseState, string? message = null) : base(responseState, message)
    {
        Result = result;
    }

    public T Result { get; set; } = default!;
}

public class ResponseDto
{
    protected ResponseDto(ResponseState responseState, string? message = null)
    {
        ResponseState = responseState;
        if (message != null)
        {
            Messages = new List<string> { message };
        }
    }

    [JsonIgnore]
    public ResponseState ResponseState { get; set; } = default!;

    public List<string> Messages { get; set; } = default!;

    public void SetMessage(string message)
    {
        Messages ??= new List<string>();
        Messages.Add(message);
    }

    public string GetMessage()
    {
        return string.Join(Environment.NewLine, Messages);
    }

    public static ResponseDto ValidationFail(string message)
    {
        return new ResponseDto(ResponseState.ValidationFailed, message);
    }

    public static ResponseDto<TResult?> ValidationFail<TResult>(string message)
    {
        return new ResponseDto<TResult?>(default, ResponseState.ValidationFailed, message);
    }

    public static ResponseDto Ok()
    {
        return new ResponseDto(ResponseState.Ok);
    }

    public static ResponseDto<TResult?> Ok<TResult>(TResult result)
    {
        return new ResponseDto<TResult?>(result, ResponseState.Ok);
    }

    public static ResponseDto Error(string message)
    {
        return new ResponseDto(ResponseState.Error, message);
    }
}