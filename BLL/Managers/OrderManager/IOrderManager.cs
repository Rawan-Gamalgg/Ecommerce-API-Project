namespace Ecommerce.BLL
{
    public interface IOrderManager
    {
        /*------------------------------------------------------------------*/
        Task<GeneralResult<OrderReadDto>> PlaceOrderAsync(string userId);
        Task<GeneralResult<IEnumerable<OrderReadDto>>> GetOrderHistoryAsync(string userId);
        Task<GeneralResult<OrderReadDto>> GetOrderByIdAsync(string userId, int orderId);
        /*------------------------------------------------------------------*/
    }
}