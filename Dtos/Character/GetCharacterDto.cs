using test_dotnet_webapi.Dtos.Weapon;
using test_dotnet_webapi.Models;

namespace test_dotnet_webapi.Dtos.Character
{
    public class GetCharacterDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Frodo";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public RpgEnum Class { get; set; } = RpgEnum.Knight;
        public GetWeaponDto Weapon { get; set; }
    }
}