namespace Ecommerce.DAL
{
    public interface ICartItemRepository : IGenericRepository<CartItem>
    {
        /*------------------------------------------------------------------*/
        Task<IEnumerable<CartItem>> GetCartByUserId(string userId);
        Task<CartItem?> GetCartItem(string userId, int productId);
        /*------------------------------------------------------------------*/
    }
}