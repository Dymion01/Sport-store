using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Sport_store.Models;
using Sport_store.Controllers;
using Xunit;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

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
        [Fact]
        public void Can_Edit_Product()
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
            Product p1 = GetViewModel<Product>(target.Edit(1));
            Product p2 = GetViewModel<Product>(target.Edit(2));
            Product p3 = GetViewModel<Product>(target.Edit(3));
            //assert
            Assert.Equal(1, p1.Id);
            Assert.Equal(2, p2.Id);
            Assert.Equal(3, p3.Id);

        }
        
        [Fact]
        public void Cannot_Edit_Nonexistent_Product()
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
            Product result = GetViewModel<Product>(target.Edit(4));
            //assert
            Assert.Null(result);
        }
           private T GetViewModel<T>(IActionResult result) where T : class
            {
                return (result as ViewResult)?.ViewData.Model as T;
            }  
        
        [Fact]
        public void Can_Save_Valid_Changes()
        {
            //arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            Mock<ITempDataDictionary> tempData = new Mock<ITempDataDictionary>();

            AdminController target = new AdminController(mock.Object)
            {
                TempData = tempData.Object
            };
            Product product = new Product { Name = "Test" };

            //act
            IActionResult result = target.Edit(product);
            //assert
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", (result as RedirectToActionResult).ActionName);
            
        }

        [Fact]
        public void Cannot_Save_Invalid_Changes()
        {
            //arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            AdminController target = new AdminController(mock.Object);

            Product product = new Product { Name = "Test" };

            target.ModelState.AddModelError("error", "error");

            //act
            IActionResult result = target.Edit(product);

            //assert
            mock.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never());

            Assert.IsType<ViewResult>(result);

        }

        [Fact]
        public void Can_Delete_Valid_Products()
        {
            //arrange
            Product product = new Product { Id =2 , Name = "Test" };

            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
           {
                new Product {Id = 1 , Name = "P1"},
                product,
                new Product {Id = 3 , Name = "P3"},
           }.AsQueryable<Product>()) ;

            AdminController target = new AdminController(mock.Object);

            //act
            target.Delete(product.Id);

            //assert
            mock.Verify(m => m.DeleteProduct(product.Id));
        }
    }
}
