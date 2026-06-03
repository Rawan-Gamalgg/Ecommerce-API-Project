using Ecommerce.BLL.Settings;
using Ecommerce.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce.BLL
{

    public class AuthManager : IAuthManager
    {

        /*----------------------------------------------------------*/
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly RoleManager<ApplicationRole> _roleManager;
        /*----------------------------------------------------------*/
        public AuthManager(
     UserManager<ApplicationUser> userManager,
     RoleManager<ApplicationRole> roleManager,
     IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
        }
        /*----------------------------------------------------------*/

        public async Task<TokenDto?> Register(RegisterDto registerDto)
        {
            //map the loginDto to the model(ApplicationUser)
            ApplicationUser user = new ApplicationUser
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
            };
            //create the user
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);

                throw new Exception(string.Join(", ", errors));
            }
            //assign the role to the user
            var identityResult = await _userManager.AddToRoleAsync(user, "User");
            if (!identityResult.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);

                throw new Exception(string.Join(", ", errors));
            }

            return await Login(
             new LoginDto(registerDto.Email, registerDto.Password)

                );
        }
        /*----------------------------------------------------------*/
        public async Task<TokenDto?> Login(LoginDto loginDto)
        {
            //1.Validate the user credentials
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
                return null;

            var valid = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!valid)
                return null;
            //2.Define claims for the token (e.g., username, role, etc.)
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName!)
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            return GenerateToken(claims);
        }
        /*----------------------------------------------------------*/
        private TokenDto GenerateToken(List<Claim> claims)
        {

            //3. Create a security key
            var secretKey = _jwtSettings.SecretKey;

            //convert the secret key to bytes
            var keyBytes = Convert.FromBase64String(secretKey);

            var securityKey = new SymmetricSecurityKey(keyBytes);

            //4. Create signing credentials
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expireDate = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes);


            //5. Generate the token using JwtSecurityTokenHandler and return it to the client
            var tokenString = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expireDate,
                signingCredentials: signingCredentials);

            //6. encode the token
            var token = new JwtSecurityTokenHandler().WriteToken(tokenString);
            // Return token to the client
            var tokenDto = new TokenDto(
                token,
                _jwtSettings.DurationInMinutes);
            return tokenDto;
        }
        /*----------------------------------------------------------*/
        public async Task<bool> CreateRole(RoleCreateDto roleCreateDto)
        {
            ApplicationRole role = new()
            {
                Name = roleCreateDto.Name
            };

            var result =
                await _roleManager.CreateAsync(role);

            return result.Succeeded;
        }
    }
}
