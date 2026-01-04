using Fintech_App.Model.Domain;
using Fintech_App.Model.DTO;
using Fintech_App.Model.Response;
using Fintech_App.Model.ServiceInterfaces;
using Fintech_App.Util;
using Microsoft.AspNetCore.Mvc;

namespace Fintech_App.Controllers
{
    [Route("/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccount _accountService;

        public AccountController(IAccount accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<ActionResult<Account>> CreateAccount(CreateAccountDTO payload)
        {
            //run business layer
            var created = await _accountService.CreateAccount(payload);

            var response = ApiResponse<object>.SuccessResponse(created, "Created successfully");
            return Created($"", response);
        }

        [HttpGet("{Email}")]
        public async Task<ActionResult<List<GetAccountResponse>>> GetAccounts([FromRoute] GetAccountsDTO payload)
        {
            var accounts = await _accountService.GetAccounts(payload.Email);

            var response = ApiResponse<object>.SuccessResponse(accounts, "Accounts retrieved");
            return Ok(response);
        }

        [HttpPost("/transfer")]
        public async Task<ActionResult<Account>> MakeTransfer(MakeTransferDTO payload)
        {
            await _accountService.MakeTransfer(payload);
            var response = ApiResponse<object>.SuccessResponse("", "Transfer Successful");
            return Ok(response);
        }

        [HttpPut("/pin/change")]
        public async Task<ActionResult<Account>> ChangePin(ChangePinDTO payload)
        {
            await _accountService.ChangePin(payload);
            var response = ApiResponse<object>.SuccessResponse("", "Pin Changed Successfully");
            return Ok(response);
        }
    }
}
