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
        var profile = new DomainToResponse();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
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
            options => options.ComparingByMembers<Item>());
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