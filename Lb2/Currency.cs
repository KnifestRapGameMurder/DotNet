using System.ComponentModel.DataAnnotations;

namespace GameStore.Entities;

public class Currency
{
    [Key]
    public int CurrencyId { get; set; }

    [Required]
    [MaxLength(50)]
    public string CurrencyName { get; set; }

    [Required]
    [MaxLength(5)]
    public string Symbol { get; set; }
}