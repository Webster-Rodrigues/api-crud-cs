using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalago.Models;

[Table("categories")]
public class Category
{
    public Category()
    {
        Products = new Collection<Product>();
    }
    
    [Key] //Apesar de não precisar. Mapeia a propriedade em uma chave primária 
    public int CategoryId { get; set;}
    
    [Required] //Define que a coluna vai ser NOT NULL
    [StringLength(80)] //Define tamanho do campo
    public string? Name { get; set;}
    
    [Required]
    [StringLength(300)]
    public string? ImageUrl { get; set;}

    public ICollection<Product>? Products { get; set; }
}