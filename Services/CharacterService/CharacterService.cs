using System.Security.Claims;
using System.Xml.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using test_dotnet_webapi.Dtos.Character;
using test_dotnet_webapi.Models;
using test_dotnet_webapi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace test_dotnet_webapi.Services.CharacterService {
    public class CharacterService : ICharacterService {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharacterService (
            IMapper mapper,
            DataContext context,
            IHttpContextAccessor httpContextAccessor
        ) {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacter () {
            ServiceResponse<List<GetCharacterDto>> response = new ServiceResponse<List<GetCharacterDto>> ();
            List<Character> dbCharacters = await _context.Characters.Where(x => x.User.Id == GetUserId()).ToListAsync();
            response.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById (int id) {
            ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto> ();
            Character dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
            response.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter (AddCharacterDto newCharacter) {
            ServiceResponse<List<GetCharacterDto>> response = new ServiceResponse<List<GetCharacterDto>> ();
            Character character = _mapper.Map<Character>(newCharacter);
            character.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
            await _context.Characters.AddAsync(character);
            await _context.SaveChangesAsync();
            response.Data = _context.Characters.Where(c => c.User.Id == GetUserId()).Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character character = await _context.Characters.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
                if (character.User.Id == GetUserId())
                {
                    character.Name = updatedCharacter.Name;
                    character.Class = updatedCharacter.Class;
                    character.Defense = updatedCharacter.Defense;
                    character.HitPoints = updatedCharacter.HitPoints;
                    character.Intelligence = updatedCharacter.Intelligence;
                    character.Strength = updatedCharacter.Strength;
                    _context.Characters.Update(character);
                    await _context.SaveChangesAsync();
                    response.Data = _mapper.Map<GetCharacterDto>(character);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Character not found.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter (int id) {
            ServiceResponse<List<GetCharacterDto>> response = new ServiceResponse<List<GetCharacterDto>> ();
            try {
                Character character = await _context.Characters.FirstAsync(c => c.Id == id && c.User.Id == GetUserId());
                if (character != null)
                {
                    _context.Characters.Remove(character);
                    await _context.SaveChangesAsync();
                    response.Data =  _context.Characters.Where(c => c.User.Id == GetUserId()).Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
                } else {
                    response.Success = false;
                    response.Message = "Character not found.";
                }
            } catch(Exception exception) {
                response.Success = false;
                response.Message = exception.Message;
            }
            return response;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}