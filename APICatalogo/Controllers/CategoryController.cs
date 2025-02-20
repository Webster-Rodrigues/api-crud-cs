using APICatalogo.Context;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("categories")]
[ApiController]
public class CategoryController : Controller
{
    private readonly ICategoryRepository categoryRepository;


    public CategoryController(ICategoryRepository categoryRepository)
    {
        this.categoryRepository = categoryRepository;
    }

    [HttpGet("products")]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public ActionResult<IEnumerable<Category>> GetCategoriesProducts()
    {
        var categories = categoryRepository.GetCategoriesProducts();
        return Ok(categories);
    }

    [HttpGet]
    [ServiceFilter(typeof(ApiLoggingFilter))] //Aplicando o filtro
    public ActionResult<IEnumerable<Category>> Get()
    {
        return Ok(categoryRepository.GetAll());
        
    }
    
    [HttpGet("{id:int:min(1)}")] //Restrição no parâmetro
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public ActionResult<Category> Get(int id)
    {
        var category =  categoryRepository.GetById(id);
        if (category is null)
        {
             return NotFound("Category não encontrada");
        }
        return Ok(category);
    }

    [HttpPost]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public ActionResult<Category> Post(Category category)
    {
        var createdCategory = categoryRepository.Create(category);

        return CreatedAtAction(nameof(Get), new { id = createdCategory.CategoryId}, createdCategory);
    }

    [HttpPut("{id:int}")]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public ActionResult<Category> Put(int id, Category category)
    {
        if (id != category.CategoryId)
        {
            return BadRequest();
        }
        
        return Ok(categoryRepository.Update(category));
    }

    [HttpDelete("{id:int}")]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public ActionResult<Category> Delete(int id)
    {
        var category = categoryRepository.GetById(id);

        if (category is null)
        {
            return NotFound("Id informado não existe");
        }

        categoryRepository.Delete(category);
        return NoContent();
    }
    
    
}