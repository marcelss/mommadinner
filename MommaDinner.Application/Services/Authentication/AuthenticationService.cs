using MommaDinner.Application.Common.Errors;
using MommaDinner.Application.Common.Interfaces.Authentication;
using MommaDinner.Application.Common.Interfaces.Persistence;
using MommaDinner.Domain.Entities;
using OneOf;

namespace MommaDinner.Application.Services.Authentication;


public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public AuthenticationResult Login(string email, string password)
    {
        // 1. Validate the user exists
        if (_userRepository.GetUserByEmail(email) is not User user)
        {
            throw new Exception("User with given email does not exist");
        }

        // 2. Validate the password is correct
        if (user.Password != password)
        {
            throw new Exception("Invalid Password");
        }

        // 3. Create JWT
        var token = _jwtTokenGenerator.GeneratorToken(user);

        return new AuthenticationResult(user,
                                        token);
    }

    public OneOf<AuthenticationResult, IError> Register(string firstName, string lastName, string email, string password)
    {
        // 1. Validate the user doesn't exist
        if (_userRepository.GetUserByEmail(email) is not null)
        {
            return new DuplicateEmailError();
        }

        // 2. Create user (generate unique ID) & Persist to DB
        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };

        _userRepository.Add(user);

        // 3. Create JWT
        var token = _jwtTokenGenerator.GeneratorToken(user);

        return new AuthenticationResult(user,
                                        token);
    }
}
