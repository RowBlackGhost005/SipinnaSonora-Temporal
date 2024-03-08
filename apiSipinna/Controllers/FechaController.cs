using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using apiSipinna.Models;
using apiSipinna.CRUD;

namespace apiSipinna.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FechaController:ControllerBase{

    private IOperations _operations;

    public FechaController(IOperations operations){
        _operations = operations;
    }

    [HttpGet("{id}")]
    public async Task<Fecha> Get(int id){
        Fecha? fecha = null;
        try{
            fecha = await _operations.ReadFecha(id);
        }catch(Exception e){
            Console.WriteLine($"Error!!!: {e.Message}");
        }
        return fecha;
    }

    [HttpPost]
    public async Task<Boolean> Post([FromBody]Fecha fecha){
        return await _operations.Create(fecha);
    }

    [HttpPut]
    public async Task<Boolean> Put([FromBody]Fecha fecha){
        return await _operations.UpdateFecha(fecha);
    }

    [HttpDelete("{id}")]
    public async Task<Boolean> Delete(int id){
        return await _operations.DeleteFecha(id);
    }

}