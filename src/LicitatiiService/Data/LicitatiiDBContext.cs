using LicitatiiService.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace LicitatiiService.Data;

public class LicitatiiDBContext : DbContext {
    public LicitatiiDBContext( DbContextOptions options) : base(options){

    } 

    public DbSet<Licitatie> Licitatii { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
    }
}