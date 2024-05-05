using Automarket.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Automarket.DAL
{
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)     {}
    public DbSet<Car> Cars { get; set; }
	public DbSet<CarImage> CarImages { get; set; }
	public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
}
}
