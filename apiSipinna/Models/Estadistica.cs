using System.ComponentModel.DataAnnotations;

namespace apiSipinna.Models;

public class Estadistica(){
    
    [Key]
    public Int32 idestadistica {get; set;}

    public Int32? cobertura {get; set;}
    public Int32? categoria {get; set;}
    public Int32? lugar {get; set;}
    public Int32? edades {get; set;}
    public Int32? fecha {get; set;}
    public float? dato {get; set;}
}