using MommaDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using MommaDinner.Application.Services.Authentication;
using OneOf;
using MommaDinner.Application.Common.Errors;

namespace MommaDinner.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }


    [HttpPost("oneof/register")]
    public IActionResult Register(RegisterRequest request)
    {
        OneOf<AuthenticationResult, IError> registerResult = _authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);

        return registerResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            error => Problem(statusCode: (int)error.StatusCode, title: error.ErrorMessage));
    }

    private static AuthenticationResponse MapAuthResult(AuthenticationResult result)
    {
        return new AuthenticationResponse(
            result.User.Id,
            result.User.FirstName,
            result.User.LastName,
            result.User.Email,
            result.Token);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var result = _authenticationService.Login(
            request.Email,
            request.Password);

        var response = new AuthenticationResponse(
            result.User.Id,
            result.User.FirstName,
            result.User.LastName,
            result.User.Email,
            result.Token);

        return Ok(response);
    }
}
