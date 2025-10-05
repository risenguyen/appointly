using System.Reflection;
using appointly.BLL.Services;
using appointly.BLL.Services.IServices;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace appointly.BLL.Extensions;

public static class ServiceExtensions
{
    public static void AddBLL(this IServiceCollection services)
    {
        services.AddScoped<ITreatmentService, TreatmentService>();
        services.AddScoped<IStaffService, StaffService>();
        services.AddScoped<IClientService, ClientService>();

        services.AddValidatorsFromAssembly(Assembly.Load("appointly.BLL"));
        services.AddFluentValidationAutoValidation();
    }
}
