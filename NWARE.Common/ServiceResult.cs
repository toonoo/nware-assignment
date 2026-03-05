using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace NWARE.Common
{
    public class ServiceResult<T>
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("data")]
        public T Data { get; set; }

        [JsonPropertyName("total")]
        public int? Total { get; set; }

        // Array / List
        public static ServiceResult<List<TItem>> SuccessList<TItem>(List<TItem> data, string message = "Success", int code = 200)
            => new ServiceResult<List<TItem>> { Code = code, Message = message, Data = data, Total = data?.Count };

        // Fail
        public static ServiceResult<T> Fail(string message, int code = 400)
            => new ServiceResult<T> { Code = code, Message = message, Data = default };
    }
}
