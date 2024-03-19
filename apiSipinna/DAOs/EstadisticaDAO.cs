using Microsoft.EntityFrameworkCore;
using apiSipinna.Models;

public class EstadisticaDAO{
    private readonly Conexiones _context; 

    public EstadisticaDAO(Conexiones context){
        _context = context; 
    }

        public async Task<Estadistica> agregarEstadistica(Estadistica estadistica)
        {

            var indicador = estadistica.CategoriaNav.indicador;
            var alcance = estadistica.CoberturaNav.alcance;
            var poblacion = estadistica.CoberturaNav.poblacion;
            var entidad = estadistica.LugarNav.entidad;
            var rangoEdades = estadistica.EdadesNav.rangoEdades;
            var anio = estadistica.FechaNav.anio;
            var mes = estadistica.FechaNav.mes;

            try
            {
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

               estadistica.CategoriaNav = categoria;
               estadistica.CoberturaNav = cobertura;
               estadistica.LugarNav = lugar;
               estadistica.EdadesNav = edades;
               estadistica.FechaNav = fecha;

                _context.estadisticaTbl.Add(estadistica);
                await _context.SaveChangesAsync();

                return estadistica;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<string> guardarArreglo(List<Estadistica> estadistica)
        {
            try
            {

               foreach (Estadistica element in estadistica)
               {
                
                var categoria = _context.categoriaTbl.FirstOrDefault(a => a.indicador == element.CategoriaNav.indicador);
                if (categoria == null)
                {
                    categoria = new Categoria(0,element.CategoriaNav.dominio,element.CategoriaNav.categoria,element.CategoriaNav.indicador);
                }

                
                var cobertura = _context.coberturaTbl.FirstOrDefault(b => b.alcance == element.CoberturaNav.alcance && b.poblacion == element.CoberturaNav.poblacion);
                if (cobertura == null)
                {
                    cobertura = new Cobertura(0,element.CoberturaNav.alcance,element.CoberturaNav.poblacion);
                }

                var lugar = _context.lugarTbl.FirstOrDefault(b => b.entidad == element.LugarNav.entidad);
                if (lugar == null)
                {
                    lugar = new Lugar(0,element.LugarNav.entidad);
                }

                var edades = _context.edadesTbl.FirstOrDefault(b => b.rangoEdades == element.EdadesNav.rangoEdades);
                if (edades == null)
                {
                    edades = new Edades(0,element.EdadesNav.rangoEdades);
                }

                var fecha = _context.fechaTbl.FirstOrDefault(b => b.anio == element.FechaNav.anio && b.mes == element.FechaNav.mes);
                if (fecha == null)
                {
                    fecha = new Fecha(0,element.FechaNav.anio,element.FechaNav.mes);
                }

               element.CategoriaNav = categoria;
               element.CoberturaNav = cobertura;
               element.LugarNav = lugar;
               element.EdadesNav = edades;
               element.FechaNav = fecha;
            
                // Guardar el nuevo registro en la base de datos
                _context.estadisticaTbl.Add(element);
                await _context.SaveChangesAsync();                   
               }

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
                .Include(e => e.CategoriaNav)
                .Include(e => e.CoberturaNav)
                .Include(e => e.EdadesNav)
                .Include(e => e.LugarNav)
                .Include(e => e.FechaNav)
                .ToListAsync();
            }catch(Exception ex){
                throw new InvalidOperationException(ex.Message);
            }

        }

        public async Task<List<Estadistica>> getEstadisticaIndicadorFecha(string indicador, int anio)
        {
            try{
              return await _context.estadisticaTbl
                .Include(e => e.CategoriaNav)
                .Include(e => e.CoberturaNav)
                .Include(e => e.EdadesNav)
                .Include(e => e.LugarNav)
                .Include(e => e.FechaNav)
                .Where(e => e.CategoriaNav.indicador == indicador && e.FechaNav.anio == anio)
                .ToListAsync();
            }catch(Exception ex){
                throw new InvalidOperationException(ex.Message);
            }

        }

        public async Task<IEnumerable<EstadisticaConsulta>> getEstadisticaDatos()
        {
            try{
                var resultadoConsulta = from estadistica in _context.estadisticaTbl
                    join categoria in _context.categoriaTbl on estadistica.categoria equals categoria.idCategoria
                    join cobertura in _context.coberturaTbl on estadistica.cobertura equals cobertura.idCobertura
                    join edades in _context.edadesTbl on estadistica.edades equals edades.idedades
                    join fecha in _context.fechaTbl on estadistica.fecha equals fecha.idfecha
                    join lugar in _context.lugarTbl on estadistica.lugar equals lugar.idLugar
                    select new EstadisticaConsulta
                    {
                        Dominio = categoria.dominio,
                        Categoria = categoria.categoria,
                        Indicador = categoria.indicador,
                        Poblacion = cobertura.poblacion,
                        RangoEdades = edades.rangoEdades,
                        Entidad = lugar.entidad,
                        Anio = fecha.anio,
                        Dato = estadistica.dato
                    };


                return await resultadoConsulta.ToListAsync();

            }catch(Exception ex){
                throw new InvalidOperationException(ex.Message);
            }

        }

        public async Task<string> eliminarIndicador(List<Estadistica> estadistica){
            try{

                foreach(var item in estadistica)
                {
                    _context.estadisticaTbl.Remove(item);
                }

                await _context.SaveChangesAsync();

                return "se elimino";

            }catch(Exception ex){
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<string> actualizarIndicador(List<Estadistica> estadisticas){

            var estadisticaLista = await getEstadisticaIndicadorFecha("Promedio de horas que trabajan las niñas, niños y adolescentes",2017);

            if (estadisticaLista.Count == 0)
            {
                Console.WriteLine("no hay estadisticas con ese indicador y fecha");
                return "no hay estadisticas con los parametros recibidos"; 
            }

            using var transaction = _context.Database.BeginTransaction();

            Categoria categoria = new Categoria(0,"Proteccion","Trabajo","Promedio de horas que trabajan las niñas, niños y adolescentes");
            Cobertura cobertura = new Cobertura(0,"Nacional","Total");
            Lugar lugar = new Lugar(0,"Sonora");
            Edades edades = new Edades(0,"5-7");
            Fecha fecha = new Fecha(0,2017,"Febrero");

            var cadena = await eliminarIndicador(estadisticaLista);

            await guardarArreglo(estadisticas);

            transaction.Commit();

            return "se actualizo con exito los datos";
        }

        


}