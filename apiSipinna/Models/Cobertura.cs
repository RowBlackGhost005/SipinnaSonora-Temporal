using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apiSipinna.Models;

public class Cobertura(){
    
    [Key]
    public Int32 idCobertura {get; set;}

    [Column(TypeName = "varchar(12)")]
    public required String alcance {get; set;}

    [Column(TypeName = "varchar(10)")]    
    public required String poblacion {get; set;}

    //public ICollection<Estadistica> estadisticas { get; } = new List<Estadistica>(); 
}