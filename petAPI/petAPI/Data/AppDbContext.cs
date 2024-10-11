using Microsoft.EntityFrameworkCore;
using petAPI.Models;
namespace petAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext (DbContextOptions options) : base(options) 
    {
    }
    public DbSet<Animal> Animal {get; set;} = null!;
}