using APICatalago.Context;
using APICatalago.Filters;
using APICatalago.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalago.Controllers;

[Route("categories")]
[ApiController]
public class CategoryController : Controller
{
    private readonly AppDbContext context; //Acesso ao banco de dados
    private readonly ILogger<CategoryController> logger; //Logger injetado pelo .NET

    public CategoryController(AppDbContext context, ILogger<CategoryController> logger)
    {
        this.context = context;
        this.logger = logger;
    }

    [HttpGet("products")]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategoriesProducts()
    {
        logger.LogInformation("====================GET categories/products ========================");
        //Include = Método do EntityFramework para incluir, juntamente de categorias, todos os produtos relacionados
        return await context.Categories.Include(c => c.Products)
            .Where(c=>c.CategoryId <= 5).AsNoTracking().ToListAsync();
    }

    [HttpGet]
    [ServiceFilter(typeof(ApiLoggingFilter))] //Aplicando o filtro
    public async Task<ActionResult<IEnumerable<Category>>> Get()
    {
       
        return await context.Categories.AsNoTracking().ToListAsync();
        //AsNoTracking => Evita que as entidades sejam rastradas pelo EF. Recomendavel em consultas somente leituras. Assim não precisa rastrear essa consulta
        
    }
    
    [HttpGet("{id:int:min(1)}")] //Restrição no parâmetro
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public async Task<ActionResult<Category>> Get(int id)
    {
        var category = await context.Categories.FindAsync(id);
        if (category is null)
        {
             return NotFound("Category não encontrada");
        }
        return category;
    }

    [HttpPost]
    [ServiceFilter(typeof(ApiLoggingFilter))]
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
    [ServiceFilter(typeof(ApiLoggingFilter))]
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
    [ServiceFilter(typeof(ApiLoggingFilter))]
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