using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using apiSipinna.Models;
using apiSipinna.CRUD;

namespace apiSipinna.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriaController:ControllerBase{

    private IOperations _operations;

    public CategoriaController(IOperations operations){
        _operations = operations;
    }

    [HttpGet("{id}")]
    public async Task<Categoria> Get(int id){
        Categoria? cat = null;
        try{
            cat = await _operations.ReadCategoria(id);
        }catch(Exception e){
            Console.WriteLine($"Error!!!: {e.Message}");
        }
        return cat;
    }

    [HttpPost]
    public async Task<Boolean> Post([FromBody]Categoria cat){
        return await _operations.Create(cat);
    }

}