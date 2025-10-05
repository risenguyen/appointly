using appointly.DAL.Context;
using appointly.DAL.Repositories;
using appointly.DAL.Repositories.IRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace appointly.DAL.Extensions;

public static class ServiceExtensions
{
    public static void AddDAL(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
        );
        services
            .AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<ITreatmentRepository, TreatmentRepository>();
        services.AddScoped<IStaffRepository, StaffRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
    }
}
