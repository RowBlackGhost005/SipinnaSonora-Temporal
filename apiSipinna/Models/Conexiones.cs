//contexto de base de datos

using Microsoft.EntityFrameworkCore;

namespace apiSipinna.Models;

public class Conexiones : DbContext{
    public Conexiones(DbContextOptions<Conexiones> options) :base (options){

    }

    public DbSet<Estadistica> estadisticaTbl {get; set;} = null!;
    public DbSet<Categoria> categoriaTbl {get;set;} = null!;

    public DbSet<Cobertura> coberturaTbl {get;set;} = null!;

    public DbSet<Edades> edadesTbl {get;set;} = null!;

    public DbSet<Fecha> fechaTbl {get;set;} = null!;

    public DbSet<Lugar> lugarTbl {get;set;} = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        //configurar tablas
        modelBuilder.Entity<Estadistica>().ToTable("estadistica");
        modelBuilder.Entity<Categoria>().ToTable("categoria");
        modelBuilder.Entity<Cobertura>().ToTable("cobertura");
        modelBuilder.Entity<Edades>().ToTable("edades");
        modelBuilder.Entity<Fecha>().ToTable("fecha");
        modelBuilder.Entity<Lugar>().ToTable("lugar");

        //valor unico 
        modelBuilder.Entity<Edades>()
            .HasAlternateKey(c => c.rangoEdades);

        modelBuilder.Entity<Lugar>()
            .HasAlternateKey(c => c.entidad);    
    }


}