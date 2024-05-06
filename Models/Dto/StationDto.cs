namespace FuelPredictor.Models.Dto
{
   public class StationDto
{
    public int Id { get; set; }
    public string? Nom { get; set; }
    public string? Adresse { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public int IdVille { get; set; }
    public int IdCompany { get; set; }
    public string? Ville { get; set; }
    public string? Company { get; set; }
    public float PrixGasoilAujourdhui { get; set; }
    public float PrixEssenceAujourdhui { get; set; }
}
}
