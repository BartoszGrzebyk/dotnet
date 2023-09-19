using dotnet.api.Data;
using dotnet.api.Models;
using dotnet.api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace dotnet.api.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<User> AddUserAsync(User user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        
        return await context.Users.FindAsync(user.Id);
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await context.Users.SingleOrDefaultAsync(usr => usr.Email == email);
    }
}