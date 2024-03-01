using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apiSipinna.Models;

public class Edades(){
    
    [Key]
    public Int32 idedades {get; set;}

    [Column(TypeName = "varchar(10)")]
    public required String rangoEdades {get; set;}

    //public ICollection<Estadistica> estadisticas { get; } = new List<Estadistica>(); 
}