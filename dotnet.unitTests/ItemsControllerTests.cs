using AutoMapper;
using dotnet.api.Controllers;
using dotnet.api.Dtos.Items;
using dotnet.api.MappingProfiles;
using dotnet.api.Models;
using dotnet.api.Repositories.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace dotnet.unitTests;

public class ItemsControllerTests
{
    private readonly Mock<IItemsRepository> repositoryStub = new();
    private readonly Mock<ILogger<ItemsController>> loggerStub = new();
    private readonly IMapper mapperStub;

    public ItemsControllerTests()
    {
        var requestToDomainProfile = new RequestToDomain();
        var domainToResponseProfile = new DomainToResponse();
        var configuration = new MapperConfiguration(cfg => {
            cfg.AddProfile(requestToDomainProfile);
            cfg.AddProfile(domainToResponseProfile);
        });
        mapperStub = new Mapper(configuration);
    }

    private readonly Random rand = new();



    [Fact]
    public async Task GetItemAsync_UnexistingItem_ReturnsNotFound()
    {
        repositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<int>()))
            .ReturnsAsync((Item)null);
        
        var controller = new ItemsController(mapperStub, loggerStub.Object, repositoryStub.Object);

        var result = await controller.GetItemAsync(rand.Next());

        Assert.IsType<NotFoundResult>(result.Result);
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task GetItemAsync_ExistingItem_ReturnsExpectedItem()
    {
        var expectedItem = CreateRandomItem();
        
        repositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<int>()))
            .ReturnsAsync(expectedItem);

        var controller = new ItemsController(mapperStub, loggerStub.Object, repositoryStub.Object);
    
        var result = await controller.GetItemAsync(rand.Next());
    
        result.Value.Should().BeEquivalentTo(expectedItem,
            options => options.ComparingByMembers<ItemDto>());
    }

    [Fact]
    public async Task GetItemsAsync_WithExistingItem_ReturnsAllItems()
    {
        var expectedItems = new[]{CreateRandomItem(), CreateRandomItem(), CreateRandomItem()};

        repositoryStub.Setup(repo => repo.GetItemsAsync()).ReturnsAsync(expectedItems);

        var controller = new ItemsController(mapperStub, loggerStub.Object, repositoryStub.Object);

        var actualItems = await controller.GetItemsAsync();

        actualItems.Should().BeEquivalentTo(expectedItems,
        options => options.ComparingByMembers<ItemDto>());
    }

    [Fact]
    public async Task CreateItemsAsync_WithNewItem_ReturnsCreatedItem()
    {
        var itemToCreate = new CreateItemDto(){
            Name = Guid.NewGuid().ToString(),
            Description = Guid.NewGuid().ToString()
        };

        repositoryStub.Setup(repo => repo.CreateItemAsync(It.IsAny<Item>()))
            .ReturnsAsync((Item item) => {
                item.Id = It.IsAny<int>();
                item.Name = itemToCreate.Name;
                item.Description = itemToCreate.Description;
                return item;
            });

        var controller = new ItemsController(mapperStub, loggerStub.Object, repositoryStub.Object);

        var result = await controller.CreateItemAsync(itemToCreate);

        var createdItem = (result.Result as CreatedAtActionResult).Value as ItemDto;

        itemToCreate.Should().BeEquivalentTo(
            createdItem,
            options => options.ComparingByMembers<ItemDto>().ExcludingMissingMembers()
        );
        Assert.IsType<int>(createdItem.Id);
    }

    [Fact]
    public async Task UpdateItemAsync_WithExistingItem_ReturnsNoContent()
    {
        Item existingItem = CreateRandomItem();

        var itemToUpdate = new UpdateItemDto(){
            Id = existingItem.Id,
            Name = Guid.NewGuid().ToString(),
            Description = Guid.NewGuid().ToString()
        };

        repositoryStub.Setup(repo => repo.UpdateItemAsync(It.IsAny<Item>()))
            .ReturnsAsync(existingItem);

        var controller = new ItemsController(mapperStub, loggerStub.Object, repositoryStub.Object);

        var result = await controller.UpdateItemAsync(itemToUpdate);

        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task UpdateItemAsync_WithNotExistingItem_ReturnsNotFound()
    {
        var itemToUpdate = new UpdateItemDto(){
            Id = rand.Next(),
            Name = Guid.NewGuid().ToString(),
            Description = Guid.NewGuid().ToString()
        };

        repositoryStub.Setup(repo => repo.UpdateItemAsync(It.IsAny<Item>()))
            .ReturnsAsync((Item)null);

        var controller = new ItemsController(mapperStub, loggerStub.Object, repositoryStub.Object);

        var result = await controller.UpdateItemAsync(itemToUpdate);

        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task DeleteItemAsync_WithNotExistingItem_ReturnsNotFound()
    {
        repositoryStub.Setup(repo => repo.DeleteItemAsync(It.IsAny<int>()))
            .ReturnsAsync((Item)null);

        var controller = new ItemsController(mapperStub, loggerStub.Object, repositoryStub.Object);

        var result = await controller.DeleteItemAsync(rand.Next());

        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task DeleteItemAsync_WithExistingItem_ReturnsNotFound()
    {
        var existingItem = CreateRandomItem();

        repositoryStub.Setup(repo => repo.DeleteItemAsync(It.IsAny<int>()))
            .ReturnsAsync(existingItem);

        var controller = new ItemsController(mapperStub, loggerStub.Object, repositoryStub.Object);

        var result = await controller.DeleteItemAsync(rand.Next());

        result.Should().BeOfType<NoContentResult>();
    }

    private Item CreateRandomItem()
    {
        return new() {
            Id = rand.Next(),
            Name = Guid.NewGuid().ToString(),
            Description = Guid.NewGuid().ToString(),
        };
    }
}