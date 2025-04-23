using appointly.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace appointly.DAL.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{
    public DbSet<Treatment> Treatments { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<EmployeeTreatment> EmployeeTreatments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<EmployeeTreatment>()
            .HasOne(et => et.Employee)
            .WithMany(e => e.EmployeeTreatments);

        modelBuilder
            .Entity<EmployeeTreatment>()
            .HasOne(et => et.Treatment)
            .WithMany(ss => ss.EmployeeTreatments);
    }
}
