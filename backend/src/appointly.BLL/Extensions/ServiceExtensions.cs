using appointly.BLL.Services;
using appointly.BLL.Services.IServices;
using Microsoft.Extensions.DependencyInjection;

namespace appointly.BLL.Extensions;

public static class ServiceExtensions
{
    public static void AddBLL(this IServiceCollection services)
    {
        services.AddScoped<ITreatmentService, TreatmentService>();
    }
}
