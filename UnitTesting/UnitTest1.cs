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
        // Verificacion de que el metodo create funciona corectamente utilizando xUnit y InMemory para simular una base de datos de pruebas a base del context
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

            };


            var result = productService.AddNewProduct(productViewModel);


            Assert.NotNull(result);

        }
    }
}
