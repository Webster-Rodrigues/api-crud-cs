using APICatalogo.Context;

namespace APICatalogo.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private IProductRepository? _productRepository;  
    private ICategoryRepository? _categoryRepository;
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IProductRepository ProductRepository
    {
        get
        {
            return _productRepository ??= new ProductRepository(_context);
        }
    }

    public ICategoryRepository CategoryRepository
    {
        get
        {
            return _categoryRepository ??= new CategoryRepository(_context);
        }
    }

    public void Commit()
    {
        _context.SaveChanges();  
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}