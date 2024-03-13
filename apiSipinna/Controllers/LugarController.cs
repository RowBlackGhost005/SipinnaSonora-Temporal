using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using apiSipinna.Models;
using apiSipinna.CRUD;

namespace apiSipinna.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LugarController:ControllerBase{

    private IOperations _operations;

    public LugarController(IOperations operations){
        _operations = operations;
    }

    [HttpGet("{id}")]
    public async Task<Lugar> Get(int id){
        Lugar? Lugar = null;
        try{
            Lugar = await _operations.ReadLugar(id);
        }catch(Exception e){
            Console.WriteLine($"Error!!!: {e.Message}");
        }
        return Lugar;
    }

    [HttpGet]
    public async Task<Lugar> Get([FromBody] Lugar lugar){
        return await _operations.ReadLugar(lugar);
    }

    [HttpPost]
    public async Task<Boolean> Post([FromBody]Lugar Lugar){
        return await _operations.Create(Lugar);
    }

    [HttpPut]
    public async Task<Boolean> Put([FromBody]Lugar Lugar){
        return await _operations.UpdateLugar(Lugar);
    }

    [HttpDelete("{id}")]
    public async Task<Boolean> Delete(int id){
        return await _operations.DeleteLugar(id);
    }

    [HttpDelete]
    public async Task<Boolean> Delete([FromBody] Lugar Lugar){
        return await _operations.DeleteLugar(Lugar);
    }

}