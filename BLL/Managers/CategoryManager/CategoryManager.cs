using FluentValidation;
using Ecommerce.DAL;

namespace Ecommerce.BLL
{
    public class CategoryManager : ICategoryManager
    {
        /*------------------------------------------------------------------*/
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CategoryCreateDto> _createValidator;
        private readonly IValidator<CategoryEditDto> _editValidator;
        /*------------------------------------------------------------------*/
        public CategoryManager(
            IUnitOfWork unitOfWork,
            IValidator<CategoryCreateDto> createValidator,
            IValidator<CategoryEditDto> editValidator)
        {
            _unitOfWork = unitOfWork;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }
        /*------------------------------------------------------------------*/
        public async Task<GeneralResult<IEnumerable<CategoryReadDto>>> GetAllAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllWithProducts();
            var result = categories.Select(c => MapToDto(c));
            return GeneralResult<IEnumerable<CategoryReadDto>>.Ok(result);
        }
        /*------------------------------------------------------------------*/
        public async Task<GeneralResult<CategoryReadDto>> GetByIdAsync(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetById(id);
            if (category == null)
                return GeneralResult<CategoryReadDto>.Fail("Category not found");

            return GeneralResult<CategoryReadDto>.Ok(MapToDto(category));
        }
        /*------------------------------------------------------------------*/
        public async Task<GeneralResult<CategoryReadDto>> CreateAsync(CategoryCreateDto dto)
        {
            // 1. validate
            var validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return GeneralResult<CategoryReadDto>.Fail(errors);
            }

            // 2. create
            var category = new Category { Name = dto.Name };
            _unitOfWork.CategoryRepository.Create(category);
            await _unitOfWork.Save();

            return GeneralResult<CategoryReadDto>.Ok(MapToDto(category), "Category created successfully");
        }
        /*------------------------------------------------------------------*/
        public async Task<GeneralResult<CategoryReadDto>> UpdateAsync(int id, CategoryEditDto dto)
        {
            // 1. validate
            var validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return GeneralResult<CategoryReadDto>.Fail(errors);
            }

            // 2. find
            var category = await _unitOfWork.CategoryRepository.GetById(id);
            if (category == null)
                return GeneralResult<CategoryReadDto>.Fail("Category not found");

            // 3. update
            category.Name = dto.Name;
            _unitOfWork.CategoryRepository.Update(category);
            await _unitOfWork.Save();

            return GeneralResult<CategoryReadDto>.Ok(MapToDto(category), "Category updated successfully");
        }
        /*------------------------------------------------------------------*/
        public async Task<GeneralResult<bool>> DeleteAsync(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetById(id);
            if (category == null)
                return GeneralResult<bool>.Fail("Category not found");

            _unitOfWork.CategoryRepository.Delete(category);
            await _unitOfWork.Save();

            return GeneralResult<bool>.Ok(true, "Category deleted successfully");
        }
        /*------------------------------------------------------------------*/
        public async Task<GeneralResult<CategoryReadDto>> UpdateImageAsync(int id, string imageUrl)
        {
            var category = await _unitOfWork.CategoryRepository.GetById(id);
            if (category == null)
                return GeneralResult<CategoryReadDto>.Fail("Category not found");

            category.ImageURL = imageUrl;
            _unitOfWork.CategoryRepository.Update(category);
            await _unitOfWork.Save();

            return GeneralResult<CategoryReadDto>.Ok(MapToDto(category), "Image updated successfully");
        }
        /*------------------------------------------------------------------*/
        private CategoryReadDto MapToDto(Category c) => new()
        {
            Id = c.Id,
            Name = c.Name,
            ImageUrl = c.ImageURL,
            ProductsCount = c.Products?.Count,
            CreatedAt = c.CreatedAt
        };
        /*------------------------------------------------------------------*/
    }
}