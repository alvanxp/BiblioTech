using BiblioTech.Domain.Dto;
using BiblioTech.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BiblioTech.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController(IUserService userService) : ControllerBase
{
    // POST
    [HttpPost("authenticate")]
    [SwaggerOperation("Authenticate")]
    public async Task<IActionResult> Authenticate(AuthenticateRequest model)
    {
        var response = await userService.Authenticate(model);
        if (!response.Success)
            return BadRequest(new { message = response.Message });

        return Ok(response.Data);
    }
    
    // POST
    [HttpPost("register")]
    [SwaggerOperation("Register")]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        var response = await userService.Register(registerRequest);
        if (!response.Success)
            return BadRequest(new { message = response.Message });
        return Ok(response.Data);
    }
}