using EvilCorp.DTOs.LoginDTOs;
using EvilCorp.Services.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvilCorp.Controllers;

[ApiController]
[Route("EvilCorp")]
public class LoginController : ControllerBase
{
    private readonly ILoginService _service;

    public LoginController(ILoginService service)
    {
        _service = service;
    }
    
    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult RegisterUser(RegisterRequest request)
    {
        _service.RegisterUser(request);

        return Ok("User registered");
    }
    
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> LoginPatient(LoginRequest request)
    {
        var token = await _service.LoginUser(request);

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
        var token = await _service.RefreshToken(refreshToken);

        return Ok(new
        {
            accesToken = token.Item1,
            refreshToken = token.Item2
        });
    }

    [AllowAnonymous]
    [HttpGet("test")]
    public IActionResult LoginTest()
    {
        //var claimsFromAccessToken = User.Claims;

        return Ok(_service.GetTestData());
    }
}