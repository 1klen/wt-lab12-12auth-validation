using Microsoft.EntityFrameworkCore;
using wt_lab5_5mvc_leshkevich.Models;

namespace wt_lab5_5mvc_leshkevich.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Car> Cars => Set<Car>();
    public DbSet<User> Users => Set<User>();
}