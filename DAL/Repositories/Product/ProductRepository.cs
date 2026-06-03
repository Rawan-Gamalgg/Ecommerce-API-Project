

using Microsoft.EntityFrameworkCore;

namespace Ecommerce.DAL
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
        /*---------------------------------------------------------------------*/
        public async Task<IEnumerable<Product>> GetAllWithCategories()
        {
            return await _context.
                Products
                .Include(p => p.Category)
                .AsNoTracking()
                .ToListAsync();
        }
        /*---------------------------------------------------------------------*/
        public async Task<Product?> GetByIdWithCategory(int id)
        {
            return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);

        }

        /*------------------------------------------------------------------*/
        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();

        }
        /*------------------------------------------------------------------*/
        public async Task<bool> AnyWithName(string Name)
        {
            return await _context.Products.AnyAsync(p => p.Name == Name);

        }
        /*---------------------------------------------------------------------*/
        public async Task<IEnumerable<Product>> GetFilteredAsync(
               int? categoryId, string? name, int pageNumber=1, int pageSize=5)
        {
            //safety
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 5;
            if (pageSize > 50) pageSize = 50; // prevent someone requesting pageSize=999999

            var query = _context.Products.Include(p => p.Category).AsQueryable();

            if (categoryId.HasValue)
                query = query.Where(p => p.CategoryId == categoryId.Value);

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(p => p.Name.Contains(name));

            return await query
                .AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        /*---------------------------------------------------------------------*/

        public async Task<Product?> GetByIdWithCategoryAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }

}

    
