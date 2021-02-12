using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Application
{
    public class Result
    {
        public bool IsSuccess { get; protected set; }
        public string Message { get; protected set; }
        public static Result Ok(string message = null) => new Result() { Message = message, IsSuccess = true };
        public static Result Failed(string message = null) => new Result() { Message = message, IsSuccess = false };

        public static Result<T> Ok<T>(T data = default, string message = null) => new Result<T>(data, message, true);
        public static Result<T> Failed<T>(string message = null, T data = default) => new Result<T>(data, message, false);

    }
    public sealed class Result<T> : Result
    {
        public Result(T data, string message)
        {
            Data = data;
            Message = message;
        }

        public Result(T data, string message, bool isSuccess) : this(data, message)
        {
            Data = data;
            Message = message;
            IsSuccess = isSuccess;
        }


        public T Data { get; }


        public static Result<T> Ok(T data = default, string message = null) => new Result<T>(data, message, true);
        public static Result<T> Failed(string message = null, T data = default) => new Result<T>(data, message, false);
    }
}
