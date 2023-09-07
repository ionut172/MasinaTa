using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Data;
using SearchService.Models;
using SearchService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
builder.Services.AddHttpClient<LicitatiiHttpClient>();
var app = builder.Build();

app.UseAuthorization();

app.MapControllers();
try{
DBInitializer.InitDB(app);
}
catch(Exception ex){
    Console.WriteLine(ex.ToString());
}
app.Run();
