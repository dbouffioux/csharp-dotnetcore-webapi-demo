using AutoMapper;
using test_dotnet_webapi.Models;
using test_dotnet_webapi.Dtos.Character;

namespace test_dotnet_webapi {

    public class AutoMapperProfile : Profile 
    {
        public AutoMapperProfile() {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();
            CreateMap<UpdateCharacterDto, GetCharacterDto>();
        }
    }
}