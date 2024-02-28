//contexto de base de datos

using Microsoft.EntityFrameworkCore;

namespace apiSipinna.Models;

public class Conexiones : DbContext{
    public Conexiones(DbContextOptions<Conexiones> options) :base (options){

    }

    public DbSet<Estadistica> estadistica {get; set;} = null!;
    public DbSet<Categoria> categoria {get;set;} = null!;

    public DbSet<Cobertura> cobertura {get;set;} = null!;

    public DbSet<Edades> edades {get;set;} = null!;

    public DbSet<Fecha> fecha {get;set;} = null!;

    public DbSet<Lugar> lugar {get;set;} = null!;
}