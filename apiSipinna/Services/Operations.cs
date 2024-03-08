using apiSipinna.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Connections;
using apiSipinna.Utils;
using Microsoft.AspNetCore.Http.Timeouts;
using K4os.Hash.xxHash;

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

        public async Task<bool> DeleteCategoria(int id)
        {
            Categoria cat;
            try{
                cat = await ReadCategoria(id);
                if(cat != null){
                    Conexion.categoriaTbl.Attach(cat);
                    Conexion.categoriaTbl.Remove(cat);
                    await Conexion.SaveChangesAsync();
                    return true;
                }else{
                    return false;
                }
            }catch(Exception e){
                logger.MostrarExceptionInfo(e);
                return false;
            }
        }

        public async Task<bool> DeleteCobertura(int id)
        {
            Cobertura cob;
            try{
                cob = await ReadCobertura(id);
                if(cob != null){
                    Conexion.coberturaTbl.Attach(cob);
                    Conexion.coberturaTbl.Remove(cob);
                    await Conexion.SaveChangesAsync();
                    return true;
                }else{
                    return false;
                }
            }catch(Exception e){
                logger.MostrarExceptionInfo(e);
                return false;
            }
        }

        public async Task<bool> DeleteEdades(int id)
        {
            Edades edades;
            try{
                edades = await ReadEdades(id);
                if(edades != null){
                    Conexion.edadesTbl.Attach(edades);
                    Conexion.edadesTbl.Remove(edades);
                    await Conexion.SaveChangesAsync();
                    return true;
                }else{
                    return false;
                }
            }catch(Exception e){
                logger.MostrarExceptionInfo(e);
                return false;
            }
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

        public async Task<Cobertura> ReadCobertura(int id)
        {
            if(id <= 0){
                throw new Exception("'id' no puede ser igual o menor que '0'");
            }else{
                return await Conexion.coberturaTbl.FindAsync(id);
            }
        }

        public async Task<Edades> ReadEdades(int id)
        {
            if(id <= 0){
                throw new Exception("'id' no puede ser igual o menor que '0'");
            }else{
                return await Conexion.edadesTbl.FindAsync(id);
            }
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

        public async Task<bool> UpdateCategoria(Categoria cat){
            Categoria row;
            try{
                row = await ReadCategoria(cat.idCategoria);
                if(row != null){
                    row.categoria = "" != cat.categoria ? cat.categoria : row.categoria;
                    row.dominio = "" != cat.dominio ? cat.dominio : row.dominio;
                    row.indicador = "" != cat.indicador ? cat.indicador : row.indicador;
                    await Conexion.SaveChangesAsync();
                    return true;
                }else{
                    return false;
                }
            }catch(Exception e){
                logger.MostrarExceptionInfo(e);
                return false;
            }
        }

        public async Task<bool> UpdateCobertura(Cobertura cob)
        {
            Cobertura row;
            try{
                row = await ReadCobertura(cob.idCobertura);
                if(row != null){
                    row.poblacion = "" != cob.poblacion ? cob.poblacion : row.poblacion;
                    row.alcance = "" != cob.alcance ? cob.alcance : row.alcance;
                    await Conexion.SaveChangesAsync();
                    return true;
                }else{
                    return false;
                }
            }catch(Exception e){
                logger.MostrarExceptionInfo(e);
                return false;
            }
        }

        public async Task<bool> UpdateEdades(Edades edades)
        {
            Edades row;
            try{
                row = await ReadEdades(edades.idedades);
                if(row != null){
                    row.rangoEdades = "" != edades.rangoEdades ? edades.rangoEdades : row.rangoEdades;
                    await Conexion.SaveChangesAsync();
                    return true;
                }else{
                    return false;
                }
            }catch(Exception e){
                logger.MostrarExceptionInfo(e);
                return false;
            }
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