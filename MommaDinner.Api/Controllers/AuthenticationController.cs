using System.Linq;
using MommaDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using OneOf;
using MommaDinner.Application.Common.Errors;
using MediatR;
using MommaDinner.Application.Authentication.Commands.Register;
using MommaDinner.Application.Authentication.Queries.Login;
using MommaDinner.Application.Authentication.Commom;
using MapsterMapper;

namespace MommaDinner.Api.Controllers;

[Route("auth")]
public class AuthenticationController : ApiController
{    
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    public AuthenticationController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    #region OneOf
    /*
    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        OneOf<AuthenticationResult, IErrorBase> registerResult = _authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);

        return registerResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            error => Problem(statusCode: (int)error.StatusCode, title: error.ErrorMessage));
    }
    */
    #endregion OneOf

    #region FluentResults
    /*
    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var registerResult = _authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);

        if (registerResult.IsSuccess)
            return Ok(MapAuthResult(registerResult.Value));

        var firstError = registerResult.Errors.First();

        if (firstError is DuplicateEmailError)
            return Problem(statusCode: (int)StatusCodes.Status409Conflict, title: "User with given email already exists");

        return Problem();
    }
    */
    #endregion FluentResults

    #region ErrorOr & Domain Errors

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);

        var registerResult = await _mediator.Send(command);

        return registerResult.Match(
            registerResult => Ok(_mapper.Map<AuthenticationResponse>(registerResult)),
            errors => Problem(errors)
        );
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);

        var authResult = await _mediator.Send(query);
       
        return authResult.Match(
            authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
            errors => Problem(errors)
        );
    }

    #endregion ErrorOr & Domain Errors

    /// <summary>
    /// Manually Map
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    private static AuthenticationResponse MapAuthResult(AuthenticationResult result)
    {
        return new AuthenticationResponse(
            result.User.Id,
            result.User.FirstName,
            result.User.LastName,
            result.User.Email,
            result.Token);
    }
}
