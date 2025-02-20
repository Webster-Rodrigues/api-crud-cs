using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    
    public CategoryRepository(AppDbContext context) : base(context)
    {
        
    }
    
    public IEnumerable<Category> GetCategoriesProducts()
    {
        return context.Categories.Include(c => c.Products) 
                    .Where(c => c.CategoryId <= 5).AsNoTracking().ToList();

    }
}