using APICatalogo.Context;

namespace APICatalogo.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext context;

    public Repository(AppDbContext context)
    {
        this.context = context;
    }

    public IEnumerable<T> GetAll()
    {
        return context.Set<T>().ToList(); //Set -> Acessa a tbl da entidade T
    }

    public T? GetById(int id)
    {
        return context.Set<T>().Find(id);
    }

    public T Create(T entity)
    {
        context.Set<T>().Add(entity);
        context.SaveChanges();
        return entity;
    }

    public T Update(T entity)
    {
        context.Set<T>().Update(entity);
        context.SaveChanges();
        return entity;
    }

    public T Delete(T entity)
    {
        context.Set<T>().Remove(entity);
        context.SaveChanges();
        return entity;
    }
}