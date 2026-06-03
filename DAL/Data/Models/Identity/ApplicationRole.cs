
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.DAL
{
    public class ApplicationRole : IdentityRole
    {
        public string? Description { get; set; }
    }
}
