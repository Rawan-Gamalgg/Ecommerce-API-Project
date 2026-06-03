// Controllers/ProductsController.cs
using Ecommerce.BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        /*------------------------------------------------------------------*/
        private readonly IProductManager _productManager;
        /*------------------------------------------------------------------*/
        public ProductsController(IProductManager productManager)
        {
            _productManager = productManager;
        }
        /*------------------------------------------------------------------*/
        // GET: api/products?categoryId=1&name=phone&pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<ActionResult<GeneralResult<IEnumerable<ProductReadDto>>>> GetAll(
            [FromQuery] int? categoryId,
            [FromQuery] string? name,
            [FromQuery] int pageNumber ,
            [FromQuery] int pageSize )
        {
            var response = await _productManager.GetAllAsync(categoryId, name, pageNumber, pageSize);
            return Ok(response);
        }
        /*------------------------------------------------------------------*/
        // GET: api/products/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GeneralResult<ProductReadDto>>> GetById([FromRoute] int id)
        {
            var response = await _productManager.GetByIdAsync(id);
            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }
        /*------------------------------------------------------------------*/
        // POST: api/products
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<GeneralResult<ProductReadDto>>> Create([FromBody] ProductCreateDto dto)
        {
            var response = await _productManager.CreateAsync(dto);
            if (!response.Success)
                return BadRequest(response);

            return CreatedAtAction(nameof(GetById), new { id = response.Data!.Id }, response);
        }
        /*------------------------------------------------------------------*/
        // PUT: api/products/5
        [HttpPut("{id:int}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<GeneralResult<ProductReadDto>>> Update(
            [FromRoute] int id,
            [FromBody] ProductEditDto dto)
        {
            var response = await _productManager.UpdateAsync(id, dto);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
        /*------------------------------------------------------------------*/
        // DELETE: api/products/5
        [HttpDelete("{id:int}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<GeneralResult<bool>>> Delete([FromRoute] int id)
        {
            var response = await _productManager.DeleteAsync(id);
            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }
        /*------------------------------------------------------------------*/
    }
}