using Microsoft.EntityFrameworkCore;
using Modelo.Models;
using Modelo.ViewModels;
using Servicio.Services;

public class EcommerceContextInMemory : EcommerceContext
{
    public EcommerceContextInMemory(DbContextOptions<EcommerceContext> options)
        : base(options)
    {
    }
}

public class ProductServiceTests
{
    [Fact]
    public void AddNewProduct_WithValidData_ShouldReturnProductDTO()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<EcommerceContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
            .Options;

        using (var context = new EcommerceContextInMemory(options))
        {
            var productService = new ProductService(context);
            var productViewModel = new ProductViewModel
            {
                Brand = "Example Brand",
                Model = "Example Model",
                Storage = 256,
                Ram = 8,
                Description = "Example Description",
                Price = 499.99M,
                // Establece otras propiedades según sea necesario.
            };

            // Act
            var result = productService.AddNewProduct(productViewModel);

            // Assert
            Assert.NotNull(result);
            // Agrega aserciones para las propiedades de result si es necesario.
        }
    }
}
