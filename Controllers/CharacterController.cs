using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using test_dotnet_webapi.Dtos.Character;
using test_dotnet_webapi.Models;
using test_dotnet_webapi.Services.CharacterService;

namespace test_dotnet_webapi.Controllers {
    [Authorize]
    [ApiController]
    [Route ("[controller]")]
    public class CharacterController : ControllerBase {
        private readonly ICharacterService _characterService;
        public CharacterController (ICharacterService characterService) {
            _characterService = characterService;
        }
        
        [AllowAnonymous]
        [HttpGet ("GetAll")]
        public async Task<IActionResult> Get () {
            return Ok (await _characterService.GetAllCharacter ());
        }

        [HttpGet ("{id}")]
        public async Task<IActionResult> GetSingle (int id) {
            ServiceResponse<GetCharacterDto> result = await _characterService.GetCharacterById (id);
            if (result.Data == null) {
                return NotFound (result);
            }
            return Ok (result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCharacter (AddCharacterDto newCharacter) {
            return Ok (await _characterService.AddCharacter (newCharacter));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCharacter (UpdateCharacterDto updateCharacter) {
            ServiceResponse<GetCharacterDto> response = await _characterService.UpdateCharacter (updateCharacter);
            if (response.Data == null) {
                return NotFound (response);
            }
            return Ok (response);
        }

        [HttpDelete ("{id}")]
        public async Task<IActionResult> DeleteCharacter (int id) {
            ServiceResponse<List<GetCharacterDto>> response = await _characterService.DeleteCharacter (id);
            if (response.Data == null) {
                return NotFound (response);
            }
            return Ok (response);
        }
    }
}