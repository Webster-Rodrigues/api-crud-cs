using APICatalago.Context;
using APICatalago.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalago.Controllers;

[Route("products")]
[ApiController]
public class ProductController : Controller
{
    private readonly AppDbContext context; //Acesso ao banco de dados

    public ProductController(AppDbContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> Get() //IEnumerable tem interface somente leitura; Permite adiar a execução;
                                                                 //Não preciso ter toda coleção na memória
    {
        return await context.Products.AsNoTracking().ToListAsync();
        //await => Indica que desejamos aguardar essa operação. Enquanto isso os recursos utilziados 
        //para processar essa requisição podem ser usados p/ processar outras requisições
        
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> Get(int id)
    {
        var product = await context.Products.FindAsync(id);
        if (product is null)
        {
            return NotFound("O Id informado não existe");
        }
        return product;
    }

    [HttpPost]
    public ActionResult Post(Product product)
    {
        context.Products.Add(product); //Inclui no contexto do EF
        context.SaveChanges(); //Salva no banco de dados
        return Created("", product);
    }

    
    [HttpPut("{id:int}")]
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
    public ActionResult Delete(int id)
    {
        var product = context.Products.Find(id);
        if (product is null)
        {
            return NotFound("Produto não localizado");
        }
        
        context.Products.Remove(product);
        context.SaveChanges();
        return Ok(product);
    }
}