// OrderItemRepository.cs
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.DAL
{
    public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(AppDbContext context) : base(context)
        {
        }
        /*------------------------------------------------------------------*/
        public async Task<IEnumerable<OrderItem>> GetItemsByOrderId(int orderId)
        {
            return await _context.OrderItems
                .Include(oi => oi.Product)
                .Where(oi => oi.OrderId == orderId)
                .AsNoTracking()
                .ToListAsync();
        }
        /*------------------------------------------------------------------*/
    }
}