using System.Collections.Generic;
using System.Threading.Tasks;
using test_dotnet_webapi.Dtos.Character;
using test_dotnet_webapi.Models;

namespace test_dotnet_webapi.Services.CharacterService {
    public interface ICharacterService {
        Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacter (int userId);
        Task<ServiceResponse<GetCharacterDto>> GetCharacterById (int id);
        Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter (AddCharacterDto newCharacter);
        Task<ServiceResponse<GetCharacterDto>> UpdateCharacter (UpdateCharacterDto updateCharacter);
        Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter (int id);
    }
}