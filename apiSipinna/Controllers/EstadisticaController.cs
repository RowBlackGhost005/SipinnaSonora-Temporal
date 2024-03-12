using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apiSipinna.Models;
using apiSipinna.Modules.XlsParser;


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

        [HttpGet]
        [Route("datos")]
        public async Task<ActionResult<IEnumerable<EstadisticaConsulta>>> GetestadisticasDatos()
        {
            var resultados = await estadisticaDAO.getEstadisticaDatos();
            return Ok(resultados);
        }
        

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

        /*
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
        */

        // POST: api/Estadistica
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*
        [HttpPost]
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
                //dummy de estadistica
                Categoria categoria = new Categoria(0,"Proteccion","Trabajo","Promedio de horas que trabajan las niñas, niños y adolescentes");
                Cobertura cobertura = new Cobertura(0,"Nacional","Total");
                Lugar lugar = new Lugar(0,"Sonora");
                Edades edades = new Edades(0,"5-7");
                Fecha fecha = new Fecha(0,2017,"Febrero");

                Estadistica estadistica = new Estadistica(0,cobertura,categoria,edades,lugar,fecha,4350.0f);

                await estadisticaDAO.agregarEstadistica(estadistica);

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
                //dummy arreglo de estadisticas
                
                Categoria categoria = new Categoria(0,"Proteccion","Trabajo","Promedio de horas que trabajan las niñas, niños y adolescentes");
                Cobertura cobertura = new Cobertura(0,"Nacional","Total");
                Lugar lugar = new Lugar(0,"Sonora");
                Edades edades = new Edades(0,"5-7");
                Fecha fecha = new Fecha(0,2017,"Febrero");

                Estadistica estadistica = new Estadistica(0,cobertura,categoria,edades,lugar,fecha,4350.0f);
                Estadistica estadistica2 = new Estadistica(0,cobertura,categoria,edades,lugar,fecha,4350.0f);
                Estadistica estadistica3 = new Estadistica(0,cobertura,categoria,edades,lugar,fecha,4350.0f);

                var estadistics = new List<Estadistica>(){
                    estadistica,
                    estadistica2,
                    estadistica3
                };
                
 
                await estadisticaDAO.guardarArreglo(estadistics);

                return Ok("Datos guardados con exito");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /*
        [HttpPost]
        [Route("transaccionExcel")]
        public async Task<ActionResult<Estadistica>> Post()
        {
            try
            {
               
                String filepath = "Assests/Test Formato.xlsx";
                FileStream stream = File.Open(filepath, FileMode.Open, FileAccess.Read);
                XlsParser xlsParser = new XlsParser(stream);

                var datalist = xlsParser.GetStatisticData();
                var estadistics = new List<Estadistica>(){
   
                };  

                foreach(var item in xlsParser.GetStatisticData())
                {

                    Console.WriteLine(item);


                    
                    Categoria categoria = new Categoria(0,"Proteccion","Trabajo","Promedio de horas que trabajan las niñas, niños y adolescentes");
                    Cobertura cobertura = new Cobertura(0,"Nacional",item.poblacion);
                    Lugar lugar = new Lugar(0,item.entidad);
                    Edades edades = new Edades(0,item.edades);
                    Fecha fecha = new Fecha(0,2017,"Febrero");

                    Estadistica estadistica = new Estadistica(0,cobertura,categoria,edades,lugar,fecha,item.dato);

                    estadistics.Add(estadistica); 
                    
                }

 
                await estadisticaDAO.guardarArreglo(estadistics);

                return Ok("Datos guardados con exito");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
        */
    
        /*
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
        */

        private bool EstadisticaExists(int id)
        {
            return _context.estadisticaTbl.Any(e => e.idestadistica == id);
        }

        // GET: api/Estadistica
        [HttpPost]
        [Route("xls")]
        public async Task<IActionResult> SaveXlsEstadisticas([FromForm] IFormFile xls)
        {
            try{

                if (xls.Length <= 0 || xls.ContentType is null) return BadRequest();
                var actualFileName = xls.FileName;

                using (var stream = xls.OpenReadStream())
                {
                    if(stream != null){

                        Console.WriteLine("Parsing File");
                        XlsParser xlsParser = new XlsParser(stream);

                        foreach(var item in xlsParser.GetStatisticData())
                        {
                            //TODO: CALL DAO
                            Console.WriteLine(item);
                        }

                    }else{
                        return BadRequest();
                    }  
                }
    
                return Ok(); 

            }catch(Exception ex){
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }

        }

    }
}
