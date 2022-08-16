using ErrorOr;
using FluentResults;
using MommaDinner.Application.Authentication.Commom;
using MommaDinner.Application.Common.Errors;
using OneOf;

namespace MommaDinner.Application.Services.Authentication.Queries;

public interface IAuthenticationQueryService
{
    ErrorOr<AuthenticationResult> Login(string email, string password);
}