using dotnet.api.Dtos.Users;
using dotnet.api.Models;

namespace dotnet.api.Repositories.Interfaces;

public interface IUserRepository
{
	Task<User> GetUserByEmailAsync(string email);
	Task<User> AddUserAsync(User user);
}