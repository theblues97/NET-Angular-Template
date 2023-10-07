using Application.Products;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public ProductController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        
        [HttpGet]
        [Route("GetOrder")]
        public async Task<IActionResult> GetOrder()
        {
            var rs = await _orderService.GetOrder();
            return Ok(rs);
        }
        
        [HttpPost]
        [Route("SaveOrder")]
        public async Task<IActionResult> SaveOrder([FromBody] List<OrderDto> orderList)
        {
            var rs = await _orderService.SaveOrder(orderList);
            return Ok(rs);
        }
    }
}