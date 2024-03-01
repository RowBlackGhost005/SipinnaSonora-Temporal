using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apiSipinna.Models;

public class Fecha(){
    
    [Key]
    public Int32 idfecha {get; set;}

    [Column(TypeName = "INT")]
    public required Int32 anio {get; set;}

    [Column(TypeName = "varchar(10)")]
    public required String mes {get; set;}


    //public ICollection<Estadistica> estadisticas { get; } = new List<Estadistica>(); 
}