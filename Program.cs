using Microsoft.EntityFrameworkCore;
using SkepERP.Data;
using SkepERP.Interfaces;
using SkepERP.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // comment
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
