using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apiSipinna.Models;

public class Categoria(){
    
    [Key]
    public Int32 idCategoria {get; set;}

    [Column(TypeName = "varchar(45)")]
    public required String? dominio {get; set;}

    [Column(TypeName = "varchar(120)")]
    public required String categoria {get; set;}

    [Column(TypeName = "varchar(120)")]    
    public required String indicador {get; set;}

    //public ICollection<Estadistica> estadisticas { get; } = new List<Estadistica>(); 
}