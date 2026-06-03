
namespace Ecommerce.DAL
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        /*------------------------------------------------------------------*/
        Task<IEnumerable<Category>> GetAllWithProducts();
        Task<Category?> GetByIdWithProducts(int id);
        /*------------------------------------------------------------------*/


    }
}

