using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TheCuriousReaders.Models.RequestModels;
using TheCuriousReaders.Models.ServiceModels;
using TheCuriousReaders.Services.Interfaces;

namespace TheCuriousReaders.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public LoginController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Login(RequestLoginModel request)
        {
            var response = await _userService.LoginAsync(_mapper.Map<UserLoginModel>(request));

            if (response.Token == null)
            {
                return StatusCode(401, "Unauthenticated");
            }

            return Ok(response);
        }
    }
}
