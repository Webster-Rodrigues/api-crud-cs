using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using APICatalago.Validations;

namespace APICatalago.Models;

[Table("Products")]
public class Product
{
    [Key]
    public int ProductId { get; set; }
    
    [Required]
    [StringLength(80)]
    [FirstUpperLetter]
    public string? Name { get; set; }
    
    [Required]
    [StringLength(300)]
    public string? Description{ get; set; }
    
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }
    
    [Required]
    [StringLength(300)]
    public string? ImageUrl { get; set; }
    
    public float Stock { get; set; }
    public DateTime DateRegister { get; set; }
    public int CategoryId { get; set; }
    
    [JsonIgnore]
    public Category? Category { get; set; }
    
}