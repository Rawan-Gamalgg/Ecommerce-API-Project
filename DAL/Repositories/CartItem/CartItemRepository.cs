using Microsoft.EntityFrameworkCore;

namespace Ecommerce.DAL
{
    public class CartItemRepository : GenericRepository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(AppDbContext context) : base(context)
        {
        }
        /*------------------------------------------------------------------*/
        public async Task<IEnumerable<CartItem>> GetCartByUserId(string userId)
        {
            return await _context.CartItems
                .Include(ci => ci.Product)
                .Where(ci => ci.UserId == userId)
                .AsNoTracking()
                .ToListAsync();
        }
        /*------------------------------------------------------------------*/
        public async Task<CartItem?> GetCartItem(string userId, int productId)//to check if item already in cart
        {
            return await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.UserId == userId && ci.ProductId == productId);
        }
        /*------------------------------------------------------------------*/
    }
}