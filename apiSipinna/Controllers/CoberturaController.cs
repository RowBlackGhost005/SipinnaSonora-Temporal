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
        Cobertura? cob = null;
        try{
            cob = await _operations.ReadCobertura(id);
        }catch(Exception e){
            Console.WriteLine($"Error!!!: {e.Message}");
        }
        return cob;
    }

    [HttpPost]
    public async Task<Boolean> Post([FromBody]Cobertura cob){
        return await _operations.Create(cob);
    }

    [HttpPut]
    public async Task<Boolean> Put([FromBody]Cobertura cob){
        return await _operations.UpdateCobertura(cob);
    }

    [HttpDelete("{id}")]
    public async Task<Boolean> Delete(int id){
        return await _operations.DeleteCobertura(id);
    }

}