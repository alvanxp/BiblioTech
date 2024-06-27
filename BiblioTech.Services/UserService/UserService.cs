using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BiblioTech.Data;
using BiblioTech.Domain.Dto;
using BiblioTech.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using AuthenticateResponse = BiblioTech.Domain.Dto.AuthenticateResponse;

namespace BiblioTech.Services.UserService;

public class UserService(IOptions<JwtSettings> appSettings, IUserRepository userRepository)
    : IUserService
{
    private readonly JwtSettings _jwtSettings = appSettings.Value;
    public async Task<ResultDto<AuthenticateResponse?>> Authenticate(AuthenticateRequest model)
    {
        var user = await userRepository.GetUserByUsername(model.Username);

        if (user == null)
        {
            return new ResultDto<AuthenticateResponse?>()
            {
                Success = false,
                Message = "Invalid Credentials"
            };
        }
        var hashedPassword = HashPassword(model.Password, user.Salt);
        if (hashedPassword != user.HashedPassword)
        {
            return new ResultDto<AuthenticateResponse?>()
            {
                Success = false,
                Message ="Invalid Credentials" 
            };
        }
        
        var token = GenerateJwtToken(user.Username);
        return new ResultDto<AuthenticateResponse?>()
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
        var user = new User
        {
            FirstName = registerRequest.FirstName,
            LastName = registerRequest.LastName,
            Username = registerRequest.Username,
            Salt = Guid.NewGuid().ToString()
        };

        user.HashedPassword = HashPassword(registerRequest.Password, user.Salt);
        var updatedUser = await userRepository.Insert(user);
        if (updatedUser == null)
        {
            return new ResultDto<AuthenticateResponse>
            {
                Success = false,
                Message = "User registration failed."
            };
        }

        var token = GenerateJwtToken(updatedUser.Id.ToString());
        return new ResultDto<AuthenticateResponse>
        {
            Success = true,
            Message = "User registered successfully.",
            Data = new AuthenticateResponse()
            {
                FirstName = updatedUser.FirstName,
                LastName = updatedUser.LastName,
                Username = updatedUser.Username,
                Token = token
            }
        };
    }
    private string HashPassword(string password, string salt)
    {
        using var sha256 = System.Security.Cryptography.SHA256.Create();
        var saltedPassword = salt + password;
        var bytes = System.Text.Encoding.UTF8.GetBytes(saltedPassword);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    } 

    private string GenerateJwtToken(string username)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}