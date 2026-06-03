using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL
{
    public interface IUnitOfWork
    {
        /*------------------------------------------------------------------*/
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        ICartItemRepository CartItemRepository { get; }
        IOrderRepository OrderRepository { get; }
        IOrderItemRepository OrderItemRepository { get; }
        /*------------------------------------------------------------------*/
        Task<int> Save(); //to ensure only one commit to the database, and all changes are saved together
    }
}
