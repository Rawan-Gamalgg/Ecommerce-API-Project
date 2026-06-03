using Ecommerce.BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        /*------------------------------------------------------------------*/
        private readonly ICategoryManager _categoryManager;
        /*------------------------------------------------------------------*/
        public CategoriesController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }
        /*------------------------------------------------------------------*/
        // GET: api/categories
        [HttpGet]
        public async Task<ActionResult<GeneralResult<IEnumerable<CategoryReadDto>>>> GetAll()
        {
            var response = await _categoryManager.GetAllAsync();
            return Ok(response);
        }
        /*------------------------------------------------------------------*/
        // GET: api/categories/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GeneralResult<CategoryReadDto>>> GetById([FromRoute] int id)
        {
            var response = await _categoryManager.GetByIdAsync(id);
            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }
        /*------------------------------------------------------------------*/
        // POST: api/categories
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<GeneralResult<CategoryReadDto>>> Create([FromBody] CategoryCreateDto dto)
        {
            var response = await _categoryManager.CreateAsync(dto);
            if (!response.Success)
                return BadRequest(response);

            return CreatedAtAction(nameof(GetById), new { id = response.Data!.Id }, response);
        }
        /*------------------------------------------------------------------*/
        // PUT: api/categories/5
        [HttpPut("{id:int}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<GeneralResult<CategoryReadDto>>> Update(
            [FromRoute] int id,
            [FromBody] CategoryEditDto dto)
        {
            var response = await _categoryManager.UpdateAsync(id, dto);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
        /*------------------------------------------------------------------*/
        // DELETE: api/categories/5
        [HttpDelete("{id:int}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<GeneralResult<bool>>> Delete([FromRoute] int id)
        {
            var response = await _categoryManager.DeleteAsync(id);
            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }
        /*------------------------------------------------------------------*/
    }
}


