using System.ComponentModel.DataAnnotations;

namespace apiSipinna.Models;

public class Fecha(){
    
    [Key]
    public Int32 idfecha {get; set;}

    public Int32? anio {get; set;}
    public String? mes {get; set;}

}