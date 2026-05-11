using AutoFixture;
using FluentAssertions;
using InventoryAPI.Domain.DTOs.Category;
using InventoryAPI.Domain.Exceptions;
using InventoryAPI.Domain.Interfaces.Repositories;
using InventoryAPI.Domain.Services;
using InventoryAPI.Entities;
using InventoryAPI.Tests.Customizations;
using Moq;

namespace InventoryAPI.Tests.Domain;

public class CategoryServiceTests
{
    private readonly IFixture _fixture = FixtureFactory.Create();
    private readonly Mock<ICategoryRepository> _repositoryMock;
    private readonly CategoryService _service;

    public CategoryServiceTests()
    {
        _repositoryMock = _fixture.Freeze<Mock<ICategoryRepository>>();
        _service = _fixture.Create<CategoryService>();
    }

    [Fact]
    public async Task CreateAsync_WhenCategoryDoesNotExist_ShouldReturnCreatedCategory()
    {
        // Arrange
        var dto = _fixture.Create<CreateCategoryDto>();
        var category = _fixture.Create<Category>();

        _repositoryMock.Setup(r => r.ExistsByNameAsync(dto.Name))
            .ReturnsAsync(false);

        _repositoryMock.Setup(r => r.GetOrCreateAsync(It.IsAny<Category>()))
            .ReturnsAsync(category);

        // Act
        var result = await _service.CreateAsync(dto);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(category.Id);
        result.Name.Should().Be(category.Name);
    }

    [Fact]
    public async Task GetByIdAsync_WhenCategoryExists_ShouldReturnCategory()
    {
        // Arrange
        var category = _fixture.Create<Category>();

        _repositoryMock.Setup(r => r.GetByIdAsync(category.Id))
            .ReturnsAsync(category);

        // Act
        var result = await _service.GetByIdAsync(category.Id);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(category.Id);
        result.Name.Should().Be(category.Name);
    }

    [Fact]
    public async Task GetByIdAsync_WhenCategoryDoesNotExist_ShouldThrowNotFoundException()
    {
        // Arrange
        var id = Guid.NewGuid();

        _repositoryMock.Setup(r => r.GetByIdAsync(id))
            .ReturnsAsync((Category?)null);

        // Act
        var act = async () => await _service.GetByIdAsync(id);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task DeleteAsync_WhenCategoryDoesNotExist_ShouldThrowNotFoundException()
    {
        // Arrange
        var id = Guid.NewGuid();

        _repositoryMock.Setup(r => r.GetByIdAsync(id))
            .ReturnsAsync((Category?)null);

        // Act
        var act = async () => await _service.DeleteAsync(id);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task DeleteAsync_WhenCategoryExists_ShouldCallDeleteOnRepository()
    {
        // Arrange
        var category = _fixture.Create<Category>();

        _repositoryMock.Setup(r => r.GetByIdAsync(category.Id))
            .ReturnsAsync(category);

        // Act
        await _service.DeleteAsync(category.Id);

        // Assert
        _repositoryMock.Verify(r => r.DeleteAsync(category.Id), Times.Once);
    }
    
    [Fact]
    public async Task CreateAsync_WhenNameAlreadyExists_ShouldThrowDuplicateNameException()
    {
        // Arrange
        var dto = _fixture.Create<CreateCategoryDto>();

        _repositoryMock.Setup(r => r.ExistsByNameAsync(dto.Name))
            .ReturnsAsync(true);

        // Act
        var act = async () => await _service.CreateAsync(dto);

        // Assert
        await act.Should().ThrowAsync<DuplicateNameException>();
    }
}