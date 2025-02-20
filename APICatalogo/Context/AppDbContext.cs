using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Context;

//Classe de contexto é responsável por realizar a comunicação entre as entidades e o banco de dados
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    { }

    //Mapeamento obj relacional
    public DbSet<Product>? Products { get; set; }
    public DbSet<Category>? Categories { get; set; }
    
}