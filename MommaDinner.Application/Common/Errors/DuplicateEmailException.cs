using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MommaDinner.Application.Common.Errors;

public class DuplicateEmailException : Exception, IServiceException
{
    public DuplicateEmailException(string? message = null) : base(message)
    {
        if (string.IsNullOrWhiteSpace(message))
            message = ErrorMessage;
    }

    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

    public string ErrorMessage => "User with given email already exists";
}
