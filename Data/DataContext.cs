using Microsoft.EntityFrameworkCore;

namespace superhero.Data;

public class DataContext : DbContext
{
    public DbSet<SuperHero> SuperHeros { get; set; }
    public DataContext(DbContextOptions<DataContext> options)
    : base(options) { }
}