//contexto de base de datos

using Microsoft.EntityFrameworkCore;

namespace apiSipinna.Models;

public class Conexiones : DbContext{
    public Conexiones(DbContextOptions<Conexiones> options) :base (options){

    }

    public DbSet<Estadistica> estadistica {get; set;} = null!;

}