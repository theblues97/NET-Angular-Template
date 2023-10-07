using Application.Production;
using Autofac;
using Autofac.Extras.Moq;
using Core.Model;
using Domain.Entity;

namespace Project.Test;

public class OrderTest : BaseTestService
{

    [Fact]
    public async void TestGetOrder()
    {
        using (AutoMock)
        {
            var mockOrderService = AutoMock.Create<OrderService>();

            var actual = await mockOrderService.GetOrder();
            Assert.NotNull(actual.Data);
        }
    }

    //[Fact]
    //public async void TestSaveOrderHasLessThan30Customer()
    //{
    //    List<OrderDto> checkOrder = new()
    //    {
    //        new()
    //        {
    //            CustomerName = "c1",
    //            CustomerDob = DateTime.Now,

    //        }
    //    };

    //    using (AutoMock)
    //    {
    //        var mockOrderService = AutoMock.Create<OrderService>();

    //        var actual = await mockOrderService.SaveOrder(checkOrder);
    //        Assert.Equal(Status.Error, actual.Status);
    //        Assert.Equal("Customer < 30", actual.Message);
    //    }
    //}
}