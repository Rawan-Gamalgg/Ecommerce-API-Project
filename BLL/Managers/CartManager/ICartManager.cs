namespace Ecommerce.BLL
{
    public interface ICartManager
    {
        /*------------------------------------------------------------------*/
        Task<GeneralResult<IEnumerable<CartItemReadDto>>> GetCartAsync(string userId);
        Task<GeneralResult<CartItemReadDto>> AddToCartAsync(string userId, CartItemCreateDto dto);
        Task<GeneralResult<CartItemReadDto>> UpdateQuantityAsync(string userId, CartItemUpdateDto dto);
        Task<GeneralResult<bool>> RemoveFromCartAsync(string userId, int productId);
        /*------------------------------------------------------------------*/
    }
}