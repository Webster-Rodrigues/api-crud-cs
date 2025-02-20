using APICatalago.Context;
using APICatalago.Filters;
using APICatalago.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalago.Controllers;

[Route("products")]
[ApiController]
public class ProductController : Controller
{
    private readonly AppDbContext context; //Acesso ao banco de dados
    private readonly ILogger<CategoryController> logger;

    public ProductController(AppDbContext context, ILogger<CategoryController> logger)
    {
        this.context = context;
        this.logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> Get() //IEnumerable tem interface somente leitura; Permite adiar a execução. Não preciso ter toda coleção na memória
                                                                 
    {
        logger.LogInformation("====================GET products ========================");
        return await context.Products.AsNoTracking().ToListAsync();
        
        
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> Get(int id)
    {
        var product = await context.Products.FindAsync(id);
        
        logger.LogInformation("==================== GET products/id = {Id} ========================", id);
        if (product is null)
        {
            logger.LogInformation("==================== GET products/id = {Id} NOT FOUND ========================", id);
            return NotFound("O Id informado não existe");
        }
        return product;
    }

    [HttpPost]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public ActionResult Post(Product product)
    {
        context.Products?.Add(product); //Inclui no contexto do EF
        context.SaveChanges(); //Salva no banco de dados
        return Created("", product);
    }

    
    [HttpPut("{id:int}")]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public ActionResult Put(int id, Product product)
    {
        if (id != product.ProductId)
        {
            return BadRequest(); //400
        }
        //Contexto precisa saber que a entidade pode ser modificada
        context.Entry(product).State = EntityState.Modified;
        context.SaveChanges();
        
        return Ok(product);
    }

    [HttpDelete("{id:int}")]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public ActionResult Delete(int id)
    {
        var product = context.Products?.Find(id);
        if (product is null)
        {
            return NotFound("Produto não localizado");
        }
        
        context.Products.Remove(product);
        context.SaveChanges();
        return Ok(product);
    }
}