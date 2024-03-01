using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apiSipinna.Models;

[Table("estadistica")]
public class Estadistica(){
    
    [Key]
    public Int32 idestadistica {get; set;}

    //public int cobertura { get; set; }
    [ForeignKey("cobertura")]
    public required Cobertura Cobertura { get; set; }

    //public int categoria { get; set; }
    [ForeignKey("categoria")]
    public required Categoria Categoria{ get; set; }

    //public int edades { get; set; }
    [ForeignKey("edades")]
    public required Edades Edades { get; set; }

    //public int lugar { get; set; }
    [ForeignKey("lugar")]
    public required Lugar Lugar { get; set; }

    //public int fecha { get; set; }
    [ForeignKey("fecha")]
    public required Fecha Fecha { get; set; }

    public required float dato { get; set; }

}