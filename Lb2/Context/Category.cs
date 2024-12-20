﻿using System.ComponentModel.DataAnnotations;

namespace GameStore.Entities;

public interface ICategory
{
    int CategoryId { get; set; }
    string CategoryName { get; set; }
}

public class Category 
{
    [Key]
    public int CategoryId { get; set; }

    [Required]
    [MaxLength(50)]
    public string CategoryName { get; set; }
}