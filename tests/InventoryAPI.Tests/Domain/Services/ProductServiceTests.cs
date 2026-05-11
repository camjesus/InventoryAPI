using AutoFixture;
using FluentAssertions;
using InventoryAPI.Domain.DTOs.Category;
using InventoryAPI.Domain.DTOs.Product;
using InventoryAPI.Domain.Exceptions;
using InventoryAPI.Domain.Interfaces.Repositories;
using InventoryAPI.Domain.Interfaces.Services;
using InventoryAPI.Domain.Services;
using InventoryAPI.Entities;
using InventoryAPI.Tests.Customizations;
using Moq;

namespace InventoryAPI.Tests.Domain;

public class ProductServiceTests
{
    private readonly IFixture _fixture = FixtureFactory.Create();
    private readonly Mock<IProductRepository> _repositoryMock;
    private readonly Mock<ICategoryService> _categoryServiceMock;
    private readonly ProductService _service;

    public ProductServiceTests()
    {
        _repositoryMock = _fixture.Freeze<Mock<IProductRepository>>();
        _categoryServiceMock = _fixture.Freeze<Mock<ICategoryService>>();
        _service = _fixture.Create<ProductService>();
    }

    [Fact]
    public async Task CreateAsync_WhenIsANew_ShouldReturnCreatedProduct()
    {
        // Arrange
        var dto = _fixture.Create<CreateProductDto>();
        var product = _fixture.Create<Product>();

        _repositoryMock.Setup(r => r.ExistsBySkuAsync(dto.SKU))
            .ReturnsAsync(false);
        _categoryServiceMock.Setup(s => s.GetByIdAsync(dto.CategoryId))
            .ReturnsAsync(_fixture.Create<CategoryResponseDto>());
        _repositoryMock.Setup(r => r.GetOrCreateAsync(It.IsAny<Product>()))
            .ReturnsAsync(product);

        // Act
        var result = await _service.CreateAsync(dto);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(product.Id);
    }

    [Fact]
    public async Task CreateAsync_WhenSkuExists_ShouldThrowDuplicateSkuException()
    {
        // Arrange
        var dto = _fixture.Create<CreateProductDto>();

        _repositoryMock.Setup(r => r.ExistsBySkuAsync(dto.SKU))
            .ReturnsAsync(true);

        // Act
        var act = async () => await _service.CreateAsync(dto);

        // Assert
        await act.Should().ThrowAsync<DuplicateSkuException>();
    }

    [Fact]
    public async Task CreateAsync_WhenPriceIsNegative_ShouldThrowInvalidPriceException()
    {
        // Arrange
        var dto = new CreateProductDto("Monitor", null, "MON-001", -100, 10, Guid.NewGuid());

        _repositoryMock.Setup(r => r.ExistsBySkuAsync(dto.SKU))
            .ReturnsAsync(false);

        // Act
        var act = async () => await _service.CreateAsync(dto);

        // Assert
        await act.Should().ThrowAsync<InvalidPriceException>();
    }

    [Fact]
    public async Task CreateAsync_WhenStockIsNegative_ShouldThrowInvalidStockException()
    {
        // Arrange
        var dto = new CreateProductDto("Monitor", null, "MON-001", 100, -10, Guid.NewGuid());

        _repositoryMock.Setup(r => r.ExistsBySkuAsync(dto.SKU))
            .ReturnsAsync(false);

        // Act
        var act = async () => await _service.CreateAsync(dto);

        // Assert
        await act.Should().ThrowAsync<InvalidStockException>();
    }

    [Fact]
    public async Task CreateAsync_WhenCategoryNotExist_ShouldThrowNotFoundException()
    {
        // Arrange
        var dto = _fixture.Create<CreateProductDto>();

        _repositoryMock.Setup(r => r.ExistsBySkuAsync(dto.SKU))
            .ReturnsAsync(false);
        _categoryServiceMock.Setup(s => s.GetByIdAsync(dto.CategoryId))
            .ThrowsAsync(new NotFoundException("Category", dto.CategoryId));

        // Act
        var act = async () => await _service.CreateAsync(dto);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task GetByIdAsync_WhenProductExists_ShouldReturnProduct()
    {
        //IDENMPOTENCIA
        // Arrange
        var product = _fixture.Create<Product>();

        _repositoryMock.Setup(r => r.GetByIdAsync(product.Id))
            .ReturnsAsync(product);

        // Act
        var result = await _service.GetByIdAsync(product.Id);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(product.Id);
    }

    [Fact]
    public async Task GetByIdAsync_WhenProductNotExist_ShouldThrowNotFoundException()
    {
        // Arrange
        var id = Guid.NewGuid();

        _repositoryMock.Setup(r => r.GetByIdAsync(id))
            .ReturnsAsync((Product?)null);

        // Act
        var act = async () => await _service.GetByIdAsync(id);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task DeleteAsync_WhenProductExists_ShouldCallDeleteOnRepository()
    {
        // Arrange
        var product = _fixture.Create<Product>();

        _repositoryMock.Setup(r => r.GetByIdAsync(product.Id))
            .ReturnsAsync(product);

        // Act
        await _service.DeleteAsync(product.Id);

        // Assert
        _repositoryMock.Verify(r => r.DeleteAsync(product.Id), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WhenProductNotExist_ShouldThrowNotFoundException()
    {
        // Arrange
        var id = Guid.NewGuid();

        _repositoryMock.Setup(r => r.GetByIdAsync(id))
            .ReturnsAsync((Product?)null);

        // Act
        var act = async () => await _service.DeleteAsync(id);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}