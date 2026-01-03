using Fintech_App.Model.Domain;
using Microsoft.AspNetCore.Http;
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

        // [HttpPost]
        // public ActionResult<Account> CreateAccount()
        // {

        // }

        [HttpGet("{id}")]
        public ActionResult<List<Account>> GetAccounts()
        {
            _logger.LogInformation("Running /Account");
            return Ok();
        }
    }
}
