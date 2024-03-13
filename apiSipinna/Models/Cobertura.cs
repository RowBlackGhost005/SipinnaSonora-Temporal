using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apiSipinna.Models;

public class Cobertura{
    
    public Cobertura(){
        
    }
    public Cobertura(Int32 idCobertura,string alcance,string poblacion){
        this.idCobertura = idCobertura;
        this.alcance = alcance;
        this.poblacion = poblacion;
    }

    [Key]
    public  Int32 idCobertura {get; set;}

    [Column(TypeName = "varchar(12)")]
    public  String alcance {get; set;}

    [Column(TypeName = "varchar(10)")]    
    public  String poblacion {get; set;}

    //public ICollection<Estadistica> estadisticas { get; } = new List<Estadistica>(); 
}