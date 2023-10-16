using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RedisExampleApp.API.Models;
using RedisExampleApp.API.Repository;
using RedisExampleApp.API.Services;
using RedisExampleApp.Cache;
using StackExchange.Redis;
using IDatabase = StackExchange.Redis.IDatabase;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Request response dönene kadar tek bir nesne örneði kullanýlsýn.
//Response döndüðü zaman yýkýlsýn.
//builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductRepository>(sp =>
{
    var appDbContext = sp.GetService<AppDbContext>();   
    var productRepository = new ProductRepository(appDbContext);
    var redisService = sp.GetRequiredService<RedisService>();

    return new ProductRepositoryWithCacheDecarator(productRepository,redisService);
});
builder.Services.AddScoped<IProductService, ProductServices>();

//Inmemory DB
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("myDatabase");
});

builder.Services.AddSingleton<RedisService>(sp =>
{
    return new RedisService(url: builder.Configuration["CacheOptions:Url"]);
});

builder.Services.AddSingleton<IDatabase>(sp =>
{
    var redisService = sp.GetRequiredService<RedisService>();
    return redisService.GetDb(0);
});

var app = builder.Build();

//Inmemory db için yapýlan iþlem
//Database sýfýrdan oluþturur.Varsa oluþturmaz.
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
