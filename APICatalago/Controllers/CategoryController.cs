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
        return context.Categories.Include(p => p.Products).ToList();
        //Include = Método do EntityFramework para incluir, juntamente de categorias, todos os produtos relacionados
    }

    [HttpGet]
    public ActionResult<IEnumerable<Category>> Get()
    {
        return context.Categories.ToList();
        
    }
    
    [HttpGet("{id:int}")]
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