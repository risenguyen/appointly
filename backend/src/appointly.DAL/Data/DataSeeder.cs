using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace appointly.DAL.Data;

public class DataSeeder
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;

    public DataSeeder(UserManager<IdentityUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task SeedAsync()
    {
        var adminEmail =
            _configuration["AdminUser:Email"]
            ?? throw new InvalidOperationException("Admin user email is not configured.");
        var adminPassword =
            _configuration["AdminUser:Password"]
            ?? throw new InvalidOperationException("Admin user password is not configured.");

        if (await _userManager.FindByEmailAsync(adminEmail) == null)
        {
            var user = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
            };
            await _userManager.CreateAsync(user, adminPassword);
        }
    }
}
