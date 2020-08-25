using System.Security.Cryptography.X509Certificates;
using System.Net.Cache;
using Microsoft.AspNetCore.Mvc;
using test_dotnet_webapi.Data;
using test_dotnet_webapi.Dtos.Character;
using test_dotnet_webapi.Models;
using System.Threading.Tasks;

namespace test_dotnet_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        public AuthController(IAuthRepository authRepo) {
            _authRepo = authRepo;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDto request)
        {
            ServiceResponse<int> response = await _authRepo.Register(
                new User { Username = request.Username }, request.Password
            );
            if (!response.Success) {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto request) {
            ServiceResponse<string> response = await _authRepo.Login(request.Username, request.Password);
            if (!response.Success) {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}