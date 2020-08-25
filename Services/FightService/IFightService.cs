using System.Threading.Tasks;
using test_dotnet_webapi.Dtos.Fight;
using test_dotnet_webapi.Dtos.Skill;
using test_dotnet_webapi.Models;

namespace test_dotnet_webapi.Services.FightService
{
    public interface IFightService
    {
        Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request);
        Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request);
        Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto request);
    }
}