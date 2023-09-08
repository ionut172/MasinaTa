using System.Net;
using MongoDB.Driver;
using MongoDB.Entities;
using Polly;
using Polly.Extensions.Http;
using SearchService.Data;
using SearchService.Models;
using SearchService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
// 2. se adauga metoda 
builder.Services.AddHttpClient<LicitatiiHttpClient>().AddPolicyHandler(GetPolicy());
var app = builder.Build();

app.UseAuthorization();

app.MapControllers();
// 3. Se adauga lifetime-ul aplicatiei, astfel incat serviciul sa ruleze cu datele deja existente, chiar daca nu preia date actuale.
app.Lifetime.ApplicationStarted.Register(async () =>
{
    try
    {
        await DBInitializer.InitDB(app);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    };
});
app.Run();
// 1. Microsoft.Extensions.Http.Polly - asigura conexiunea independenta intre doua servicii;
// 1. Metoda HandleTransientHttpError -> handle erorr /// sau incarca permanent la fiecare 3 secunde.
static IAsyncPolicy<HttpResponseMessage> GetPolicy() => HttpPolicyExtensions.HandleTransientHttpError().OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound).WaitAndRetryForeverAsync(_ => TimeSpan.FromSeconds(3));
