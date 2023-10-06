using MongoDB.Entities;

namespace Models;


public class Licitatie : Entity {
    public DateTime LicitatieDateEnd { get; set; }
    public string vanzator {get;set;}
    public int pretRezervare {get;set;}    
    public bool Finished {get;set;}          
}