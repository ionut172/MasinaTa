using LicitatiiService.Data;
using LicitatiiService.DTO;
using LicitatiiService.RequestHelpers;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.Entities;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddControllers();
// Se adauga serviciul de licitatii cu parametri de configurare pentru string connection din appsettings.json pentru postgresql
builder.Services.AddDbContext<LicitatiiDBContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddEndpointsApiExplorer();
// Se adauga automapper-ul Automapper;
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMvc().AddJsonOptions(opt =>
{
    // Face suppress la json values by default ca $id, $values;
    opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();
try
{
    DBInitializer.InitDB(app);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    throw new Exception("Eroare", ex);
}

app.Run();

