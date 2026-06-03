using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.BLL
{
    

    public sealed record ImageUploadDto(IFormFile File);
}
