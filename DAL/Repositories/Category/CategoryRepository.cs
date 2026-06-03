using Microsoft.EntityFrameworkCore;

namespace Ecommerce.DAL
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) 
        {
        }
        /*------------------------------------------------------------------*/
       

        /*------------------------------------------------------------------*/

        public async Task<IEnumerable<Category>> GetAllWithProducts()
        {
            return await _context.Categories
                .Include(c => c.Products)
                .AsNoTracking()
                .ToListAsync();
        }
        /*------------------------------------------------------------------*/

        public async Task<Category?> GetByIdWithProducts(int id)
        {
            return await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);
        }


        /*------------------------------------------------------------------*/




    }
}
