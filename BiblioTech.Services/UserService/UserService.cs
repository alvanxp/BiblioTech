using System.Security.Cryptography;
using BiblioTech.Data;
using BiblioTech.Domain.Dto;
using BiblioTech.Domain.Entities;
using BiblioTech.Services.Authentication;
using Microsoft.Extensions.Options;
using AuthenticateResponse = BiblioTech.Domain.Dto.AuthenticateResponse;

namespace BiblioTech.Services.UserService;

public class UserService(IUserRepository userRepository, AuthenticationService authenticationService)
    : IUserService
{
    public async Task<ResultDto<AuthenticateResponse>> Authenticate(AuthenticateRequest model)
    {
        var user = await userRepository.GetUserByUsername(model.Username);

        if (user == null || !authenticationService.VerifyPassword(model.Password, user.HashedPassword, user.Salt))
        {
            return new ResultDto<AuthenticateResponse>()
            {
                Success = false,
                Message = "Invalid Credentials"
            };
        }

        var token = authenticationService.GenerateJwtToken(user.Username);
        return new ResultDto<AuthenticateResponse>()
        {
            Success = true,
            Data = new AuthenticateResponse()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Token = token
            }
        };
    }

    public async Task<ResultDto<AuthenticateResponse>> Register(RegisterRequest registerRequest)
    {
        var existingUser = await userRepository.GetUserByUsername(registerRequest.Username);
        if (existingUser != null)
        {
            return new ResultDto<AuthenticateResponse>
            {
                Success = false,
                Message = "Username is already taken."
            };
        }

        var salt = Convert.ToBase64String(RandomNumberGenerator.GetBytes(16));
        var user = new User
        {
            FirstName = registerRequest.FirstName,
            LastName = registerRequest.LastName,
            Username = registerRequest.Username,
            Salt = salt,
            HashedPassword = authenticationService.HashPassword(registerRequest.Password, salt)
        };

        var newUser = await userRepository.Insert(user);
        if (newUser == null)
        {
            return new ResultDto<AuthenticateResponse>
            {
                Success = false,
                Message = "User registration failed."
            };
        }

        var token = authenticationService.GenerateJwtToken(newUser.Username);
        return new ResultDto<AuthenticateResponse>
        {
            Success = true,
            Message = "User registered successfully.",
            Data = new AuthenticateResponse()
            {
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Username = newUser.Username,
                Token = token
            }
        };
    }


}