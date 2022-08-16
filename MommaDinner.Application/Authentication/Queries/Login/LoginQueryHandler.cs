using ErrorOr;
using MediatR;
using MommaDinner.Application.Common.Interfaces.Authentication;
using MommaDinner.Application.Common.Interfaces.Persistence;
using MommaDinner.Domain.Commom.Errors;
using MommaDinner.Domain.Entities;
using MommaDinner.Application.Authentication.Commom;

namespace MommaDinner.Application.Authentication.Queries.Login;

/// <summary>
/// <summary>
/// Handler with ErrorOr & Domain Errors
/// </summary>
/// </summary>
public class LoginQueryHandler :
    IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        // 1. Validate the user exists
        if (_userRepository.GetUserByEmail(query.Email) is not User user)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        // 2. Validate the password is correct
        if (user.Password != query.Password)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        // 3. Create JWT
        var token = _jwtTokenGenerator.GeneratorToken(user);

        return new AuthenticationResult(user,
                                        token);
    }
}
