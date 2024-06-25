using BiblioTech.Domain.Dto;

namespace BiblioTech.Services;

public interface IUserService
{
    Task<ResultDto<AuthenticateResponse?>> Authenticate(AuthenticateRequest model);

    Task<ResultDto<AuthenticateResponse>> Register(RegisterRequest registerRequest);
}