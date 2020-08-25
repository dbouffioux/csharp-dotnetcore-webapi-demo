using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using test_dotnet_webapi.Dtos.AddCharacterSkill;
using test_dotnet_webapi.Services.CharacterSkillService;

namespace test_dotnet_webapi.Controllers {
    [Authorize]
    [ApiController]
    [Route ("[controller]")]
    public class CharacterSkillController : ControllerBase {
        private readonly ICharacterSkillService _characterSkillService;
        public CharacterSkillController (ICharacterSkillService characterSkillService) {
            _characterSkillService = characterSkillService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCharacterSkill (AddCharacterSkillDto newCharacterSkill) {
            return Ok (await _characterSkillService.AddCharacterSkill (newCharacterSkill));
        }
    }
}