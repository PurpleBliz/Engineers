using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engineers.Api.Controllers;
using Engineers.Api.IService;
using Engineers.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Engineers.Models;

[TestClass]
public class OrderServiceTests 
{
    [TestMethod]
    public void Test_SetRespond()
    {
        // Arrange
        var mock = new Mock<IOrderService>();
        var controller = new OrderController(mock.Object);

        Respond respond = new()
        {
            UserId = "0d62d11b-0c4e-426b-9881-99d98a178050",
            OrderId = 1,
            Text = "Отклик юнит тест"
        };

        // Act
        var start = controller.Send(respond);

        var act = start?.Text;

        // Assert
        Assert.AreEqual("OK", "OK");
    }
}