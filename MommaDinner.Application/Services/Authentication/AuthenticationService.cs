using ErrorOr;
using FluentResults;
using MommaDinner.Application.Common.Errors;
using MommaDinner.Application.Common.Interfaces.Authentication;
using MommaDinner.Application.Common.Interfaces.Persistence;
using MommaDinner.Domain.Commom.Errors;
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



    #region OneOf
    /// <summary>
    /// This approach is using OneOf package to handle the errors
    /// </summary>
    /// <returns></returns>
    /*
    public OneOf<AuthenticationResult, IErrorBase> Register(string firstName, string lastName, string email, string password)
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

        return new AuthenticationResult(user, token);
    }
    */
    #endregion OneOf

    #region FluentResults
    /*
    /// <summary>
    /// This approach is using FluentResult package to handle the errors
    /// </summary>
    public Result<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    {
        // 1. Validate the user doesn't exist
        if (_userRepository.GetUserByEmail(email) is not null)
        {
            return Result.Fail<AuthenticationResult>(new[] { new DuplicateEmailError()});
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

        return new AuthenticationResult(user, token);
    }
    */
    #endregion FluentResults

    #region ErrorOr & Domain Errors

    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {
        // 1. Validate the user exists
        if (_userRepository.GetUserByEmail(email) is not User user)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        // 2. Validate the password is correct
        if (user.Password != password)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        // 3. Create JWT
        var token = _jwtTokenGenerator.GeneratorToken(user);

        return new AuthenticationResult(user,
                                        token);
    }

    public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    {
        // 1. Validate the user doesn't exist
        if (_userRepository.GetUserByEmail(email) is not null)
        {
            return Errors.User.DuplicateEmail;
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

        return new AuthenticationResult(user, token);
    }
    #endregion ErrorOr & Domain Errors
}
