using Ecommerce.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Ecommerce.BLL
{
    public interface IProductManager
    {
        /*------------------------------------------------------------------*/
        Task<GeneralResult<IEnumerable<ProductReadDto>>> GetAllAsync(int? categoryId, string? name, int pageNumber, int pageSize);
        Task<GeneralResult<ProductReadDto?>> GetByIdAsync(int id);
        Task<GeneralResult<ProductReadDto>> CreateAsync(ProductCreateDto dto);
        Task<GeneralResult<ProductReadDto>> UpdateAsync(int id, ProductEditDto dto);
        Task<GeneralResult<bool>> DeleteAsync(int id);
        Task<GeneralResult<ProductReadDto>> UpdateImageAsync(int id, string imageUrl);
        /*----------------------------------------------------------*/
        //Task<List<ProductReadDto>> GetByCategoryAsync(int categoryId);  // for the partial view

        //Task<List<CategoryReadDto>> GetAllCategoriesAsync();
    }
}
