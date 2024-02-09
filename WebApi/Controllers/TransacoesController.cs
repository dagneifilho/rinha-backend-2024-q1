using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApi.Controllers;

[ApiController]
public class TransacoesController : ControllerBase
{
    private readonly ITransacoesService _service;
    public TransacoesController(ITransacoesService service)
    {
        _service = service;
    }
    [HttpPost("clientes/{id}/transacoes")]
    public async Task<IActionResult> NovaTransacao([FromRoute] int id, [FromBody] NovaTransacao transacao)
    {
        transacao.ClienteId = id;
        if(!ModelState.IsValid)
            return StatusCode(400,GetModelStateErrors(ModelState));
        return StatusCode(200, await _service.NovaTransacao(transacao));
        
    }

    [HttpGet("/clientes/{id}/extrato")]
    public async Task<IActionResult> GetExtrato([FromRoute]int id)
    {
        return StatusCode(200, await _service.Extrato(id));
    }


    private List<ModelError> GetModelStateErrors(ModelStateDictionary modelState)
    {
        return modelState.Values.SelectMany(m => m.Errors).ToList();
    }
}
