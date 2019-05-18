using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace Rolo.Auth.Core.Entities
{
    public class Result<T> where T : class
    {
        public Boolean Status { get; private set; }

        public List<string> Errors { get; private set; }

        public T Body { get; set; }

        private Result(Boolean status, T body = null)
        {
            this.Status = status;
            this.Body = body;

            this.Errors = new List<string>();
        }

        public static Result<T> Sucess(T body)
        {
            return new Result<T>(true, body);
        }

        public static Result<T> Error(T body = null)
        {
            return new Result<T>(false, body);
        }

        public static Result<T> Error(string mensagemErro)
        {
            return new Result<T>(false).WithMessage(mensagemErro);
        }

        internal static Result<T> Error(ValidationResult validationResult)
        {
            var result = new Result<T>(false);

            foreach (var item in validationResult.Errors)
                result.Errors.Add(item.ErrorMessage);

            return result;
        }

        public Result<T> WithMessage(string errorMessage)
        {
            this.Errors.Add(errorMessage);
            return this;
        }
    }
}
