using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        /*------------------------------------------------------------------*/
        public IProductRepository ProductRepository{ get; }
        public ICategoryRepository CategoryRepository { get; }
        public ICartItemRepository CartItemRepository { get; }
        public IOrderItemRepository OrderItemRepository { get; }
        public IOrderRepository OrderRepository { get; }
        /*------------------------------------------------------------------*/
        private readonly AppDbContext _context;
        /*------------------------------------------------------------------*/
        public UnitOfWork(ICategoryRepository categoryRepository,
            IProductRepository productRepository,
            AppDbContext context,
            IOrderRepository orderRepository,
            IOrderItemRepository orderItemRepository,
            ICartItemRepository cartItemRepository)
        {
            ProductRepository = productRepository;
            CategoryRepository = categoryRepository;
            _context = context;
            OrderRepository = orderRepository;
            OrderItemRepository = orderItemRepository;
            CartItemRepository = cartItemRepository;
        }
        /*------------------------------------------------------------------*/

        public async Task<int> Save()//Async
        {
           return await _context.SaveChangesAsync();    
        }
    }
}
