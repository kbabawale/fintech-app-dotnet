using Fintech_App.Model.Domain;
using Fintech_App.Model.DTO;
using Fintech_App.Util;
using Microsoft.AspNetCore.Mvc;

namespace Fintech_App.Controllers
{
    // [Route("[controller]")]
    [Route("/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public ActionResult<Account> CreateAccount(CreateAccountDTO payload)
        {
            _logger.LogInformation("Running POST /Account");
            var response = ApiResponse<object>.SuccessResponse("", "Created successfully");
            return CreatedAtAction(nameof(GetAccounts), new { Id = 1 }, response);
        }

        [HttpGet("{id}")]
        public ActionResult<List<Account>> GetAccounts()
        {
            _logger.LogInformation("Running /Account");
            return Ok();
        }
    }
}
