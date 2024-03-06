using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apiSipinna.Models;

public class Fecha{
    
    public Fecha(){
        
    }
    public Fecha(Int32 idfecha,Int32 anio, string mes){
        this.idfecha = idfecha;
        this.anio = anio;
        this.mes = mes;
    }

    [Key]
    public Int32 idfecha {get; set;}

    [Column(TypeName = "INT")]
    public Int32 anio {get; set;}

    [Column(TypeName = "varchar(10)")]
    public  String mes {get; set;}


    //public ICollection<Estadistica> estadisticas { get; } = new List<Estadistica>(); 
}