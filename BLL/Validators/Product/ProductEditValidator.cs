using FluentValidation;

namespace Ecommerce.BLL
{ 
    public class ProductEditValidator : AbstractValidator<ProductEditDto>
    {
        public ProductEditValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Invalid product Id");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required")
                .Length(3, 100).WithMessage("Name must be between 3 and 100 characters");

            RuleFor(x => x.Price)
                .GreaterThan(10).WithMessage("Price must be greater than 10");

            RuleFor(x => x.Count)
                .GreaterThanOrEqualTo(0).WithMessage("Count cannot be negative");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Please select a valid category");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters")
                .When(x => x.Description != null);
        }
    }
}