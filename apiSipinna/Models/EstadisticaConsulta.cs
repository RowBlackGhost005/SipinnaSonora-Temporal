namespace apiSipinna.Models;
public class EstadisticaConsulta
{
    public string? Dominio { get; set; }
    public string? Categoria { get; set; }
    public string? Indicador { get; set; }
    public string? Poblacion { get; set; }
    public string? RangoEdades { get; set; }
    public string? Entidad { get; set; }
    public int? Anio { get; set; }
    public float? Dato { get; set; }
}