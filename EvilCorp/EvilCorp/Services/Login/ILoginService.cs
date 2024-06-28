using EvilCorp.DTOs.LoginDTOs;

namespace EvilCorp.Services.Login;

public interface ILoginService
{
    public Task RegisterUser(RegisterRequest request);

    public Task<Tuple<string, string>> LoginUser(LoginRequest request);

    public Task<Tuple<string, string>> RefreshToken(RefreshTokenRequest refreshToken);

    public string GetTestData();
}