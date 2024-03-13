using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using apiSipinna.Models;
using apiSipinna.CRUD;
using Newtonsoft.Json;

namespace apiSipinna.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstadisticaController:ControllerBase{

    private IOperations _operations;

    public EstadisticaController(IOperations operations){
        _operations = operations;
    }


    [HttpPost]
    public async Task<Boolean> Post([FromBody]List<Estadistica> estadisticas){
        Estadistica aux;
        foreach(Estadistica e in estadisticas){
            if(e!=null){
                try{
                    aux = new Estadistica{
                        Categoria = await _operations.ReadCategoria(e.Categoria.idCategoria),
                        Cobertura = await _operations.ReadCobertura(e.Cobertura.idCobertura),
                        Edades = await _operations.ReadEdades(e.Edades.idedades),
                        Fecha = await _operations.ReadFecha(e.Fecha.idfecha),
                        Lugar = await _operations.ReadLugar(e.Lugar.idLugar),
                        dato = e.dato
                    };
                    if(!await _operations.Create(aux)){
                        throw new Exception($"No se pudo añadir el elemento {e.ToString()}");
                    }
                }catch(Exception ex){
                    Console.WriteLine($"Error: {ex.Message}");
                    return false;
                }
            }
        }
        return true;
        //return await _operations.Create(Estadistica);
    }

    /**

    [HttpGet("{id}")]
    public async Task<Estadistica> Get(int id){
        Estadistica? Estadistica = null;
        try{
            Estadistica = await _operations.ReadEstadistica(id);
        }catch(Exception e){
            Console.WriteLine($"Error!!!: {e.Message}");
        }
        return Estadistica;
    }

    [HttpGet]
    public async Task<Estadistica> Get([FromBody] Estadistica Estadistica){
        return await _operations.ReadEstadistica(Estadistica);
    }


    [HttpPut]
    public async Task<Boolean> Put([FromBody]Estadistica Estadistica){
        return await _operations.UpdateEstadistica(Estadistica);
    }

    [HttpDelete("{id}")]
    public async Task<Boolean> Delete(int id){
        return await _operations.DeleteEstadistica(id);
    }

    [HttpDelete]
    public async Task<Boolean> Delete([FromBody] Estadistica Estadistica){
        return await _operations.DeleteEstadistica(Estadistica);
    }
    */

}