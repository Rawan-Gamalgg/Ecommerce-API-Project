
namespace Ecommerce.DAL
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        /*------------------------------------------------------------------*/
        Task<IEnumerable<Product>> GetAllWithCategories();
        Task<Product?> GetByIdWithCategory(int id);
        Task<bool> AnyWithName(string Name);
        Task<IEnumerable<Product>> GetFilteredAsync(int? categoryId, string? name, int pageNumber =1, int pageSize=5);
        Task<Product?> GetByIdWithCategoryAsync(int id);
        /*---------------------------------------------------------------------*/


    }
}
