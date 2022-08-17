using ErrorOr;
using MediatR;
using MommaDinner.Application.Common.Interfaces.Authentication;
using MommaDinner.Application.Common.Interfaces.Persistence;
using MommaDinner.Domain.Commom.Errors;
using MommaDinner.Domain.Entities;
using MommaDinner.Application.Authentication.Commom;

namespace MommaDinner.Application.Authentication.Commands.Register;

/// <summary>
/// Handler with ErrorOr & Domain Errors
/// </summary>
public class RegisterCommandHandler :
    IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        if (_userRepository.GetUserByEmail(command.Email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }

        // 2. Create user (generate unique ID) & Persist to DB
        var user = new User
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Password = command.Password
        };

        _userRepository.Add(user);

        // 3. Create JWT
        var token = _jwtTokenGenerator.GeneratorToken(user);

        return new AuthenticationResult(user, token);
    }
}
