using Application.Products;
using Autofac.Extras.Moq;
using Core.Application;
using Domain.Entity;

namespace Project.Test;

public class OrderTest : BaseTestService
{

    [Fact]
    public async void TestGetShops()
    {
        using (AutoMock)
        {
            var mockOrderService = AutoMock.Create<OrderService>();

            var actual = await mockOrderService.GetShops();
            Assert.NotEmpty(actual.Data);
        }
    }

    [Fact]
    public async void TestAddShop()
    {
        using (AutoMock)
        {
            var mockOrderService = AutoMock.Create<OrderService>();

            var actual = await mockOrderService.SaveShop(new Shop()
            {
                Name = "Test",
                Location = "Tect Location"
            });
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