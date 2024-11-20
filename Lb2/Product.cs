using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Entities;

public class Product
{
    [Key] public int ProductId { get; set; }

    [Required] [MaxLength(100)] public string Name { get; set; }

    [Required] public decimal Price { get; set; }

    [ForeignKey("Currency")] public int CurrencyId { get; set; }
    public Currency Currency { get; set; }

    [ForeignKey("Category")] public int CategoryId { get; set; }
    public Category Category { get; set; }
}