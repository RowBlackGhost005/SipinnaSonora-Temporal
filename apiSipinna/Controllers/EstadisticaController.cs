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
        private readonly EstadisticaDAO estadisticaDAO;
        public EstadisticaController(Conexiones context)
        {
            _context = context;
            estadisticaDAO = new EstadisticaDAO(_context);
        }

        // GET: api/Estadistica
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estadistica>>> GetestadisticaTbl()
        {
            try{

              var listaEstadistica = await estadisticaDAO.getEstadistica();

              return Ok(listaEstadistica);

            }catch(Exception ex){
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }

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
                Estadistica estadistica = await estadisticaDAO.agregarEstadistica(estadisticaDto);

                return Ok(estadistica);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }        

        [HttpPost]
        [Route("transaccion")]
        public async Task<ActionResult<Estadistica>> Post([FromBody] EstadisticaPost[] estadisticaDto)
        {
            try
            {
                await estadisticaDAO.guardarArreglo(estadisticaDto);

                return Ok("Datos guardados con exito");
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
