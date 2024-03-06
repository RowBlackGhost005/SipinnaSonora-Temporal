using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apiSipinna.Models;

public class Categoria{
    
    public Categoria(){
        
    }
    public Categoria(Int32 idCategoria,string dominio,string categoria, string indicador){
        this.idCategoria = idCategoria;
        this.dominio = dominio;
        this.categoria = categoria;
        this.indicador = indicador;
    }

    [Key]
    public Int32 idCategoria {get; set;}

    [Column(TypeName = "varchar(45)")]
    public  String? dominio {get; set;}

    [Column(TypeName = "varchar(120)")]
    public  String categoria {get; set;}

    [Column(TypeName = "varchar(120)")]    
    public  String indicador {get; set;}

    //public ICollection<Estadistica> estadisticas { get; } = new List<Estadistica>(); 
}