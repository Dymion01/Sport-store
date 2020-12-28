using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Sport_store.Models;
using Sport_store.Controllers;
using Xunit;

namespace Sport_store.Test
{
    public class AdminControllerTests
    {
        [Fact]
        public void Index_Contains_All_Products()
        {
            //arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {Id = 1 , Name = "P1"},
                new Product {Id = 2 , Name = "P2"},
                new Product {Id = 3 , Name = "P3"},
            }.AsQueryable<Product>());

            AdminController target = new AdminController(mock.Object);

            //act
            Product[] result = GetViewModel<IEnumerable<Product>>(target.Index())?.ToArray();
            //assert
            Assert.Equal(3, result.Length);
            Assert.Equal("P1", result[0].Name);
            Assert.Equal("P2", result[1].Name);
            Assert.Equal("P3", result[2].Name);
        }
           private T GetViewModel<T>(IActionResult result) where T : class
            {
                return (result as ViewResult)?.ViewData.Model as T;
            }               
    }
}
