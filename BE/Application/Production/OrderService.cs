using Core.Application;
using Core.Model;
using Core.Repositories;
using Domain.Entity;

namespace Application.Production;

public interface IOrderService
{
    Task<BaseDataResponseModel<List<OrderDto>>> GetOrder();
    Task<BaseResponseModel> SaveOrder(List<OrderDto> orderList);
}

public class OrderService : BaseService, IOrderService
{
    private readonly IRepository<Customer> _customerRepository;
    private readonly IRepository<Shop> _shopRepository;
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<ShopProduct> _shopProductsRepository;
    private readonly IRepository<Order> _orderRepository;

    public OrderService(
        IRepository<Customer> customerRepository,
        IRepository<Shop> shopRepository,
        IRepository<Product> productRepository,
        IRepository<ShopProduct> shopProductsRepository,
        IRepository<Order> orderRepository)
    {
        _customerRepository = customerRepository;
        _shopRepository = shopRepository;
        _productRepository = productRepository;
        _shopProductsRepository = shopProductsRepository;
        _orderRepository = orderRepository;
    }

    public async Task<BaseDataResponseModel<List<OrderDto>>> GetOrder()
    {
        var query = from customer in _customerRepository.GetQueryable()
                    from shop in _shopRepository.GetQueryable()
                    join shopProd in _shopProductsRepository.GetQueryable()
                        on shop.Id equals shopProd.ShopId
                    join product in _productRepository.GetQueryable()
                        on shopProd.ProductId equals product.Id
                    join order in _orderRepository.GetQueryable()
                        on new { CustomerId = customer.Id, ShopProductId = shopProd.Id }
                        equals new { order.CustomerId, order.ShopProductId }
                    orderby customer.Email, 
                        shop.Location descending,
                        product.Price
                    select new OrderDto()
                    {
                        CustomerName = customer.FullName,
                        CustomerDob = customer.Dob,
                        CustomerEmail = customer.Email,

                        ShopName = shop.Name,
                        ShopLocation = shop.Location,

                        ProductName = product.Name,
                        ProductImage = product.Image,
                        Price = product.Price,

                        Quantity = order.Quantity,

                    };
        var listOrder = query.ToList();
        var rs = new SuccessDataResponse<List<OrderDto>>(listOrder);
        return rs;
    }

    public async Task<BaseResponseModel> SaveOrder(List<OrderDto> orderList)
    {

        using (var trans = UnitOfWork.BeginTransaction())
        {
            try
            {
                var taskSaveCustomer = SaveCustomerByOrderList(orderList);
                var taskSaveProduct = SaveProductByOrderList(orderList);
                var taskSaveShop = SaveShopByOrderList(orderList);
                Task.WaitAll(taskSaveCustomer, taskSaveProduct, taskSaveShop);

                var customers = _customerRepository.GetQueryable().ToList();
                var products = _productRepository.GetQueryable().ToList();
                var shops = _shopRepository.GetQueryable().ToList();

                await SaveShopProductByOrderList(orderList, products, shops);

                await SaveOrderByOrderList(orderList, products, shops, customers);

                await trans.CommitAsync();
                return new SuccessResponse();
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return new ErrorResponse(ex.Message);
            }
        }  
    }

    private async Task SaveCustomerByOrderList(List<OrderDto> orderList)
    {
        var customerSaveList = orderList.Select(x => new Customer()
        {
            FullName = x.CustomerName,
            Dob = x.CustomerDob,
            Email = x.CustomerEmail,
        }).DistinctBy(d => d.Email);

        if (customerSaveList.Count() < 30)
            throw new Exception("Customer < 30");

        await _customerRepository.InsertRangeAsync(customerSaveList, autoSave: true);
    }

    private async Task SaveProductByOrderList(List<OrderDto> orderList)
    {
        var productSaveList = orderList.Select(x => new Product()
        {
            Name = x.ProductName,
            Image = x.ProductImage,
            Price = x.Price,
        }).DistinctBy(d => d.Name);

        await _productRepository.InsertRangeAsync(productSaveList, autoSave: true);
    }

    private async Task SaveShopByOrderList(List<OrderDto> orderList)
    {
        var shopSaveList = orderList.Select(x => new Shop()
        {
            Name = x.ShopName,
            Location = x.ShopLocation,
        }).DistinctBy(d => d.Name);

        if (shopSaveList.Count() < 3)
            throw new Exception("Shop < 3");

        await _shopRepository.InsertRangeAsync(shopSaveList, autoSave: true);
    }

    private async Task SaveShopProductByOrderList(
        List<OrderDto> orderList,
        List<Product> products,
        List<Shop> shops)
    {

        var shopProdDistintByName = orderList
                .Select(x => new
                {
                    x.ShopName,
                    x.ProductName
                }).Distinct().ToList();

        var shopProductSaveList = from shopProdDist in shopProdDistintByName
                                  join shop in shops
                                      on shopProdDist.ShopName equals shop.Name
                                  join product in products
                                      on shopProdDist.ProductName equals product.Name
                                  join shopProd in _shopProductsRepository.GetQueryable()
                                      on new { ShopId = shop.Id, ProductId = product.Id }
                                      equals new { shopProd.ShopId, shopProd.ProductId } into lShopProd
                                  from shopProd in lShopProd.DefaultIfEmpty()
                                  where shopProd == null
                                  select new ShopProduct()
                                  {
                                      ShopId = shop.Id,
                                      ProductId = product.Id
                                  };

        await _shopProductsRepository.InsertRangeAsync(shopProductSaveList, autoSave: true);
    }

    private async Task SaveOrderByOrderList(
        List<OrderDto> orderList,
        List<Product> products,
        List<Shop> shops,
        List<Customer> customers)
    {
        var totalProductQuantity = orderList.Sum(x => x.Quantity);
        if (totalProductQuantity < 32768)
            throw new Exception("Total product < 32768");

        var shopProducts = _shopProductsRepository.GetQueryable().ToList();

        var query = from lOrder in orderList
                    join customer in customers
                        on lOrder.CustomerName equals customer.FullName
                    join shop in shops
                        on lOrder.ShopName equals shop.Name
                    join product in products
                        on lOrder.ProductName equals product.Name
                    join shopProd in shopProducts
                        on new { ShopId = shop.Id, ProductId = product.Id }
                        equals new { shopProd.ShopId, shopProd.ProductId }
                    select new Order()
                    {
                        CustomerId = customer.Id,
                        ShopProductId = shopProd.Id,
                        Quantity = lOrder.Quantity,
                    };


        await _orderRepository.InsertRangeAsync(query, autoSave: true);
    }
}