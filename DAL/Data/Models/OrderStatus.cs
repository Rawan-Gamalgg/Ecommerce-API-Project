using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.DAL
{
    public enum OrderStatus
    {
        Pending,
        Confirmed,
        Shipped,
        Delivered,
        Cancelled
    }
}
