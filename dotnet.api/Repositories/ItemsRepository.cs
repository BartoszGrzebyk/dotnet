using dotnet.api.Data;
using dotnet.api.Dtos.Items;
using dotnet.api.Models;
using dotnet.api.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace dotnet.api.Repositories;

public class ItemsRepository : GenericRepository<Item>, IItemsRepository
{
    public ItemsRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Item> CreateItemAsync(Item item)
    {
        await context.Items.AddAsync(item);
        await context.SaveChangesAsync();
        return await context.Items.FindAsync(item.Id);
    }

    public async Task<Item> DeleteItemAsync(int Id)
    {
        var item = await context.Items.FindAsync(Id);
        if(item is null) return null;

        context.Items.Remove(item);
        await context.SaveChangesAsync();
        return item;
    }

    public async Task<Item> GetItemAsync(int id)
    {
        var item = await context.Items.FindAsync(id);
        if (item == null) return null;

        return item;
    }

    public async Task<IEnumerable<Item>> GetItemsAsync()
    {
        return await context.Items.ToListAsync();
    }

    public async Task<Item> UpdateItemAsync(Item item)
    {
        var result = await context.Items.FindAsync(item.Id);

        if(result is null) return null;

        result.Name = item.Name;
        result.Description = item.Description;
        await context.SaveChangesAsync();

        return result;
    }
}