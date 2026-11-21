namespace wt_lab5_5mvc_leshkevich.Models;

public class Car
{
    public int Id { get; set; }
    public string BrandModel { get; set; } = "";
    public string Category { get; set; } = "";
    public decimal PricePerDay { get; set; }
    public bool IsAvailable { get; set; }
    public string Description { get; set; } = "";
}