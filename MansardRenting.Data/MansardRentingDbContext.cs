using MansardRenting.Data.Configurations;
using MansardRenting.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MansardRenting.Data;

public class MansardRentingDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public MansardRentingDbContext(DbContextOptions<MansardRentingDbContext> options)
        : base(options)
    {
    }

    public DbSet<Agent> Agents { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<House> Houses { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        Assembly configAssembly = Assembly.GetAssembly(typeof(MansardRentingDbContext)) ??
                                  Assembly.GetExecutingAssembly();
        builder.ApplyConfigurationsFromAssembly(configAssembly);
        //builder.ApplyConfiguration(new HouseEntityConfiguration());
        //builder.ApplyConfiguration(new CategoryEntityConfiguration());

        base.OnModelCreating(builder);
    }
}