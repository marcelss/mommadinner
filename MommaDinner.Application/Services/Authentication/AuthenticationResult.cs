using MommaDinner.Domain.Entities;

namespace MommaDinner.Application.Services.Authentication;

public record AuthenticationResult(
    User User,
    string Token);