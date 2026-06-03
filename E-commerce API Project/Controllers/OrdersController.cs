using Ecommerce.BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecommerce.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] //requires login
    public class OrdersController : ControllerBase
    {
        /*------------------------------------------------------------------*/
        private readonly IOrderManager _orderManager;
        /*------------------------------------------------------------------*/
        public OrdersController(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }
        /*------------------------------------------------------------------*/
        // POST: api/orders
        [HttpPost]
        public async Task<ActionResult<GeneralResult<OrderReadDto>>> PlaceOrder()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var response = await _orderManager.PlaceOrderAsync(userId);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
        /*------------------------------------------------------------------*/
        // GET: api/orders
        [HttpGet]
        public async Task<ActionResult<GeneralResult<IEnumerable<OrderReadDto>>>> GetOrderHistory()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var response = await _orderManager.GetOrderHistoryAsync(userId);
            return Ok(response);
        }
        /*------------------------------------------------------------------*/
        // GET: api/orders/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GeneralResult<OrderReadDto>>> GetOrderById([FromRoute] int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var response = await _orderManager.GetOrderByIdAsync(userId, id);
            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }
        /*------------------------------------------------------------------*/
    }
}