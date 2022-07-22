using System.Net;

namespace MommaDinner.Application.Common.Errors;

public interface IError
{
    public HttpStatusCode StatusCode { get; }
    public string ErrorMessage { get; }
}
