using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EvilCorp.Context;
using EvilCorp.DTOs.LoginDTOs;
using EvilCorp.Helpers;
using EvilCorp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EvilCorp.Services.Login;

public class LoginService : ILoginService
{
    private readonly IConfiguration _configuration;
    private readonly EvilCorpContext _context;

    public LoginService(IConfiguration configuration, EvilCorpContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    public async Task RegisterUser(RegisterRequest request)
    {
        var hashedPasswordAndSalt = SecurityHelper.GetHashedPasswordAndSalt(request.Password);

        var user = new User()
        {
            Login = request.Login,
            Password = hashedPasswordAndSalt.Item1,
            Salt = hashedPasswordAndSalt.Item2,
            RefreshToken = SecurityHelper.GenerateRefreshToken(),
            RefreshTokenExpTime = DateTime.Now.AddDays(1)
        };

        await _context.User.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<Tuple<string, string>> LoginUser(LoginRequest request)
    {
        var user = await _context.User.Where(u => u.Login == request.Login).FirstOrDefaultAsync();
        
        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid credentials.");
        }

        string passwordHashFromDb = user.Password;
        string curHashedPassword = SecurityHelper.GetHashedPasswordWithSalt(request.Password, user.Salt);

        if (passwordHashFromDb != curHashedPassword)
        {
            throw new UnauthorizedAccessException("Invalid credentials.");
        }

        Claim[] userclaim = new[]
        {
            new Claim(ClaimTypes.Name, user.Login)
        };
        
        if(request.Login == "secretuser" && request.Password == "secretpassword")
        {
            userclaim = new[]
            {
                new Claim(ClaimTypes.Role, "admin")
            };
        }
        else
        {
            userclaim = new[]
            {
                new Claim(ClaimTypes.Role, "stduser")
            };
        }
        

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            claims: userclaim,
            expires: DateTime.Now.AddMinutes(10), // // //
            signingCredentials: creds
        );

        user.RefreshToken = SecurityHelper.GenerateRefreshToken();
        user.RefreshTokenExpTime = DateTime.Now.AddDays(1);
        await _context.SaveChangesAsync();

        return new Tuple<string, string>(new JwtSecurityTokenHandler().WriteToken(token), user.RefreshToken);
    }

    public async Task<Tuple<string, string>> RefreshToken(RefreshTokenRequest refreshToken)
    {
        User user = _context.User.Where(u => u.RefreshToken == refreshToken.RefreshToken).FirstOrDefault();
        if (user == null)
        {
            throw new SecurityTokenException("Invalid refresh token");
        }

        if (user.RefreshTokenExpTime < DateTime.Now)
        {
            throw new SecurityTokenException("Refresh token expired");
        }

        Claim[] userclaim = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()),
            new Claim(ClaimTypes.Name, user.Login)
        };

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken jwtToken = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            claims: userclaim,
            expires: DateTime.Now.AddMinutes(10), // // //
            signingCredentials: creds
        );

        user.RefreshToken = SecurityHelper.GenerateRefreshToken();
        user.RefreshTokenExpTime = DateTime.Now.AddDays(1);
        _context.SaveChanges();

        return new Tuple<string, string>(new JwtSecurityTokenHandler().WriteToken(jwtToken), user.RefreshToken);
    }

    public string GetTestData()
    {
        return "Secret data here you are";
    }
}