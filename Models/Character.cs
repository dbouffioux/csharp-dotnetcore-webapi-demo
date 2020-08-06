namespace test_dotnet_webapi.Models
{
    public class Character
    { 
        public int Id { get; set; }
        public string Name { get; set; } = "Frodo";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public Weapon Weapon { get; set; }
        public RpgEnum Class { get; set; } = RpgEnum.Knight;
        public User User { get; set; }
    }
}