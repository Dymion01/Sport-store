using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using Sport_store.Controllers;
using Sport_store.Models;
using System.Linq;
using Sport_store.Models.ViewModels;

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
            ProductsListViewModel result = controller.List(null,2).ViewData.Model as ProductsListViewModel;

            // assert
            Product[] prodArray = result.Products.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.Equal("P4", prodArray[0].Name);
            Assert.Equal("P5", prodArray[1].Name);
        }

        [Fact]
        public void Can_Send_Pagination_View_Model()
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

            //arrange
            ProductController controller = new ProductController(mock.Object) { PageSize = 3 };

            // act
            ProductsListViewModel result = controller.List(null,2).ViewData.Model as ProductsListViewModel;

            //assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(5, pageInfo.TotalItems);
            Assert.Equal(2, pageInfo.TotalPages);
        }

        [Fact]
        public void Can_Fillter_Products()
        {
            // arrange
            
             Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[5] {
                new Product { Id = 1, Name = "P1", Category = {CategoryId =1 ,Name = "Cat1" } },
                new Product { Id = 2, Name = "P2", Category = {CategoryId =2 ,Name = "Cat2" } },
                new Product { Id = 3, Name = "P3", Category = {CategoryId =1 ,Name = "Cat1" } },
                new Product { Id = 4, Name = "P4", Category = {CategoryId =2 ,Name = "Cat2" } },
                new Product { Id = 5, Name = "P5", Category = {CategoryId =3 ,Name = "Cat3" } }
            }).AsQueryable<Product>());


            // arrange 
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            // act
            Product[] result = (controller.List("Cat2", 1).ViewData.Model as ProductsListViewModel).Products.ToArray();

            // assert
            Assert.Equal(2, result.Length);
            Assert.True(result[0].Name == "P2" && result[0].Category.Name == "Cat2");
            Assert.True(result[1].Name == "P4" && result[1].Category.Name == "Cat2");
        }
    }
}
