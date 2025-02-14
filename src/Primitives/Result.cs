using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitives
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T? Value { get; }
        public string Error { get; }

        private Result(bool isSuccess, T? value, string error)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
        }

        public static Result<T> Success(T value) => new Result<T>(true, value, string.Empty);
        public static Result<T> Failure(string error) => new Result<T>(false, default, error);
    }

    // Classe helper per creare Result senza tipo generico
    public static class Result
    {
        public static Result<T> Success<T>(T value) => Result<T>.Success(value);
        public static Result<T> Failure<T>(string error) => Result<T>.Failure(error);

    }
}
