using FluentValidation;

namespace Ecommerce.BLL
{
    public class CartItemUpdateValidator : AbstractValidator<CartItemUpdateDto>
    {
        public CartItemUpdateValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("Invalid product");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be at least 1");
        }
    }
}