using APICatalogo.Context;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("products")]
[ApiController]
public class ProductController : Controller
{
    private readonly IRepository<Product> repository;

    public ProductController(IRepository<Product> repository)
    {
        this.repository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> Get() 
    {
         return Ok(repository.GetAll());

    }

    [HttpGet("{id:int}")]
    public ActionResult<Product> Get(int id)
    {
        var product = repository.GetById(id);
        if (product is null)
        {
            return NotFound("O Id informado não existe");
        }
        return Ok(product);
    }

    [HttpPost]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public ActionResult Post(Product product)
    {
        var createdProduct = repository.Create(product);
        return CreatedAtAction(nameof(Get), new { id = createdProduct.ProductId }, createdProduct);
    }

    
    [HttpPut("{id:int}")]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public ActionResult Put(int id, Product product)
    {
        if (id != product.ProductId)
        {
            return BadRequest(); //400
        }
        return Ok(repository.Update(product));
    }

    [HttpDelete("{id:int}")]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public ActionResult Delete(int id)
    {
        var product = repository.GetById(id);
        if (product is null)
        {
            return NotFound("Produto não localizado");
        }

        repository.Delete(product);
        return NoContent();
    }
}