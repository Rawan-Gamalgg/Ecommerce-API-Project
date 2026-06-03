namespace Ecommerce.BLL
{
    public interface ICategoryManager
    {
        /*------------------------------------------------------------------*/
        Task<GeneralResult<IEnumerable<CategoryReadDto>>> GetAllAsync();
        Task<GeneralResult<CategoryReadDto>> GetByIdAsync(int id);
        Task<GeneralResult<CategoryReadDto>> CreateAsync(CategoryCreateDto dto);
        Task<GeneralResult<CategoryReadDto>> UpdateAsync(int id, CategoryEditDto dto);
        Task<GeneralResult<bool>> DeleteAsync(int id);
        Task<GeneralResult<CategoryReadDto>> UpdateImageAsync(int id, string imageUrl);
        /*------------------------------------------------------------------*/
    }
}