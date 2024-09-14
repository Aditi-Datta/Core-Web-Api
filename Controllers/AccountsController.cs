using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using crud_operation.Iservice;
using crud_operation.Models;

namespace crud_operation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(ILogger<AccountsController> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        [HttpGet]
        [Route("getAccountById/{id}")]
         public async Task<ActionResult<MessageStatus>> GetAccountById(int id)
        {
            try
            {
                var accounts = _accountService.SearchAccountNameById(id);
                if (accounts != null && accounts.Any())
                {
                    var res = new MessageStatus
                    {
                        Message = "Data retrieved successfully.",
                        Status = true,
                        Data = accounts,
                        Code = 200,
                    };

                    return Ok(res); // Return 200 OK with response object
                }
                else
                {
                    var messageStatus = new MessageStatus
                    {
                        Data = null,
                        Status = false,
                        Code = 404,
                        Message = "No account found with the provided ID"
                    };

                    return NotFound(messageStatus); // Return 404 Not Found with message status object
                }
            }
            catch (Exception ex)
            {
                var errorStatus = new MessageStatus
                {
                    Data = null,
                    Status = false,
                    Code = 500,
                    Message = ex.Message
                };

                _logger.LogError(ex, "An error occurred while retrieving account information.");
                return StatusCode((int)HttpStatusCode.InternalServerError, errorStatus); // Return 500 Internal Server Error with message status object
            }
        }
    }
}
