using MommaDinner.Domain.Entities;

namespace MommaDinner.Application.Common.Interfaces.Authentication;
public interface IJwtTokenGenerator
{
    string GeneratorToken(User user);
}