using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using Sport_store.Models;
using Sport_store.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Sport_store.Test
{
    public class OrderControllerTests
    {
        [Fact]
        public void Cannot_Ceckout_Empty_Cart()
        {
            //arrange 
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();

            Cart cart = new Cart();
            Order order = new Order();

            OrderController target = new OrderController(mock.Object, cart);

            //act
            ViewResult result = target.Checkout(order) as ViewResult;

            //assert
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
            Assert.True(string.IsNullOrEmpty(result.ViewName));
            Assert.False(result.ViewData.ModelState.IsValid);
        }
    }
}
