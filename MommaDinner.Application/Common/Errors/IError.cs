using System.Net;

namespace MommaDinner.Application.Common.Errors;

public interface IErrorBase
{
    public HttpStatusCode StatusCode { get; }
    public string ErrorMessage { get; }
}
