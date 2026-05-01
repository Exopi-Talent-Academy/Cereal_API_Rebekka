using Cereal_API.Repositories;
using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Register services:
var services = builder.Services;

try
{
    using var connection = new SqliteConnection(@"Data Source=C:\Dev\ExOpi\Cereal_API_Rebekka\Cereals.db");
    connection.Open();
} 
catch (SqliteException ex)
{
    Console.WriteLine(ex.Message);
}

services.AddTransient<ICerealRepository, CerealRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
