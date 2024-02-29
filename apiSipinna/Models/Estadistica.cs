using System.ComponentModel.DataAnnotations;

namespace apiSipinna.Models;

public class Estadistica(){
    
    [Key]
    public Int32 idestadistica {get; set;}

    //public Cobertura? Cobertura {get; set;} = null!;
    public Int32? cobertura {get; set;} 

    //public Categoria? Categoria {get; set;} = null!;
    public Int32? categoria {get; set;}

    //public Edades? Edades {get; set;} = null!;
    public Int32? edades {get; set;}

    //public Lugar? Lugar {get; set;} = null!;
    public Int32? lugar {get; set;}

    //public Fecha? Fecha {get; set;} = null!;
    public Int32? fecha {get; set;}
    
    public float? dato {get; set;}

}