using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using test_dotnet_webapi.Data;
using test_dotnet_webapi.Dtos.Character;
using test_dotnet_webapi.Dtos.Weapon;
using test_dotnet_webapi.Models;

namespace test_dotnet_webapi.Services.WeaponService {
    public class WeaponService : IWeaponService {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public WeaponService (
            DataContext context,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper
        ) {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<ServiceResponse<GetCharacterDto>> AddWeapon (AddWeaponDto newWeapon) {
            ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto> ();
            try {
                Character character = await _context.Characters
                    .FirstOrDefaultAsync (c => c.Id == newWeapon.CharacterId &&
                        c.User.Id == int.Parse (_httpContextAccessor.HttpContext.User.FindFirstValue (ClaimTypes.NameIdentifier)));
                if (character == null) {
                    response.Success = false;
                    response.Message = "Character not found.";
                } else {
                    Weapon weapon = new Weapon {
                        Name = newWeapon.Name,
                        Damage = newWeapon.Damage,
                        Character = character
                    };
                    await _context.Weapons.AddAsync (weapon);
                    await _context.SaveChangesAsync ();

                    response.Data = _mapper.Map<GetCharacterDto> (character);
                }
            } catch (Exception ex) {
                response.Success = false;
                response.Message = ex.Message;

            }
            return response;
        }
    }
}