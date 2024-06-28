using EvilCorp.DTOs.LoginDTOs;
using EvilCorp.Services.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvilCorp.Controllers;

[ApiController]
[Route("user")]
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

    [Authorize]
    [HttpGet]
    public IActionResult GetPatients()
    {
        //var claimsFromAccessToken = User.Claims;

        return Ok(_service.GetTestData());
    }
}