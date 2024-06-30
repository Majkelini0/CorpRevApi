using EvilCorp.DTOs.LoginDTOs;
using EvilCorp.Services.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvilCorp.Controllers;

[ApiController]
[Route("EvilCorp")]
public class LoginController : ControllerBase
{
    private readonly ILoginService _loginService;

    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }
    
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(RegisterRequest request)
    {
        await _loginService.RegisterUser(request);

        return Ok("User registered");
    }
    
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> LoginPatient(LoginRequest request)
    {
        var token = await _loginService.LoginUser(request);

        return Ok(new
        {
            accesToken = token.Item1,
            refreshToken = token.Item2
        });
    }
    
    [AllowAnonymous]
    [HttpPost("refresh")]
    [Authorize(AuthenticationSchemes = "IgnoreTokenExpirationScheme")]
    public async Task<IActionResult> Refresh(RefreshTokenRequest refreshToken)
    {
        var token = await _loginService.RefreshToken(refreshToken);

        return Ok(new
        {
            accesToken = token.Item1,
            refreshToken = token.Item2
        });
    }
}