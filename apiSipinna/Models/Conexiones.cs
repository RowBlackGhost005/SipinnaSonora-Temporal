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

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        //configurar tablas

      modelBuilder.Entity<Estadistica>()
       .HasOne<Categoria>()
       .WithMany()  
       .HasForeignKey(e => e.categoria);

      modelBuilder.Entity<Estadistica>()
       .HasOne<Cobertura>()
       .WithMany()  
       .HasForeignKey(e => e.cobertura);
       
        modelBuilder.Entity<Estadistica>()
       .HasOne<Edades>()
       .WithMany()  
       .HasForeignKey(e => e.edades);

       modelBuilder.Entity<Estadistica>()
       .HasOne<Fecha>()
       .WithMany()  
       .HasForeignKey(e => e.fecha);

       modelBuilder.Entity<Estadistica>()
       .HasOne<Lugar>()
       .WithMany()  
       .HasForeignKey(e => e.lugar);

        /*
        modelBuilder.Entity<Categoria>()
        .HasMany<Estadistica>()
        .WithOne();

        modelBuilder.Entity<Cobertura>()
        .HasMany<Estadistica>()
        .WithOne();

        modelBuilder.Entity<Edades>()
        .HasMany<Estadistica>()
        .WithOne();

        modelBuilder.Entity<Fecha>()
        .HasMany<Estadistica>()
        .WithOne();

        modelBuilder.Entity<Lugar>()
        .HasMany<Estadistica>()
        .WithOne();
       */
        modelBuilder.Entity<Estadistica>().Property(e => e.dato);
    }


}