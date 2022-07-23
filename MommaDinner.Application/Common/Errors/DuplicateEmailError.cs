using System.Net;
using FluentResults;

namespace MommaDinner.Application.Common.Errors
{
    #region OneOf
    /*
    public record struct DuplicateEmailError() : IErrorBase
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public string ErrorMessage => "User with given email already exists";
    }
    */
    #endregion OneOf
    
    #region fluentResult
    public class DuplicateEmailError : IError
    {
        public List<IError> Reasons => throw new NotImplementedException();

        public string Message => throw new NotImplementedException();

        public Dictionary<string, object> Metadata => throw new NotImplementedException();
    }
    #endregion fluentResult
}