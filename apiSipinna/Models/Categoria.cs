using System.ComponentModel.DataAnnotations;

namespace apiSipinna.Models;

public class Categoria(){
    
    [Key]
    public Int32 idCategoria {get; set;}

    public String? dominio {get; set;}
    public String? categoria {get; set;}
    public String? indicador {get; set;}

}