using APICatalogo.Context;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace APICatalogo.Controllers;

[Route("categories")]
[ApiController]
public class CategoryController : Controller
{
    private readonly IUnitOfWork repository;

    public CategoryController(IUnitOfWork repository)
    {
        this.repository = repository;
    }

    [HttpGet("products")]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public ActionResult<IEnumerable<Category>> GetCategoriesProducts()
    {
        var categories = repository.CategoryRepository.GetCategoriesProducts();
        return Ok(categories);
    }

    [HttpGet]
    [ServiceFilter(typeof(ApiLoggingFilter))] //Aplicando o filtro
    public ActionResult<IEnumerable<Category>> Get()
    {
        return Ok(repository.CategoryRepository.GetAll());
    }

    [HttpGet("{id:int:min(1)}")] //Restrição no parâmetro
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public ActionResult<Category> Get(int id)
    {
        var category = repository.CategoryRepository.GetById(id);
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
        var createdCategory = repository.CategoryRepository.Create(category);
        repository.Commit();

        return CreatedAtAction(nameof(Get), new { id = createdCategory.CategoryId }, createdCategory);
    }

    [HttpPut("{id:int}")]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public ActionResult<Category> Put(int id, Category category)
    {
        if (id != category.CategoryId)
        {
            return BadRequest();
        }

        repository.CategoryRepository.Update(category);
        repository.Commit();
        return Ok(category);
    }

    [HttpDelete("{id:int}")]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public ActionResult<Category> Delete(int id)
    {
        var category = repository.CategoryRepository.GetById(id);

        if (category is null)
        {
            return NotFound("Id informado não existe");
        }

        repository.CategoryRepository.Delete(category);
        repository.Commit();
        return NoContent();
    }
}