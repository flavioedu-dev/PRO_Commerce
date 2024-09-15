using Microsoft.EntityFrameworkCore;
using PROCommerce.Authentication.Domain.Entities;

namespace PROCommerce.Authentication.Infrastructure.Repositories;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}