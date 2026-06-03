using FluentValidation;

namespace Ecommerce.BLL
{
    public class CategoryCreateValidator : AbstractValidator<CategoryCreateDto>
    {
        public CategoryCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required")
                .Length(2, 100).WithMessage("Name must be between 2 and 100 characters");
        }
    }
}