using APICatalago.Context;
using APICatalago.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalago.Controllers;

[Route("categories")]
[ApiController]
public class CategoryController : Controller
{
    private readonly AppDbContext context; //Acesso ao banco de dados

    public CategoryController(AppDbContext context)
    {
        this.context = context;
    }

    [HttpGet("products")]
    public ActionResult<IEnumerable<Category>> GetCategoriesProducts()
    {
        //return context.Categories.Include(p => p.Products).AsNoTracking().ToList();
        //Include = Método do EntityFramework para incluir, juntamente de categorias, todos os produtos relacionados
        return context.Categories.Include(c => c.Products)
            .Where(c=>c.CategoryId <= 5).ToList();
        //Evita retornar todos os registros
    }

    [HttpGet]
    public ActionResult<IEnumerable<Category>> Get()
    {
        return context.Categories.AsNoTracking().ToList();
        //AsNoTracking => Evita que as entidades sejam rastradas pelo EF. Recomendavel em consultas somente leituras
        //Assim não precisa rastrear essa consulta
        
    }
    
    [HttpGet("{id:int:min(1)}")] //Restrição no parâmetro
    public ActionResult<Category> Get(int id)
    {
        var category = context.Categories.Find(id);
        if (category is null)
        {
            return NotFound("Category não encontrada");
        }
        return category;
    }

    [HttpPost]
    public ActionResult<Category> Post(Category category)
    {
        if (category is null)
        {
            return BadRequest();
        }
        context.Categories?.Add(category);
        context.SaveChanges();
        return category;
    }

    [HttpPut("{id:int}")]
    public ActionResult<Category> Put(int id, Category category)
    {
        if (id != category.CategoryId)
        {
            return BadRequest();
        }

        context.Entry(category).State = EntityState.Modified;
        context.SaveChanges();
        return Ok(category);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<Category> Delete(int id)
    {
        var category = context.Categories.Find(id);

        if (category is null)
        {
            return NotFound("Id informado não existe");
        }
        
        context.Categories.Remove(category);
        context.SaveChanges();
        return Ok(category);
        
    }
    
    
}