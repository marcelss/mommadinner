using ErrorOr;
using FluentResults;
using MommaDinner.Application.Common.Errors;
using OneOf;

namespace MommaDinner.Application.Services.Authentication.Commands;

public interface IAuthenticationCommandService
{
    //OneOf<AuthenticationResult, IErrorBase> Register(string firstName, string lastName, string email, string password);
    //Result<AuthenticationResult> Register(string firstName, string lastName, string email, string password);
    //ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password);
}