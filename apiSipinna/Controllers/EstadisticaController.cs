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
        if(estadisticas.Count > 0){
            foreach(Estadistica e in estadisticas){
                if(e != null){
                    if(await _operations.InsertEstadistica(e)){
                        Console.WriteLine("Dato agregado");
                    }else{
                        Console.WriteLine("No se pudo agregar el dato");
                        return false;
                    }
                }
            }
            return true;
        }else{
            return false;
        }
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