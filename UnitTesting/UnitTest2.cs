using Microsoft.EntityFrameworkCore;
using Modelo.Models;
using Modelo.ViewModels;
using Servicio.Services;
using System;
using System.Linq;
using Xunit;

public class ProductServiceUpdateTests
{
    [Fact]
    public void UpdateProduct_ShouldUpdateProductCorrectly()
    {
        // Verificacion de que el metodo update funciona corectamente utilizando xUnit y InMemory para simular una base de datos de pruebas a base del context
        var options = new DbContextOptionsBuilder<EcommerceContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
            .Options;

        using (var context = new EcommerceContextInMemory(options))
        {

            var initialProduct = new Product
            {
                Brand = "Initial Brand",
                Model = "Initial Model",
                Storage = 128,
                Ram = 4,
                Description = "Initial Description",
                Price = 299.99M,
            };
            context.Product.Add(initialProduct);
            context.SaveChanges();

            var productService = new ProductService(context);
            var updatedProductViewModel = new ProductViewModel
            {
                Brand = "Updated Brand",
                Model = "Updated Model",
                Storage = 256,
                Ram = 8,
                Description = "Updated Description",
                Price = 399.99M,
            };

            var result = productService.UpdateProduct(initialProduct.Id, updatedProductViewModel);

            Assert.NotNull(result);
            Assert.Equal(updatedProductViewModel.Brand, result.Brand);
            Assert.Equal(updatedProductViewModel.Model, result.Model);
            Assert.Equal(updatedProductViewModel.Storage, result.Storage);
            Assert.Equal(updatedProductViewModel.Ram, result.Ram);
            Assert.Equal(updatedProductViewModel.Description, result.Description);
            Assert.Equal(updatedProductViewModel.Price, result.Price);

        }
    }
}
