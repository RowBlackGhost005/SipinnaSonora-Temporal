using Microsoft.EntityFrameworkCore;
using apiSipinna.Models;

public class EstadisticaDAO{
    private readonly Conexiones _context; 

    public EstadisticaDAO(Conexiones context){
        _context = context; 
    }

        public async Task<Estadistica> agregarEstadistica(Estadistica estadisticaDto)
        {
            try
            {

               var indicador = estadisticaDto.Indicador;
               var alcance = estadisticaDto.Alcance;
               var poblacion = estadisticaDto.Poblacion;
               var entidad = estadisticaDto.Entidad;
               var rangoEdades = estadisticaDto.RangoEdades;
               var anio = estadisticaDto.Anio ?? 0;
               var mes = estadisticaDto.Mes;
               float datoEst = estadisticaDto.DatoEst ?? 0.0f;
            
                 
                var categoria = _context.categoriaTbl.FirstOrDefault(a => a.indicador == indicador);
                if (categoria == null)
                {
                    throw new InvalidOperationException("No se encontro");
                }

                
                var cobertura = _context.coberturaTbl.FirstOrDefault(b => b.alcance == alcance && b.poblacion == poblacion);
                if (cobertura == null)
                {
                    throw new InvalidOperationException("No se encontro");
                }

                var lugar = _context.lugarTbl.FirstOrDefault(b => b.entidad == entidad);
                if (lugar == null)
                {
                    throw new InvalidOperationException("No se encontro");
                }

                var edades = _context.edadesTbl.FirstOrDefault(b => b.rangoEdades == rangoEdades);
                if (edades == null)
                {
                    throw new InvalidOperationException("No se encontro");
                }

                var fecha = _context.fechaTbl.FirstOrDefault(b => b.anio == anio && b.mes == mes);
                if (fecha == null)
                {
                    throw new InvalidOperationException("No se encontro");
                }


                var datoEstadistico = new Estadistica
                {
                    Categoria = categoria,
                    Cobertura = cobertura,
                    Lugar = lugar,
                    Edades = edades,
                    Fecha = fecha,
                    dato = datoEst
                };
                
                Console.WriteLine("texto es: "+datoEstadistico.dato);

                // Guardar el nuevo registro en la base de datos
                _context.estadisticaTbl.Add(datoEstadistico);
                await _context.SaveChangesAsync();

                return datoEstadistico;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<string> guardarArreglo(EstadisticaPost[] estadisticaDto)
        {
            try
            {

               using var transaction = _context.Database.BeginTransaction(); 

               foreach (EstadisticaPost element in estadisticaDto)
               {
    
               var indicador = element.Indicador;
               var alcance = element.Alcance;
               var poblacion = element.Poblacion;
               var entidad = element.Entidad;
               var rangoEdades = element.RangoEdades;
               var anio = element.Anio ?? 0;
               var mes = element.Mes;
               float datoEst = element.DatoEst ?? 0.0f;
            
                 
                var categoria = _context.categoriaTbl.FirstOrDefault(a => a.indicador == indicador);
                if (categoria == null)
                {
                    throw new InvalidOperationException("No se encontro");
                }

                
                var cobertura = _context.coberturaTbl.FirstOrDefault(b => b.alcance == alcance && b.poblacion == poblacion);
                if (cobertura == null)
                {
                    throw new InvalidOperationException("No se encontro");
                }

                var lugar = _context.lugarTbl.FirstOrDefault(b => b.entidad == entidad);
                if (lugar == null)
                {
                    throw new InvalidOperationException("No se encontro");
                }

                var edades = _context.edadesTbl.FirstOrDefault(b => b.rangoEdades == rangoEdades);
                if (edades == null)
                {
                    throw new InvalidOperationException("No se encontro");
                }

                var fecha = _context.fechaTbl.FirstOrDefault(b => b.anio == anio && b.mes == mes);
                if (fecha == null)
                {
                    throw new InvalidOperationException("No se encontro");
                }


                var datoEstadistico = new Estadistica
                {
                    Categoria = categoria,
                    Cobertura = cobertura,
                    Lugar = lugar,
                    Edades = edades,
                    Fecha = fecha,
                    dato = datoEst
                };
                
                Console.WriteLine("texto es: "+datoEstadistico.dato);

                // Guardar el nuevo registro en la base de datos
                _context.estadisticaTbl.Add(datoEstadistico);
                await _context.SaveChangesAsync();                   
               }

                transaction.Commit();

                return "transaccion completada";
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<IEnumerable<Estadistica>> getEstadistica()
        {
            try{
              return await _context.estadisticaTbl
                .Include(e => e.Categoria)
                .Include(e => e.Cobertura)
                .Include(e => e.Edades)
                .Include(e => e.Lugar)
                .Include(e => e.Fecha)
                .ToListAsync();
            }catch(Exception ex){
                throw new InvalidOperationException(ex.Message);
            }

        }

        


}