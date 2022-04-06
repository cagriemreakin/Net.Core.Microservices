#nullable disable

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Course.Common.Dtos {
    public class Response<T> {
        public T Data { get; set; }

        [JsonIgnore]
        public int StatusCode { get; private set; }

        [JsonIgnore]
        public bool IsSucess { get; private set; }

        public List<string> Errors { get; set; }


        #region Static Factory Methods

        public static Response<T> Success(T data, int statusCode) {
            return new Response<T> { Data = data, StatusCode = statusCode, IsSucess = true };
        }
        public static Response<T> Success(int statusCode) {
            return new Response<T> { Data = default, StatusCode = statusCode, IsSucess = true };
        }
        public static Response<T> Fail(List<string> errors, int statusCode) {
            return new Response<T> { Errors = errors, StatusCode = statusCode, IsSucess = false };
        }
        public static Response<T> Fail(string error, int statusCode) {
            return new Response<T> { Errors = new List<string>() { error }, StatusCode = statusCode, IsSucess = false };
        }

        #endregion
    }
}
