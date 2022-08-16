using ErrorOr;
using MediatR;
using MommaDinner.Application.Authentication.Commom;

namespace MommaDinner.Application.Authentication.Commands.Register;
public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;