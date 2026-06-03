using Ecommerce.DAL;
using FluentValidation;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Ecommerce.BLL
{
    public class CartManager : ICartManager
    {
        /*------------------------------------------------------------------*/
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CartItemCreateDto> _createValidator;
        private readonly IValidator<CartItemUpdateDto> _updateValidator;
        /*------------------------------------------------------------------*/
        public CartManager(
            IUnitOfWork unitOfWork,
            IValidator<CartItemCreateDto> createValidator,
            IValidator<CartItemUpdateDto> updateValidator)
        {
            _unitOfWork = unitOfWork;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }
        /*------------------------------------------------------------------*/
        public async Task<GeneralResult<IEnumerable<CartItemReadDto>>> GetCartAsync(string userId)
        {
            var items = await _unitOfWork.CartItemRepository.GetCartByUserId(userId);
            var result = items.Select(ci => MapToDto(ci));
            return GeneralResult<IEnumerable<CartItemReadDto>>.Ok(result);
        }
        /*------------------------------------------------------------------*/
        public async Task<GeneralResult<CartItemReadDto>> AddToCartAsync(string userId, CartItemCreateDto dto)
        {
            var validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return GeneralResult<CartItemReadDto>.Fail(errors);
            }

            var product = await _unitOfWork.ProductRepository.GetById(dto.ProductId);
            if (product == null)
                return GeneralResult<CartItemReadDto>.Fail("Product not found");

            if (product.Count < dto.Quantity)
                return GeneralResult<CartItemReadDto>.Fail("Not enough stock available");

            //if already in cart => increment quantity
            var existingItem = await _unitOfWork.CartItemRepository.GetCartItem(userId, dto.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += dto.Quantity;
                _unitOfWork.CartItemRepository.Update(existingItem);
                await _unitOfWork.Save();
                return GeneralResult<CartItemReadDto>.Ok(MapToDto(existingItem), "Cart updated");
            }

            var cartItem = new CartItem
            {
                UserId = userId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity
            };

            _unitOfWork.CartItemRepository.Create(cartItem);
            await _unitOfWork.Save();

            return GeneralResult<CartItemReadDto>.Ok(MapToDto(cartItem), "Item added to cart");
        }
        /*------------------------------------------------------------------*/
        public async Task<GeneralResult<CartItemReadDto>> UpdateQuantityAsync(string userId, CartItemUpdateDto dto)
        {
            //validate
            var validationResult = await _updateValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return GeneralResult<CartItemReadDto>.Fail(errors);
            }
            //check if item exists in cart
            var cartItem = await _unitOfWork.CartItemRepository.GetCartItem(userId, dto.ProductId);
            if (cartItem == null)//if not in cart, we can't update quantity
                return GeneralResult<CartItemReadDto>.Fail("Item not found in cart");

            //check stock
            var product = await _unitOfWork.ProductRepository.GetById(dto.ProductId);
            if (product!.Count < dto.Quantity)
                return GeneralResult<CartItemReadDto>.Fail("Not enough stock available");

            //update
            cartItem.Quantity = dto.Quantity;
            _unitOfWork.CartItemRepository.Update(cartItem);
            await _unitOfWork.Save();

            return GeneralResult<CartItemReadDto>.Ok(MapToDto(cartItem), "Quantity updated");
        }
        /*------------------------------------------------------------------*/
        public async Task<GeneralResult<bool>> RemoveFromCartAsync(string userId, int productId)
        {
            var cartItem = await _unitOfWork.CartItemRepository.GetCartItem(userId, productId);
            if (cartItem == null)
                return GeneralResult<bool>.Fail("Item not found in cart");

            _unitOfWork.CartItemRepository.Delete(cartItem);
            await _unitOfWork.Save();

            return GeneralResult<bool>.Ok(true, "Item removed from cart");
        }
        /*------------------------------------------------------------------*/
        private CartItemReadDto MapToDto(CartItem ci) => new()
        {
            Id = ci.Id,
            ProductId = ci.ProductId,
            ProductName = ci.Product?.Name,
            ProductImage = ci.Product?.ImageURL,
            UnitPrice = ci.Product?.Price ?? 0,
            Quantity = ci.Quantity
        };
        /*------------------------------------------------------------------*/
    }
}