using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using Sport_store.Controllers;
using Sport_store.Models;
using System.Linq;

namespace Sport_store.Test
{
    public class ProductControllerTests
    {
        [Fact]
        public void Can_Paginate()
        {
            // arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product {Id = 1 , Name = "P1"},
                new Product {Id = 2 , Name = "P2"},
                new Product {Id = 3 , Name = "P3"},
                new Product {Id = 4 , Name = "P4"},
                new Product {Id = 5 , Name = "P5"}
            }).AsQueryable<Product>());

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            // act
            IEnumerable<Product> result = controller.List(2).ViewData.Model as IEnumerable<Product>;

            // assert
            Product[] prodArray = result.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.Equal("P4", prodArray[0].Name);
            Assert.Equal("P5", prodArray[1].Name);
        }


    }
}
