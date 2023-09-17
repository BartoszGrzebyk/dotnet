using dotnet.api.Data;
using dotnet.api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace dotnet.api.Repositories;

public class GenericRepository<T> where T : class
{
	protected AppDbContext context;
	internal DbSet<T> dbSet;
	
	public GenericRepository (
		AppDbContext context
	){
		this.context = context;
		dbSet = context.Set<T>();
	}
}