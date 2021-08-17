using Microsoft.AspNetCore.Mvc;
using SingleDebtControl.Domain.Service.Debit;

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

    }
}
