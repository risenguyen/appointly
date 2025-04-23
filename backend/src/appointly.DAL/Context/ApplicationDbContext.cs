using Microsoft.EntityFrameworkCore;

namespace appointly.DAL.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options) { }
