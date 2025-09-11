using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestAngularApp.Server.Data;
using TestAngularApp.Server.Repositories.Implementation;
using TestAngularApp.Server.Repositories.Interface;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("TestData");

// Configure DbContext with the connection string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<ICategoryRepository,CategoryRepoitory>();
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//allow application to access files from the application
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider=new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Images")),
    RequestPath="/Images"
});

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
