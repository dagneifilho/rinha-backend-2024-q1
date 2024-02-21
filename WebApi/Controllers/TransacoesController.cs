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
    public async Task<IActionResult> NovaTransacao([FromRoute] int id, [FromBody] Transacao transacao)
    {
        transacao.ClienteId = id;
        return StatusCode(200, await _service.NovaTransacao(transacao));
        
    }

    [HttpGet("/clientes/{id}/extrato")]
    public async Task<IActionResult> GetExtrato([FromRoute]int id)
    {
        return StatusCode(200, await _service.Extrato(id));
    }


    private List<string> GetModelStateErrors(ModelStateDictionary modelState)
    {
        return modelState.Values.SelectMany(m => m.Errors).Select(x => x.ErrorMessage).ToList();
    }
}
