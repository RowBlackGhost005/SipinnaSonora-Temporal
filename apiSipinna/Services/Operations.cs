using apiSipinna.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Connections;
using apiSipinna.Utils;
using Microsoft.AspNetCore.Http.Timeouts;

namespace apiSipinna.CRUD{

    public class Operations : IOperations{

        Conexiones Conexion;
        Logger logger;

        public Operations(Conexiones _conexion, bool debug = true){
            Conexion = _conexion;
            logger = new Logger(debug);
        }

        public async Task<bool> Create(object cat){
            if(cat != null){
                try{
                    logger.log($"Ingresando {cat.GetType().Name}");
                    await Conexion.AddAsync(cat);
                    logger.log($"Salvando cambios de inserción... {cat.GetType().Name}");
                    await Conexion.SaveChangesAsync();
                    logger.log("Datos salvados con éxito");
                    return true;
                }catch(OperationCanceledException e){
                    logger.log($"Error al ingresar {cat.GetType().Name}: "+e.Message);
                    return false;
                }catch(Exception e){
                    if(e is OperationCanceledException || e is DbUpdateException || e is DbUpdateConcurrencyException || e is OperationCanceledException){
                        logger.log($"Error!!! Tipo: {e.GetType().Name}. Mensaje: {e.Message}");
                    }
                    return false;
                }
            }else{
                return false;
            }
        }

        public async Task<Categoria> ReadCategoria(int id){
            if(id<=0){
                throw new Exception("'id' no puede ser igual o menor que '0'");
            }else{
                return await Conexion.categoriaTbl.FindAsync(id);
            }
        }

    }

}