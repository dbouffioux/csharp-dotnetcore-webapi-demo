using System.Threading.Tasks;
using test_dotnet_webapi.Dtos.Character;
using test_dotnet_webapi.Dtos.Weapon;
using test_dotnet_webapi.Models;

namespace test_dotnet_webapi.Services.WeaponService
{
    public interface IWeaponService
    {
        Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon);
    }
}