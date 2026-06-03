using Ecommerce.BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecommerce.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] //requires login
    public class CartController : ControllerBase
    {
        /*------------------------------------------------------------------*/
        private readonly ICartManager _cartManager;
        /*------------------------------------------------------------------*/
        public CartController(ICartManager cartManager)
        {
            _cartManager = cartManager;
        }
        /*------------------------------------------------------------------*/
        // GET: api/cart
        [HttpGet]
        public async Task<ActionResult<GeneralResult<IEnumerable<CartItemReadDto>>>> GetCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var response = await _cartManager.GetCartAsync(userId);
            return Ok(response);
        }
        /*------------------------------------------------------------------*/
        // POST: api/cart
        [HttpPost]
        public async Task<ActionResult<GeneralResult<CartItemReadDto>>> AddToCart([FromBody] CartItemCreateDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var response = await _cartManager.AddToCartAsync(userId, dto);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
        /*------------------------------------------------------------------*/
        // PUT: api/cart
        [HttpPut]
        public async Task<ActionResult<GeneralResult<CartItemReadDto>>> UpdateQuantity([FromBody] CartItemUpdateDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var response = await _cartManager.UpdateQuantityAsync(userId, dto);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
        /*------------------------------------------------------------------*/
        // DELETE: api/cart/5
        [HttpDelete("{productId:int}")]
        public async Task<ActionResult<GeneralResult<bool>>> RemoveFromCart([FromRoute] int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var response = await _cartManager.RemoveFromCartAsync(userId, productId);
            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }
        /*------------------------------------------------------------------*/
    }
}