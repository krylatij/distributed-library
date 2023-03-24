using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DistributedLibrary.Shared.Enums;

namespace DistributedLibrary.Shared.Dto;

public class ResponseDto<T>
{
    public ResponseDto()
    {
    }

    public ResponseDto(T result)
    {
        Result = result;
    }

    public ResponseDto(T result, ResponseState responseState)
    {
        Result = result;
        ResponseState = responseState;
    }

    public ResponseDto(T result, ResponseState responseState, string message)
    {
        Result = result;
        ResponseState = responseState;
        Messages = new List<string> {message};
    }

    public T Result { get; set; } = default!;

    public List<string> Messages { get; set; } = default!;

    [JsonIgnore]
    public ResponseState ResponseState { get; set; } = default!;

    public void SetMessage(string message)
    {
        Messages ??= new List<string>();
        Messages.Add(message);
    }

    public string GetMessage()
    {
        return string.Join(Environment.NewLine, Messages);
    }
}