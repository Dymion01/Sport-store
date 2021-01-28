using Moq;
using Sport_store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Microsoft.EntityFrameworkCore.InMemory;

using Microsoft.EntityFrameworkCore;

namespace Sport_store.Test
{
    public class EFProductRepositoryTests
    {
        [Fact]
        public void Editing_Product_Exist()
        {
            //arrange 
            //var products = new Product[]
            //{
            //    new Product {Id = 1 , Name = "P1"},
            //    new Product {Id = 2 , Name = "P2"},
            //    new Product {Id = 3 , Name = "P3"},
            //}.AsQueryable<Product>();
            //var productsMock = new Mock<DbSet<Product>>();
            //productsMock.Setup(x => x.AddRange(products));
            //var options = new Mock<DbContextOptions<AppDbContext>>();


            //var appContextMock = new Mock<AppDbContext>(options.Object) ;
            //appContextMock.Setup(x => x.Set<Product>()).Returns(productsMock.Object);


            //appContextMock.Setup(x => x.Products).Returns(productsMock.Object);
            //var eFProductRepository = new EFProductRepository(appContextMock.Object);

            //eFProductRepository.SaveProduct(new Product { Id = 1, Name = "zmienione" });
            //var result = eFProductRepository.Products.FirstOrDefault(m => m.Id == 1);
            //Assert.Equal("zmienione", result.Name);

           
            var builder = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "C:/USERS/DYMION/SOURCE/REPOS/SPORT STORE/SPORT STORE.TEST/TESTSDATABASE.MDF").Options;

            using (var context = new AppDbContext(builder))
            {
                var ef = new EFProductRepository(context);
                var products = new Product[]
                    {
                        new Product {Id = 1 , Name = "P1"},
                        new Product {Id = 2 , Name = "P2"},
                        new Product {Id = 3 , Name = "P3"},
                    }.AsQueryable<Product>();

                context.Products.AddRange(products);
                context.SaveChanges();
                ef.SaveProduct(new Product { Id = 1, Name = "zmienione" });
                var result = ef.Products.FirstOrDefault(m => m.Id == 1);
                Assert.Equal("zmienione", result.Name);
            }
            //var mock = MockDbSetup.MockAppDbContext();
            //var ef = new EFProductRepository(mock.Object);
            //ef.SaveProduct(new Product { Id = 1, Name = "zmienione" });
            //var result = ef.Products.FirstOrDefault(m => m.Id == 1);
            //Assert.Equal("zmienione", result.Name);





        }

    }
    //public class MockDbSetup
    //{
    //    public static Mock<AppDbContext> MockAppDbContext()
    //    {
    //        var products = new Product[]
    //        {
    //            new Product {Id = 1 , Name = "P1"},
    //            new Product {Id = 2 , Name = "P2"},
    //            new Product {Id = 3 , Name = "P3"},
    //        }.AsQueryable<Product>();

    //        var mockProducts = new Mock<DbSet<Product>>();
    //        //mockProducts.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(products.Provider);
    //        //mockProducts.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(products.Expression);
    //        //mockProducts.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(products.ElementType);
    //        //mockProducts.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(products.GetEnumerator());
    //        var AppDbContext = new Mock<AppDbContext>() { CallBase = true};

    //        AppDbContext.Setup(c => c.Products).Returns(mockProducts.Object);
    //        return AppDbContext;
    //    }
    //}
}
