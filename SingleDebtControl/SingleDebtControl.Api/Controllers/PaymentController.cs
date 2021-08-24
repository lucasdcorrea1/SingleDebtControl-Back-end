using Microsoft.AspNetCore.Mvc;
using SingleDebtControl.Domain.Service.Payment;
using SingleDebtControl.Domain.Service.Payment.Dto;

namespace SingleDebtControl.Api.Controllers
{
    [ApiController]
    [Route("api/payment")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
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

            if (response <= 0)
                return BadRequest("Erro ao registrar pagamneto!");

            return Ok(new { response });
        }

        [HttpPut]
        public IActionResult Put(PaymentDto dto)
        {
            var response = _paymentService.Put(dto);

            if (!response)
                return BadRequest("Erro ao atualizar pagamento!");

            return Ok();
        }

    }
}
