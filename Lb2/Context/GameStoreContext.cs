﻿using Microsoft.EntityFrameworkCore;

namespace GameStore.Entities;

public interface IGameStoreContext
{
    DbSet<Product> Products { get; set; }
    DbSet<Category> Categories { get; set; }
    DbSet<Currency> Currencies { get; set; }
}

public class GameStoreContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Currency> Currencies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=GameStore.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Налаштування зв'язків з каскадним видаленням
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Currency)
            .WithMany()
            .HasForeignKey(p => p.CurrencyId)
            .OnDelete(DeleteBehavior.Cascade); // Каскадне видалення

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany()
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Cascade); // Каскадне видалення
    }
}