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
    public class RegistrationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public RegistrationController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RequestRegisterModel requestRegisterModel)
        {
            if (await _userService.CreateUserAsync(_mapper.Map<UserModel>(requestRegisterModel)))
            {
                return StatusCode(201);
            }

            return BadRequest("Something occured during registration. Please try again.");
        }
    }
}
