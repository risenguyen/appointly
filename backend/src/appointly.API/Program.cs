using appointly.BLL.Extensions;
using appointly.DAL.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddBLL();
builder.Services.AddDAL(builder.Configuration);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors(corsPolicyBuilder =>
    corsPolicyBuilder
        .WithOrigins(
            builder.Configuration.GetValue<string>("Cors:AllowedOrigins")?.Split(",")
                ?? ["http://localhost:5173"]
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
);

app.UseAuthorization();

app.MapControllers();

app.Run();
