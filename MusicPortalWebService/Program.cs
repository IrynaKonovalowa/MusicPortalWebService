using Microsoft.EntityFrameworkCore;
using MusicPortalWebAPIClient.Models;
using MusicPortalWebAPIClient.Repositories;


var builder = WebApplication.CreateBuilder(args);

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ClassContext>(options => options.UseLazyLoadingProxies().UseSqlServer(connection));
builder.Services.AddControllers();

builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IRepository<Genre>, GenreRepository>();
builder.Services.AddScoped<IRepository<Song>, SongRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();

var app = builder.Build();
//  CORS
//app.UseCors(builder => builder.AllowAnyOrigin());

app.UseCors(builder => builder.WithOrigins("https://localhost:7144")
                            .AllowAnyHeader()
                            .AllowAnyMethod());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
