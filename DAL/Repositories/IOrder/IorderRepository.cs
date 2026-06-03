namespace Ecommerce.DAL
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        /*------------------------------------------------------------------*/
        Task<IEnumerable<Order>> GetOrdersByUserId(string userId);
        Task<Order?> GetOrderWithItems(int orderId);
        /*------------------------------------------------------------------*/
    }
}