using Lab4_1.Injection;
using Lab4_1.Models;
using Microsoft.EntityFrameworkCore;
using Lab4_1.Injection; // Простір імен для сервісів
using Lab4_1.Models;
using Lab4_1.ModelsUpdate;
using Lab4_1.ModelsView;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingProfile));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddDbContext<BookStoreContext>(options =>
    options.UseSqlServer("data source=DESKTOP-41EOJRI\\SQLEXPRESS;initial catalog=BookStoreDB;trusted_connection=true;TrustServerCertificate=true;"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

