using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using apiSipinna.Models;
using apiSipinna.CRUD;

namespace apiSipinna.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoberturaController:ControllerBase{

    private IOperations _operations;

    public CoberturaController(IOperations operations){
        _operations = operations;
    }

    [HttpGet("{id}")]
    public async Task<Cobertura> Get(int id){
        Cobertura? cat = null;
        try{
            cat = await _operations.ReadCobertura(id);
        }catch(Exception e){
            Console.WriteLine($"Error!!!: {e.Message}");
        }
        return cat;
    }

    [HttpPost]
    public async Task<Boolean> Post([FromBody]Cobertura cat){
        return await _operations.Create(cat);
    }

    [HttpPut]
    public async Task<Boolean> Put([FromBody]Cobertura cat){
        return await _operations.UpdateCobertura(cat);
    }

    [HttpDelete("{id}")]
    public async Task<Boolean> Delete(int id){
        return await _operations.DeleteCobertura(id);
    }

}