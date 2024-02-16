using Newtonsoft.Json;

namespace Domain.DTOs;

public class ExtratoDto : BaseResponse
{

    public SaldoDto Saldo {get;set;}
    public IEnumerable<TransacaoDetailedDto> UltimasTransacoes {get;set;}

}
