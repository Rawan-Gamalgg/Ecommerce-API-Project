using FluentValidation;

namespace Ecommerce.BLL
{
    public class CategoryEditValidator : AbstractValidator<CategoryEditDto>
    {
        public CategoryEditValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Invalid category Id");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required")
                .Length(2, 100).WithMessage("Name must be between 2 and 100 characters");
        }
    }
}