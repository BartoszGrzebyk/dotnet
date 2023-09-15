using Microsoft.EntityFrameworkCore;

namespace dotnet.api.Data;

public class PostgresDbContext : DbContext
{
	protected readonly IConfiguration configuration;

	public PostgresDbContext(IConfiguration configuration) {
		this.configuration = configuration;
	}

	protected override void OnConfiguring(DbContextOptionsBuilder options){
		options.UseNpgsql(configuration.GetConnectionString("PostgresDatabase"));
	}

	public DbSet<User> Users {get; set;}
}