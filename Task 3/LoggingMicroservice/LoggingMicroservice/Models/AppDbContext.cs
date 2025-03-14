using Microsoft.EntityFrameworkCore;

namespace LoggingMicroservice.Models;

public class AppDbContext : DbContext
{
    public DbSet<Log> Logs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=task3;Username=steven;Password=0000");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Log>()
            .HasIndex(l => l.RequestId)
            .IsUnique(); // Ensure RequestId is unique
    }


}