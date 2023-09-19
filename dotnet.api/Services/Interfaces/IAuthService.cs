using dotnet.api.Dtos.Users;
using dotnet.api.Models;

namespace dotnet.api.Services.Interfaces;

public interface IAuthService
{
	Task<User> RegisterAsync(RegisterDto user);
	Task<string> LoginAsync(LoginDto user);
}