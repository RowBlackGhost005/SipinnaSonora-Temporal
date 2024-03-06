using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apiSipinna.Models;

public class Edades{
    
    public Edades(){

    }
    
    public Edades(Int32 idedades,string rangoEdades){
        this.idedades = idedades;
        this.rangoEdades = rangoEdades;
    }

    [Key]
    public Int32 idedades {get; set;}

    [Column(TypeName = "varchar(10)")]
    public String rangoEdades {get; set;}

    //public ICollection<Estadistica> estadisticas { get; } = new List<Estadistica>(); 
}