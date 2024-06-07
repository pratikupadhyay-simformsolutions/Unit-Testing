using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Unit_Testing.Controllers;
using Unit_Testing.Models;
using Xunit;

namespace Unit_Testing.Tests
{
    public class ProductControllerTests
    {
        private readonly List<Product> _testProducts;

        public ProductControllerTests()
        {
            _testProducts = GetTestProducts();
        }

        [Fact]
        public void GetAllProducts_ShouldReturnAllProducts()
        {
            // Arrange
            var controller = new ProductController(_testProducts);

            // Act
            var result = controller.GetAllProducts();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkNegotiatedContentResult<List<Product>>>(result);
            // Assert
            var okResult = Assert.IsType<OkNegotiatedContentResult<List<Product>>>(result);
            Assert.NotNull(okResult.Content);
            Assert.Equal(_testProducts.Count, okResult.Content.Count);

        }

        [Fact]
        public async Task GetAllProductsAsync_ShouldReturnAllProducts()
        {
            // Arrange
            var controller = new ProductController(_testProducts);

            // Act
            var result = await controller.GetAllProductsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkNegotiatedContentResult<List<Product>>>(result);
            var okResult = Assert.IsType<OkNegotiatedContentResult<List<Product>>>(result);
            Assert.NotNull(okResult.Content);
            Assert.Equal(_testProducts.Count, okResult.Content.Count);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        public void GetProductById_ShouldReturnCorrectProduct(int productId)
        {
            // Arrange
            var controller = new ProductController(_testProducts);

            // Act
            var result = controller.GetProductById(productId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkNegotiatedContentResult<Product>>(result);
            // Assert
            var okResult = Assert.IsType<OkNegotiatedContentResult<Product>>(result);
            var contentResult = Assert.IsType<Product>(okResult.Content);
            Assert.Equal(productId, contentResult.Id);
            Assert.Equal(_testProducts[productId - 1].Name, contentResult.Name);

        }

        [Fact]
        public void GetProductById_ShouldReturnNotFound()
        {
            // Arrange
            var controller = new ProductController(_testProducts);

            // Act
            var result = controller.GetProductById(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        private static List<Product> GetTestProducts()
        {
            return new List<Product>
            {
                new() { Id = 1, Name = "Bike", Price = 50000 },
                new() { Id = 2, Name = "Car", Price = 500000 },
                new() { Id = 3, Name = "Truck", Price = 350000 },
                new() { Id = 4, Name = "Cycle", Price = 10000 }
            };
        }
    }
}
