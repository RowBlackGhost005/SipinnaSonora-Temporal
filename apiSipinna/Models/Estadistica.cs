using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apiSipinna.Models;



[Table("estadistica")]
public class Estadistica{
    
    public Estadistica(){
        
    }
    public Estadistica(Int32 idestadistica, Cobertura cobertura, Categoria categoria, Edades edades, Lugar lugar, Fecha fecha, float dato)
    {
        this.idestadistica = idestadistica;
        this.CoberturaNav = cobertura;
        this.CategoriaNav = categoria;
        this.EdadesNav = edades;
        this.LugarNav = lugar;
        this.FechaNav = fecha;
        this.dato = dato;
    }

    [Key]
    public Int32 idestadistica {get; set;}

    public int cobertura { get; set; }
    [ForeignKey(nameof(cobertura))]
    public virtual Cobertura CoberturaNav { get; set; }

    public int categoria { get; set; }
    [ForeignKey(nameof(categoria))]
    public virtual Categoria CategoriaNav{ get; set; }

    public int edades { get; set; }
    [ForeignKey(nameof(edades))]
    public virtual Edades EdadesNav { get; set; }

    public int lugar { get; set; }
    [ForeignKey(nameof(lugar))]
    public virtual Lugar LugarNav { get; set; }

    public int fecha { get; set; }
    [ForeignKey(nameof(fecha))]
    public virtual Fecha FechaNav { get; set; }

    [Column(TypeName = "DECIMAL(9,1)")]
    public  float dato { get; set; }

}