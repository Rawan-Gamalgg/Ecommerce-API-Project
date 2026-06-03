using Microsoft.AspNetCore.Mvc;
using Ecommerce.BLL;

namespace Ecommerce.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        /*----------------------------------------------------------*/
        private readonly IAuthManager _authManager;
        /*----------------------------------------------------------*/

        public AuthController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        /*----------------------------------------------------------*/
        [HttpPost("login")]
        public async Task<ActionResult<GeneralResult<LoginDto>>> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authManager.Login(dto);

            if (result == null)
            {
                return Unauthorized(result);
            }

            return Ok(result);
        }

        /*----------------------------------------------------------*/
        [HttpPost("register")]
        public async Task<ActionResult<GeneralResult<RegisterDto>>> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result =
                await _authManager.Register(dto);

            if (result == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        /*----------------------------------------------------------*/

    }
}