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

        public Task<bool> DeleteCategoria(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCobertura(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEdades(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEstadistica(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteFecha(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteFecha(Fecha fecha)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteLugar(Lugar lugar)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteLugar(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Categoria> ReadCategoria(int id){
            if(id<=0){
                throw new Exception("'id' no puede ser igual o menor que '0'");
            }else{
                return await Conexion.categoriaTbl.FindAsync(id);
            }
        }

        public Task<Categoria> ReadCobertura(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Edades> ReadEdades(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Estadistica> ReadEstadistica(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Fecha> ReadFecha(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Lugar> ReadLugar(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCategoria(Categoria cat)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCobertura(Categoria cat)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateEdades(Edades edades)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateEstadistica(Estadistica estadistica)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateFecha(Fecha fecha)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateLugar(Lugar lugar)
        {
            throw new NotImplementedException();
        }
    }

}