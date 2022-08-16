using ErrorOr;
using MediatR;
using MommaDinner.Application.Authentication.Commom;

namespace MommaDinner.Application.Authentication.Queries.Login;
public record LoginQuery(
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;