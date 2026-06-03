using Ecommerce.BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.APIs
{

    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IAuthManager _authManager;

        public RolesController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        /*----------------------------------------------------------*/
        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> CreateRole(
            [FromBody] RoleCreateDto roleCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result =
                await _authManager.CreateRole(roleCreateDto);

            if (!result)
            {
                return BadRequest(new { Message = "Role creation failed" });
            }

            return Ok(new { Message = "Role created successfully" });
        }
    }
}