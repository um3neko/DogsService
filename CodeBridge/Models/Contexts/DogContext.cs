using CodeBridge.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeBridge.Models.Contexts;

public class DogContext : DbContext
{
    public DogContext(DbContextOptions options) : base(options) { }
    
    public virtual DbSet<Dog> Dogs { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}
