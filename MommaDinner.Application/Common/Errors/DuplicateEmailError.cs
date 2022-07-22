using System.Net;

namespace MommaDinner.Application.Common.Errors
{
    public record struct DuplicateEmailError() : IError
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public string ErrorMessage => "User with given email already exists";
    }
}