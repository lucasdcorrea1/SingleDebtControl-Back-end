using Microsoft.AspNetCore.Mvc;
using SingleDebtControl.Domain.Service.Payment;
using SingleDebtControl.Domain.Service.Payment.Dto;
using Utils.Message;

namespace SingleDebtControl.Api.Controllers
{
    [ApiController]
    [Route("api/payment")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private IMessageService _messageError;

        public PaymentController(IPaymentService paymentService, IMessageService messageError)
        {
            _paymentService = paymentService;
            _messageError = messageError;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var payments = _paymentService.Get();

            return Ok(new { payments });
        }

        [HttpPost]
        public IActionResult Post(PaymentDto dto)
        {
            var response = _paymentService.Post(dto);

            if (_messageError.Any())
                return BadRequest(new { messages = _messageError.GetMessageError() });

            return Ok(new { response });
        }

        [HttpPut]
        public IActionResult Put(PaymentDto dto)
        {
            var response = _paymentService.Put(dto);

            if (_messageError.Any())
                return BadRequest(new { messages = _messageError.GetMessageError() });

            return Ok();
        }

    }
}
