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
                    throw new Exception($"La categoría con el id {id} no existe en la base de datos.");
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
                    throw new Exception($"La cobertura con el id {id} no existe en la base de datos.");
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
                    throw new Exception($"El rango de edades con el id {id} no existe en la base de datos");
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

        public async Task<bool> DeleteFecha(int id)
        {
            Fecha fecha;
            try{
                fecha = await ReadFecha(id);
                if(fecha != null){
                    Conexion.fechaTbl.Attach(fecha);
                    Conexion.fechaTbl.Remove(fecha);
                    await Conexion.SaveChangesAsync();
                    return true;
                }else{
                    throw new Exception($"La fecha con el id {id} no existe en la base de datos.");
                }
            }catch(Exception e){
                logger.MostrarExceptionInfo(e);
                return false;
            }
        }

        public async Task<bool> DeleteFecha(Fecha fecha)
        {
            List<Fecha> fechas;
            try{
                fechas = await Conexion.fechaTbl
                                .Include(e=>e.anio)
                                .Include(e=>e.mes)
                                .ToListAsync<Fecha>();
                if(fechas.Count == 0){
                    throw new Exception($"La fecha {fecha.mes}/{fecha.anio} ingresada no existe en la base de datos.");
                }else{
                    Conexion.fechaTbl.Attach(fechas.FirstOrDefault());
                    Conexion.fechaTbl.Remove(fechas.FirstOrDefault());
                    await Conexion.SaveChangesAsync();
                    return true;
                }
            }catch(Exception e){
                logger.MostrarExceptionInfo(e);
                return false;
            }
        }

        public async Task<bool> DeleteLugar(Lugar lugar)
        {
            List<Lugar> lugares;
            try{
                lugares = await Conexion.lugarTbl.Include(e=>e.entidad).ToListAsync<Lugar>();
                if(lugares.Count == 0){
                    throw new Exception($"El lugar {lugar.entidad} no existe en la base de datos.");
                }else{
                    Conexion.lugarTbl.Attach(lugares.FirstOrDefault());
                    Conexion.lugarTbl.Remove(lugares.FirstOrDefault());
                    await Conexion.SaveChangesAsync();
                    return true;
                }
            }catch(Exception e){
                logger.MostrarExceptionInfo(e);
                return false;
            }
        }

        public async Task<bool> DeleteLugar(int id)
        {
            Lugar lugar;
            try{
                lugar = await Conexion.lugarTbl.FindAsync(id);
                if(lugar != null){
                    Conexion.lugarTbl.Attach(lugar);
                    Conexion.lugarTbl.Remove(lugar);
                    await Conexion.SaveChangesAsync();
                    return true;
                }else{
                    throw new Exception($"El lugar con el id {id} no existe en la base de datos.");
                }
            }catch(Exception e){
                logger.MostrarExceptionInfo(e);
                return false;
            }
        }

        public async Task<Categoria> ReadCategoria(int id){
            if(id <= 0){
                throw new Exception("'id' no puede ser igual o menor que '0'");
            }else{
                Categoria cat;
                try{
                    cat = await Conexion.categoriaTbl.FindAsync(id);
                    if(cat != null){
                        return cat;
                    }else{
                        throw new Exception($"La categoria con el id {id} no existe en la base de datos.");
                    }
                }catch(Exception e){
                    logger.MostrarExceptionInfo(e);
                    return null;
                }
            }
        }

        public async Task<Cobertura> ReadCobertura(int id)
        {
            if(id <= 0){
                throw new Exception("'id' no puede ser igual o menor que '0'");
            }else{
                Cobertura cobertura;
                try{
                    cobertura = await Conexion.coberturaTbl.FindAsync(id);
                    if(cobertura != null){
                        return cobertura;
                    }else{
                        throw new Exception($"La cobertura con el id {id} no existe en la base de datos.");
                    }
                }catch(Exception e){
                    logger.MostrarExceptionInfo(e);
                    return null;
                }
            }
        }

        public async Task<Edades> ReadEdades(int id)
        {
            if(id <= 0){
                throw new Exception("'id' no puede ser igual o menor que '0'");
            }else{
                Edades edades;
                try{
                    edades = await Conexion.edadesTbl.FindAsync(id);
                    if(edades != null){
                        return edades;
                    }else{
                        throw new Exception($"El rango de edades con el id {id} no existe en la base de datos.");
                    }
                }catch(Exception e){
                    logger.MostrarExceptionInfo(e);
                    return null;
                }
            }
        }

        public Task<Estadistica> ReadEstadistica(int id)
        {
            throw new Exception("Not implemented yet");
        }

        public async Task<Fecha> ReadFecha(int id)
        {
            if(id <= 0){
                throw new Exception("'id' no puede ser igual o menor que '0'");
            }else{
                Fecha fecha;
                try{
                    fecha = await Conexion.fechaTbl.FindAsync(id);
                    if(fecha != null){
                        return fecha;
                    }else{
                        throw new Exception($"La fecha con el id {id} no existe en la base de datos.");
                    }
                }catch(Exception e){
                    logger.MostrarExceptionInfo(e);
                    return null;
                }
            }
        }

        public async Task<Fecha> ReadFecha(Fecha fecha)
        {
            if(fecha.idfecha <= 0){
                throw new Exception("'id' no puede ser igual o menor que '0'");
            }else{
                List<Fecha> fechas;
                try{
                    fechas = await Conexion.fechaTbl
                                        .Include(x => x.mes)
                                        .Include(x => x.anio)
                                        .ToListAsync<Fecha>();
                    if(fechas.Count() == 0){
                        throw new Exception($"La fecha {fecha.mes}/{fecha.anio} no está en la base de datos");
                    }else{
                        return fechas.FirstOrDefault<Fecha>();
                    }
                }catch(Exception e){
                    logger.MostrarExceptionInfo(e);
                    return null;
                }
            }
        }

        public async Task<Lugar> ReadLugar(int id)
        {
            if(id <= 0){
                throw new Exception("'id' no puede ser igual o menor que '0'");
            }else{
                return await Conexion.lugarTbl.FindAsync(id);
            }
        }

        public async Task<Lugar> ReadLugar(Lugar lugar)
        {
            List<Lugar> lugares;
            try{
                lugares = await Conexion.lugarTbl.Include(e=>e.entidad).ToListAsync<Lugar>();
                if(lugares.Count == 0){
                    throw new Exception($"El lugar {lugar.entidad} no existe en la base de datos.");
                }else{
                    return lugares.FirstOrDefault<Lugar>();
                }
            }catch(Exception e){
                logger.MostrarExceptionInfo(e);
                return null;
            }
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

        public async Task<bool> UpdateFecha(Fecha fecha)
        {
            Fecha row;
            try{
                row = await ReadFecha(fecha.idfecha);
                if(row != null){
                    row.mes = "" != fecha.mes ? fecha.mes : row.mes;
                    row.anio = 0 != fecha.anio ? fecha.anio : row.anio;
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

        public async Task<bool> UpdateLugar(Lugar lugar)
        {
            Lugar row;
            try{
                row = await ReadLugar(lugar.idLugar);
                if(row != null){
                    row.entidad = "" != lugar.entidad ? lugar.entidad : row.entidad;
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
    }

}