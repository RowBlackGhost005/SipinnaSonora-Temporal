using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using apiSipinna.Models;
using apiSipinna.CRUD;

namespace apiSipinna.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EdadesController:ControllerBase{

    private IOperations _operations;

    public EdadesController(IOperations operations){
        _operations = operations;
    }

    [HttpGet("{id}")]
    public async Task<Edades> Get(int id){
        Edades? edades = null;
        try{
            edades = await _operations.ReadEdades(id);
        }catch(Exception e){
            Console.WriteLine($"Error!!!: {e.Message}");
        }
        return edades;
    }

    [HttpPost]
    public async Task<Boolean> Post([FromBody]Edades edades){
        return await _operations.Create(edades);
    }

    [HttpPut]
    public async Task<Boolean> Put([FromBody]Edades edades){
        return await _operations.UpdateEdades(edades);
    }

    [HttpDelete("{id}")]
    public async Task<Boolean> Delete(int id){
        return await _operations.DeleteEdades(id);
    }

}