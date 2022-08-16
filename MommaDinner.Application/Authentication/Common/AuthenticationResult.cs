using MommaDinner.Domain.Entities;

namespace MommaDinner.Application.Authentication.Commom;

public record AuthenticationResult(
    User User,
    string Token);