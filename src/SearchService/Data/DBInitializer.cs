using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Models;
using SearchService.Services;
using System;
using System.Text.Json;
namespace SearchService.Data;

public class DBInitializer
{
    public static async Task InitDB(WebApplication app)
    {
        await DB.InitAsync("SearchDB", MongoClientSettings.FromConnectionString(app.Configuration.GetConnectionString("MongoDbConnection")));
        await DB.Index<Item>().Key(x => x.Make, KeyType.Text).Key(x=>x.Culoare, KeyType.Text).Key(x=>x.ModelMasina, KeyType.Text).CreateAsync();
        var count = await DB.CountAsync<Item>();
        // if (count == 0)
        // {
        //     Console.WriteLine("Nu sunt date. Astept sa seed-uiesc");
        //     try
        //     {
        //         var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "licitatii.json");
        //         var itemData = await File.ReadAllTextAsync(filePath);
        //         Console.WriteLine("JSON Data:");
        //         Console.WriteLine(itemData); // Add this line to print the JSON data
        //         var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        //         var items = JsonSerializer.Deserialize<List<Item>>(itemData, options);
        //         foreach (var item in items)
        //         {
        //             Console.WriteLine(item);
        //         }
        //         if (items.Count == 0)
        //         {
        //             Console.WriteLine("0 items. Not reading.");
        //         }
        //         await DB.SaveAsync(items);
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine($"Error reading 'licitatii.json': {ex.Message}");
        //     }
        using var scope = app.Services.CreateScope();
        Console.WriteLine("Am realizat scope.");
        var httpClient = scope.ServiceProvider.GetRequiredService<LicitatiiHttpClient>();
        Console.WriteLine("Am luat serviciul");
        try{
        var items = await httpClient.GetItemsForSearchDB();
        Console.WriteLine(items.Count.ToString()+" elemente");
        Console.WriteLine(items.Count + " returned from the licitatii service");
        if( items.Count > 0) await DB.SaveAsync(items);
        }
        catch (Exception ex){
            Console.WriteLine(ex.Message);
        }
       


        

    }
}