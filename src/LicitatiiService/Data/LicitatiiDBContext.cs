using LicitatiiService.Models;
using Microsoft.EntityFrameworkCore;

namespace LicitatiiService.Data;

public class LicitatiiDBContext : DbContext {
    public LicitatiiDBContext( DbContextOptions options) : base(options){

    } 

    public DbSet<Licitatie> Licitatii { get; set; } 
}