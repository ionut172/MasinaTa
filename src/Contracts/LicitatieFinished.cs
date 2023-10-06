namespace Contracts;

public class LicitatieFinished
{
    public bool ItemSold { get; set; }
    public string LicitatieId { get; set; }
    public string Castigator { get; set; }
    public string Vanzator { get; set; }
    public int pretRezervare { get; set; }
}