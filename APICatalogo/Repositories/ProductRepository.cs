using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    
    public ProductRepository(AppDbContext context) : base(context)
    {
    }


}