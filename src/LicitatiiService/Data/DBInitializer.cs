
using LicitatiiService.Models;
using Microsoft.EntityFrameworkCore;

namespace LicitatiiService.Data;

public class DBInitializer {
    public static void InitDB(WebApplication app){
        using var scope = app.Services.CreateScope();
        SeedData(scope.ServiceProvider.GetService<LicitatiiDBContext>());
    }

    private static void SeedData(LicitatiiDBContext context)
    {
        context.Database.Migrate();
        if (context.Licitatii.Any()){
            Console.WriteLine("Exista deja date. Nu necesita seed.");
            return;
        }
        else {
            var licitatii = new List <Licitatie>() {
               	    // 1 Ford GT
            new Licitatie
            {
                Id = Guid.Parse("afbee524-5972-4075-8800-7d1f9d7b0a0c"),
                Status = Status.Live,
                PretRezervare = 20000,
                Vanzator = "bob",
                LicitatieEnd = DateTime.UtcNow.AddDays(10),
                Item = new ItemDB
                {
                    Make = "Ford",
                    ModelMasina = "GT",
                    Culoare = "White",
                    Kilometraj = 50000,
                    An = 2020,
                    ImagineUrl = "https://cdn.pixabay.com/photo/2016/05/06/16/32/car-1376190_960_720.jpg"
                }
            },
            // 2 Bugatti Veyron
            new Licitatie
            {
                Id = Guid.Parse("c8c3ec17-01bf-49db-82aa-1ef80b833a9f"),
                Status = Status.Live,
                PretRezervare = 90000,
                Vanzator = "alice",
                LicitatieEnd = DateTime.UtcNow.AddDays(60),
                Item = new ItemDB
                {
                    Make = "Bugatti",
                    ModelMasina = "Veyron",
                    Culoare = "Black",
                    Kilometraj = 15035,
                    An = 2018,
                    ImagineUrl = "https://cdn.pixabay.com/photo/2012/05/29/00/43/car-49278_960_720.jpg"
                }
            },
            // 3 Ford mustang
            new Licitatie
            {
                Id = Guid.Parse("bbab4d5a-8565-48b1-9450-5ac2a5c4a654"),
                Status = Status.Live,
                Vanzator = "bob",
                LicitatieEnd = DateTime.UtcNow.AddDays(4),
                Item = new ItemDB
                {
                    Make = "Ford",
                    ModelMasina = "Mustang",
                    Culoare = "Black",
                    Kilometraj = 65125,
                    An = 2023,
                    ImagineUrl = "https://cdn.pixabay.com/photo/2012/11/02/13/02/car-63930_960_720.jpg"
                }
            },
            // 4 Mercedes SLK
            new Licitatie
            {
                Id = Guid.Parse("155225c1-4448-4066-9886-6786536e05ea"),
                Status = Status.Revervat,
                PretRezervare = 50000,
                Vanzator = "tom",
                LicitatieEnd = DateTime.UtcNow.AddDays(-10),
                Item = new ItemDB
                {
                    Make = "Mercedes",
                    ModelMasina = "SLK",
                    Culoare = "Silver",
                    Kilometraj = 15001,
                    An = 2020,
                    ImagineUrl = "https://cdn.pixabay.com/photo/2016/04/17/22/10/mercedes-benz-1335674_960_720.png"
                }
            },
            // 5 BMW X1
            new Licitatie
            {
                Id = Guid.Parse("466e4744-4dc5-4987-aae0-b621acfc5e39"),
                Status = Status.Live,
                PretRezervare = 20000,
                Vanzator = "alice",
                LicitatieEnd = DateTime.UtcNow.AddDays(30),
                Item = new ItemDB
                {
                    Make = "BMW",
                    ModelMasina = "X1",
                    Culoare = "White",
                    Kilometraj = 90000,
                    An = 2017,
                    ImagineUrl = "https://cdn.pixabay.com/photo/2017/08/31/05/47/bmw-2699538_960_720.jpg"
                }
            },
            // 6 Ferrari spider
            new Licitatie
            {
                Id = Guid.Parse("dc1e4071-d19d-459b-b848-b5c3cd3d151f"),
                Status = Status.Live,
                PretRezervare = 20000,
                Vanzator = "bob",
                LicitatieEnd = DateTime.UtcNow.AddDays(45),
                Item = new ItemDB
                {
                    Make = "Ferrari",
                    ModelMasina = "Spider",
                    Culoare = "Red",
                    Kilometraj = 50000,
                    An = 2015,
                    ImagineUrl = "https://cdn.pixabay.com/photo/2017/11/09/01/49/ferrari-458-spider-2932191_960_720.jpg"
                }
            },
            // 7 Ferrari F-430
            new Licitatie
            {
                Id = Guid.Parse("47111973-d176-4feb-848d-0ea22641c31a"),
                Status = Status.Live,
                PretRezervare = 150000,
                Vanzator = "alice",
                LicitatieEnd = DateTime.UtcNow.AddDays(13),
                Item = new ItemDB
                {
                    Make = "Ferrari",
                    ModelMasina = "F-430",
                    Culoare = "Red",
                    Kilometraj = 5000,
                    An = 2022,
                    ImagineUrl = "https://cdn.pixabay.com/photo/2017/11/08/14/39/ferrari-f430-2930661_960_720.jpg"
                }
            },
            // 8 Audi R8
            new Licitatie
            {
                Id = Guid.Parse("6a5011a1-fe1f-47df-9a32-b5346b289391"),
                Status = Status.Live,
                Vanzator = "bob",
                LicitatieEnd = DateTime.UtcNow.AddDays(19),
                Item = new ItemDB
                {
                    Make = "Audi",
                    ModelMasina = "R8",
                    Culoare = "White",
                    Kilometraj = 10050,
                    An = 2021,
                    ImagineUrl = "https://cdn.pixabay.com/photo/2019/12/26/20/50/audi-r8-4721217_960_720.jpg"
                }
            },
            // 9 Audi TT
            new Licitatie
            {
                Id = Guid.Parse("40490065-dac7-46b6-acc4-df507e0d6570"),
                Status = Status.Live,
                PretRezervare = 20000,
                Vanzator = "tom",
                LicitatieEnd = DateTime.UtcNow.AddDays(20),
                Item = new ItemDB
                {
                    Make = "Audi",
                    ModelMasina = "TT",
                    Culoare = "Black",
                    Kilometraj = 25400,
                    An = 2020,
                    ImagineUrl = "https://cdn.pixabay.com/photo/2016/09/01/15/06/audi-1636320_960_720.jpg"
                }
            },
            // 10 Ford ModelMasina T
            new Licitatie
            {
                Id = Guid.Parse("3659ac24-29dd-407a-81f5-ecfe6f924b9b"),
                Status = Status.Live,
                PretRezervare = 20000,
                Vanzator = "bob",
                LicitatieEnd = DateTime.UtcNow.AddDays(48),
                Item = new ItemDB
                {
                    Make = "Ford",
                    ModelMasina = "ModelMasina T",
                    Culoare = "Rust",
                    Kilometraj = 150150,
                    An = 1938,
                    ImagineUrl = "https://cdn.pixabay.com/photo/2017/08/02/19/47/vintage-2573090_960_720.jpg"
                }
            } 
            };
            context.AddRange(licitatii);
            context.SaveChanges();
        }
    }
}