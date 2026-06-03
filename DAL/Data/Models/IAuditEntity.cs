using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.DAL
{
    public class IAuditEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }


    }
}
