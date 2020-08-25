using System.Threading.Tasks;
using test_dotnet_webapi.Dtos.AddCharacterSkill;
using test_dotnet_webapi.Dtos.Character;
using test_dotnet_webapi.Models;

namespace test_dotnet_webapi.Services.CharacterSkillService
{
    public interface ICharacterSkillService
    {
        Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill);
    }
}