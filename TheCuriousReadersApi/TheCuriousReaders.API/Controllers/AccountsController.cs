using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheCuriousReaders.Services.Interfaces;

namespace TheCuriousReaders.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountsController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("total")]
        public async Task<IActionResult> GetTotalCountOfAccounts()
        {
            return Ok(await _userService.GetTotalAccountsCountAsync());
        }
    }
}
