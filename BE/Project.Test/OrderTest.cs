using Application.Production;
using Autofac;
using Autofac.Extras.Moq;
using Core.Model;
using Dal.SqLite.EF;
using MockQueryable.FakeItEasy;
using Moq;

namespace Project.Test;

public class OrderTest : BaseServiceTest
{
    public OrderTest() 
    {
        AutoMock.MockRepository<Customer>();
        AutoMock.MockRepository<Shop>();
        AutoMock.MockRepository<Product>();
        AutoMock.MockRepository<ShopProduct>();
        AutoMock.MockRepository<Order>();
    }

    protected override void ServiceRegistrar(ContainerBuilder cb)
    {
        cb.RegisterType<OrderService>().PropertiesAutowired();
    }

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

    [Fact]
    public async void TestSaveOrderHasLessThan30Customer()
    {
        List<OrderDto> checkOrder = new()
        {
            new()
            {
                CustomerName = "c1",
                CustomerDob = DateTime.Now,

            }
        };

        using (AutoMock)
        {
            var mockOrderService = AutoMock.Create<OrderService>();

            var actual = await mockOrderService.SaveOrder(checkOrder);
            Assert.Equal(Status.Error, actual.Status);
            Assert.Equal("Customer < 30", actual.Message);
        }
    }
}