using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apiSipinna.Models;

public class Lugar{
    
    public Lugar(){
        
    }
    public Lugar(Int32 idlugar, String entidad){
        this.idLugar = idlugar;
        this.entidad = entidad;
    }

    [Key]
    public Int32 idLugar {get; set;}

    [Column(TypeName = "varchar(32)")]
    public String entidad {get; set;}

    //public ICollection<Estadistica> estadisticas { get; } = new List<Estadistica>(); 

}