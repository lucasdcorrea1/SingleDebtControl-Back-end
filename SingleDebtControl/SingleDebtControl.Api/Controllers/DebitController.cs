using Microsoft.AspNetCore.Mvc;
using SingleDebtControl.Domain.Service.Debit;
using SingleDebtControl.Domain.Service.Debit.Dto;
using Utils.Message;

namespace SingleDebtControl.Api.Controllers
{
    [ApiController]
    [Route("api/debit")]
    public class DebitController : Controller
    {
        private readonly IDebitService _debitService;
        private readonly IMessageService _messageError;

        public DebitController(IDebitService debitService, IMessageService messageError)
        {
            _debitService = debitService;
            _messageError = messageError;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var debits = _debitService.Get();

            return Ok(new { debits });
        }

        [HttpGet("ative")]
        public IActionResult GetAtives()
        {
            var debits = _debitService.Get(true);

            return Ok(new { debits });
        }

        [HttpPost]
        public IActionResult Post(DebitDto dto)
        {
            var response = _debitService.Post(dto);

            if (_messageError.Any())
                return BadRequest(new { messages = _messageError.GetMessageError() });

            return Ok(new { response });
        }

        [HttpPut]
        public IActionResult Put(DebitDto dto)
        {
            var response = _debitService.Put(dto);

            if (_messageError.Any())
                return BadRequest(new { messages = _messageError.GetMessageError() });

            return Ok(response);
        }

    }
}
