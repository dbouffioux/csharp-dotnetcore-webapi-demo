using AutoMapper;
using test_dotnet_webapi.Models;
using test_dotnet_webapi.Dtos.Character;
using test_dotnet_webapi.Dtos.Weapon;
using test_dotnet_webapi.Dtos.Skill;
using System.Linq;

namespace test_dotnet_webapi {

    public class AutoMapperProfile : Profile 
    {
        public AutoMapperProfile() {
            CreateMap<AddCharacterDto, Character>();
            CreateMap<UpdateCharacterDto, GetCharacterDto>();
            CreateMap<Weapon, GetWeaponDto>();
            CreateMap<Skill, GetSkillDto>();
            CreateMap<Character, GetCharacterDto>().ForMember(dto => dto.Skills, c => c.MapFrom(c => c.CharacterSkills.Select(cs => cs.Skill)));
        }
    }
}