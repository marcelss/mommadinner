using Mapster;
using MommaDinner.Application.Authentication.Commands.Register;
using MommaDinner.Application.Authentication.Commom;
using MommaDinner.Application.Authentication.Queries.Login;
using MommaDinner.Contracts.Authentication;

namespace MommaDinner.Api.Commom.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();
        config.NewConfig<LoginRequest, LoginQuery>();
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Token, src => src.Token)
            .Map(dest => dest, src => src.User);
    }
}
