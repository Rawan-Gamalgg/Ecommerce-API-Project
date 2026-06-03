using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ecommerce.BLL;

namespace Ecommerce.BLL
{
    public interface IAuthManager
    {
        Task<TokenDto?> Login(LoginDto dto);
        Task<TokenDto?> Register(RegisterDto dto);
        Task<bool> CreateRole(RoleCreateDto roleCreateDto);
    }
}