using FluentValidation;
using Ecommerce.DAL;

namespace Ecommerce.BLL
{
    public class ProductManager : IProductManager
    {
        /*------------------------------------------------------------------*/
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<ProductCreateDto> _createValidator;
        private readonly IValidator<ProductEditDto> _editValidator;
        /*------------------------------------------------------------------*/
        public ProductManager(
            IUnitOfWork unitOfWork,
            IValidator<ProductCreateDto> createValidator,
            IValidator<ProductEditDto> editValidator)
        {
            _unitOfWork = unitOfWork;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }
        /*------------------------------------------------------------------*/
        public async Task<GeneralResult<IEnumerable<ProductReadDto>>> GetAllAsync(
            int? categoryId, string? name, int pageNumber, int pageSize)
        {
            var products = await _unitOfWork.ProductRepository.GetFilteredAsync(categoryId, name, pageNumber, pageSize);
            var result = products.Select(p => MapToDto(p));
            return GeneralResult<IEnumerable<ProductReadDto>>.Ok(result);
        }
        /*------------------------------------------------------------------*/
        public async Task<GeneralResult<ProductReadDto>> GetByIdAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdWithCategory(id);
            if (product == null)
                return GeneralResult<ProductReadDto>.Fail("Product not found");

            return GeneralResult<ProductReadDto>.Ok(MapToDto(product));
        }
        /*------------------------------------------------------------------*/
        public async Task<GeneralResult<ProductReadDto>> CreateAsync(ProductCreateDto dto)
        {
            var validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return GeneralResult<ProductReadDto>.Fail(errors);
            }

            // check category exists
            var category = await _unitOfWork.CategoryRepository.GetById(dto.CategoryId);
            if (category == null)
                return GeneralResult<ProductReadDto>.Fail("Selected category does not exist");

            // create
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Count = dto.Count,
                ExpiryDate = dto.ExpiryDate ?? DateTime.Now.AddYears(1),
                CategoryId = dto.CategoryId
            };

            _unitOfWork.ProductRepository.Create(product);
            await _unitOfWork.Save();

            return GeneralResult<ProductReadDto>.Ok(MapToDto(product), "Product created successfully");
        }
        /*------------------------------------------------------------------*/
        public async Task<GeneralResult<ProductReadDto>> UpdateAsync(int id, ProductEditDto dto)
        {
            // validator
            var validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return GeneralResult<ProductReadDto>.Fail(errors);
            }

            var product = await _unitOfWork.ProductRepository.GetById(id);
            if (product == null)
                return GeneralResult<ProductReadDto>.Fail("Product not found");

            // update
            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Count = dto.Count;
            product.CategoryId = dto.CategoryId;

            _unitOfWork.ProductRepository.Update(product);
            await _unitOfWork.Save();

            return GeneralResult<ProductReadDto>.Ok(MapToDto(product), "Product updated successfully");
        }
        /*------------------------------------------------------------------*/
        public async Task<GeneralResult<bool>> DeleteAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetById(id);
            if (product == null)
                return GeneralResult<bool>.Fail("Product not found");

            _unitOfWork.ProductRepository.Delete(product);
            await _unitOfWork.Save();

            return GeneralResult<bool>.Ok(true, "Product deleted successfully");
        }
        /*------------------------------------------------------------------*/
        public async Task<GeneralResult<ProductReadDto>> UpdateImageAsync(int id, string imageUrl)
        {
            var product = await _unitOfWork.ProductRepository.GetById(id);
            if (product == null)
                return GeneralResult<ProductReadDto>.Fail("Product not found");

            product.ImageURL = imageUrl;
            _unitOfWork.ProductRepository.Update(product);
            await _unitOfWork.Save();

            return GeneralResult<ProductReadDto>.Ok(MapToDto(product), "Image updated successfully");
        }
        /*------------------------------------------------------------------*/
        private ProductReadDto MapToDto(Product p) => new()
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            Count = p.Count,
            ImageUrl = p.ImageURL,
            CategoryId = p.CategoryId,
            CategoryName = p.Category?.Name,
            ExpiryDate = p.ExpiryDate,
            CreatedAt = p.CreatedAt
        };
        /*------------------------------------------------------------------*/
    }
}