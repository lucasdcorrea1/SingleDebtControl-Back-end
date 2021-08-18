using Microsoft.AspNetCore.Mvc;
using SingleDebtControl.Domain.Service.Debit;
using SingleDebtControl.Domain.Service.Debit.Dto;

namespace SingleDebtControl.Api.Controllers
{
    [ApiController]
    [Route("api/debit")]
    public class DebitController : Controller
    {
        private readonly IDebitService _debitService;

        public DebitController(IDebitService debitService)
        {
            _debitService = debitService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var debits = _debitService.Get();

            return Ok(new { debits });
        }

        [HttpPost]
        public IActionResult Post(DebitDto dto)
        {
            var response = _debitService.Post(dto);

            if (response <= 0)
                return BadRequest("Erro ao registrar debito!");

            return Ok(new { response });
        }

        [HttpPut]
        public IActionResult Put(DebitDto dto)
        {
            var response = _debitService.Put(dto);

            if (!response)
                return BadRequest("Erro ao atualizar debito!");

            return Ok();
        }

    }
}
