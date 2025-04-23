using appointly.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace appointly.DAL.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<EmployeeSalonService>()
            .HasOne(es => es.Employee)
            .WithMany(e => e.EmployeeSalonServices);

        modelBuilder
            .Entity<EmployeeSalonService>()
            .HasOne(es => es.SalonService)
            .WithMany(ss => ss.EmployeeSalonServices);
    }
}
