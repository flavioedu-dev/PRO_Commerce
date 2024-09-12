using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PROCommerce.Authentication.Domain.Entities;

namespace PROCommerce.Authentication.Infrastructure.Repository;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}