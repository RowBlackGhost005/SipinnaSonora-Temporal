using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apiSipinna.Models;

public class Lugar(){
    
    [Key]
    public Int32 idLugar {get; set;}

    [Column(TypeName = "varchar(32)")]
    public required String entidad {get; set;}

    //public ICollection<Estadistica> estadisticas { get; } = new List<Estadistica>(); 

}