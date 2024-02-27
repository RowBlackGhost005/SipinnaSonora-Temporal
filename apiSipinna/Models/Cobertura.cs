using System.ComponentModel.DataAnnotations;

namespace apiSipinna.Models;

public class Cobertura(){
    
    [Key]
    public Int32 idCobertura {get; set;}

    public String? alcance {get; set;}
    public String? poblacion {get; set;}


}