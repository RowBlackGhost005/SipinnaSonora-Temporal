namespace apiSipinna.Models;
public class EstadisticaConsulta
{
    public required string Dominio { get; set; }
    public required string Categoria { get; set; }
    public required string Indicador { get; set; }
    public required string Poblacion { get; set; }
    public required string RangoEdades { get; set; }
    public required string Entidad { get; set; }
    public required int Anio { get; set; }
    public required float Dato { get; set; }
}