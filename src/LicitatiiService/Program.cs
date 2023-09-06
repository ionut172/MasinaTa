using LicitatiiService.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddControllers();
builder.Services.AddDbContext<LicitatiiDBContext>(opt=>{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMvc().AddJsonOptions(opt => {
    opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});
var app = builder.Build();

app.UseAuthorization();

app.MapControllers();
try{
DBInitializer.InitDB(app);
}
catch(Exception ex) {
    Console.WriteLine(ex.Message);
    throw new Exception("Eroare", ex);
}
app.Run();
