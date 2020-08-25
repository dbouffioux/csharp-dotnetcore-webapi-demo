using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using test_dotnet_webapi.Dtos.Weapon;
using test_dotnet_webapi.Services.WeaponService;

namespace test_dotnet_webapi.Controllers {

    [Authorize]
    [ApiController]
    [Route ("[controller]")]
    public class WeaponController : ControllerBase {
        private readonly IWeaponService _weaponService;
        public WeaponController (IWeaponService weaponService) {
            _weaponService = weaponService;
        }

        [HttpPost]
        public async Task<IActionResult> AddWeapon (AddWeaponDto newWeapon) {
            return Ok (await _weaponService.AddWeapon (newWeapon));
        }
    }
}