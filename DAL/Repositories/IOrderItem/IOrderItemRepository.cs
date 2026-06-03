// IOrderItemRepository.cs
namespace Ecommerce.DAL
{
    public interface IOrderItemRepository : IGenericRepository<OrderItem>
    {
        /*------------------------------------------------------------------*/
        Task<IEnumerable<OrderItem>> GetItemsByOrderId(int orderId);
        /*------------------------------------------------------------------*/
    }
}