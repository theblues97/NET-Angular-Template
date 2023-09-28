using Application.Production;
using Autofac;
using Autofac.Extras.Moq;
using Dal.SqLite.EF;

namespace Project.Test;

public class OrderTest : BaseServiceTest
{
    protected override void ServiceRegistrar(ContainerBuilder cb)
    {
        cb.RegisterType<OrderService>().PropertiesAutowired();
    }

    //TODO: Error in test
    [Fact]
    public async void TestGetOrder()
    {
        var mockCustomer = new Customer()
        {
            Id = 1,
            FullName = "TestCustomer",
            Dob = DateTime.Now,
            Email = "test@gmail.com"
        };
        var mockShop = new Shop()
        {
            Id = 1,
            Name = "TestShop",
            Location = ""
        };
        var mockProduct = new Product()
        {
            Id = 1,
            Name = "TestProduct",
            Image = "",
            Price = 10

        };
        var mockShopProduct = new ShopProduct()
        {
            Id = 1,
            ShopId = 1,
            ProductId = 1
        };
        var mockOrder = new Order()
        {
            Id = 1,
            CustomerId = 1,
            ShopProductId = 1,
            Quantity = 2
        };


        AutoMock.MockRepository<Customer>(mockCustomer);
        AutoMock.MockRepository<Shop>(mockShop);
        AutoMock.MockRepository<Product>(mockProduct);
        AutoMock.MockRepository<ShopProduct>(mockShopProduct);
        AutoMock.MockRepository<Order>(mockOrder);

        using (AutoMock)
        {
            var mockOrderService = AutoMock.Create<OrderService>();

            var actual = await mockOrderService.GetOrder();
            Assert.NotNull(actual);
            //Assert.Equal(emp, actual);

        }
    }
}