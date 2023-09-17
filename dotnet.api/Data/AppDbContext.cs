using dotnet.api.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet.api.Data;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

	public DbSet<User> Users {get; set;}
	public DbSet<Item> Items {get; set;}
}