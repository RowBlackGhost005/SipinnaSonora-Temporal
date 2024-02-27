using System.ComponentModel.DataAnnotations;

namespace apiSipinna.Models;

public class Lugar(){
    
    [Key]
    public Int32 idLugar {get; set;}

    public String? entidad {get; set;}
}