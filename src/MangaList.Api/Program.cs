using MangaList.Application;
using MangaList.Infrastructure;
using MangaList.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();  // Enable controller's logic
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Enable CORS for Frontend application
app.UseCors(builder =>
{
    builder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
});

app.UseHttpsRedirection();

app.MapControllers();   // Add controller's endpoints

/* This code snippet is creating a scope within which a database context (`MangaDbContext`) is being
retrieved from the application's services and then ensuring that the database associated with that
context is created if it does not already exist. */
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MangaDbContext>();
    dbContext.Database.EnsureCreated();
}

app.Run();
