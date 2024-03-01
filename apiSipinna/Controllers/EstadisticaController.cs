using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apiSipinna.Models;

namespace apiSipinna.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadisticaController : ControllerBase
    {
        private readonly Conexiones _context;

        public EstadisticaController(Conexiones context)
        {
            _context = context;
        }

        // GET: api/Estadistica
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estadistica>>> GetestadisticaTbl()
        {
            return await _context.estadisticaTbl
                .Include(e => e.Categoria)
                .Include(e => e.Cobertura)
                .Include(e => e.Edades)
                .Include(e => e.Lugar)
                .Include(e => e.Fecha)
                .ToListAsync();
        }
/*
        [HttpGet]
        [Route("datos")]
        public async Task<ActionResult<IEnumerable<EstadisticaConsulta>>> GetestadisticasDatos()
        {
            var consulta = from estadistica in _context.estadisticaTbl
                join categoria in _context.categoriaTbl on estadistica.categoria equals categoria.idCategoria
                join lugar in _context.lugarTbl on estadistica.lugar equals lugar.idLugar
                join edades in _context.edadesTbl on estadistica.edades equals edades.idedades
                join fecha in _context.fechaTbl on estadistica.fecha equals fecha.idfecha
                join cobertura in _context.coberturaTbl on estadistica.cobertura equals cobertura.idCobertura
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

            List<EstadisticaConsulta> resultados = await consulta.ToListAsync();

            return resultados;
        }
        */

        // GET: api/Estadistica/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estadistica>> GetEstadistica(int id)
        {
            var estadistica = await _context.estadisticaTbl.FindAsync(id);

            if (estadistica == null)
            {
                return NotFound();
            }

            return estadistica;
        }

        // PUT: api/Estadistica/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstadistica(int id, Estadistica estadistica)
        {
            if (id != estadistica.idestadistica)
            {
                return BadRequest();
            }

            _context.Entry(estadistica).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadisticaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Estadistica
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPost]
        public async Task<ActionResult<Estadistica>> PostEstadistica(Estadistica estadistica)
        {
            _context.estadisticaTbl.Add(estadistica);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstadistica", new { id = estadistica.idestadistica }, estadistica);
        }
        */

        [HttpPost]
        public async Task<ActionResult<Estadistica>> Post([FromBody] EstadisticaPost estadisticaDto)
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
                    return NotFound("No se encontro referencia a al indicador introducido");
                }

                
                var cobertura = _context.coberturaTbl.FirstOrDefault(b => b.alcance == alcance && b.poblacion == poblacion);
                if (cobertura == null)
                {
                    return NotFound("No se encontro referencia a la cobertura introducida");
                }

                var lugar = _context.lugarTbl.FirstOrDefault(b => b.entidad == entidad);
                if (lugar == null)
                {
                    return NotFound("No se encontro referencia al lugar introducido");
                }

                var edades = _context.edadesTbl.FirstOrDefault(b => b.rangoEdades == rangoEdades);
                if (edades == null)
                {
                    return NotFound("No se encontro referencia al rango de edades introducido");
                }

                var fecha = _context.fechaTbl.FirstOrDefault(b => b.anio == anio && b.mes == mes);
                if (fecha == null)
                {
                    return NotFound("No se encontro referencia a la fecha introducida");
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

                return Ok(new { mensaje = "Registro creado"});
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }        

        // DELETE: api/Estadistica/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstadistica(int id)
        {
            var estadistica = await _context.estadisticaTbl.FindAsync(id);
            if (estadistica == null)
            {
                return NotFound();
            }

            _context.estadisticaTbl.Remove(estadistica);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstadisticaExists(int id)
        {
            return _context.estadisticaTbl.Any(e => e.idestadistica == id);
        }
    }
}
