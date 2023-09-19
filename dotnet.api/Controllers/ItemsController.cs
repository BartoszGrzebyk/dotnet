using AutoMapper;
using dotnet.api.Dtos.Items;
using dotnet.api.Models;
using dotnet.api.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet.api.Controllers;

public class ItemsController : BaseController
{
    private readonly IItemsRepository itemsRepository;

    public ItemsController(IMapper mapper, IItemsRepository itemsRepository) : base(mapper)
    {
        this.itemsRepository = itemsRepository;
    }

    [HttpPost]
    public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto createItemDto)
    {
        var item = mapper.Map<Item>(createItemDto);

        var result = await itemsRepository.CreateItemAsync(item);

        var itemDto = mapper.Map<ItemDto>(result);

        return CreatedAtAction(
            nameof(GetItemAsync),
            new {id = itemDto.Id},
            itemDto
        );
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ItemDto>> GetItemAsync(int id)
    {
        var item = await itemsRepository.GetItemAsync(id);
        if(item is null) return NotFound();

        return mapper.Map<ItemDto>(item);
    }

    [HttpGet, Authorize(Roles = "Admin")]
    public async Task<IEnumerable<ItemDto>> GetItemsAsync()
    {
        var items = await itemsRepository.GetItemsAsync();

        return mapper.Map<IEnumerable<ItemDto>>(items);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateItemAsync(UpdateItemDto updateItemDto)
    {
        var item = mapper.Map<Item>(updateItemDto);

        var result = await itemsRepository.UpdateItemAsync(item);

        if(result is null) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteItemAsync(int Id)
    {
        var result = await itemsRepository.DeleteItemAsync(Id);
        if(result is null) return NotFound();

        return NoContent();
    }
}